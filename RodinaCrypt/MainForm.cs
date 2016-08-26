using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Text;
using RodinaCrypt.Properties;

namespace RodinaCrypt
{
    public partial class MainForm : Form
    {
        public const int BLOCK_SIZE = 15;
        public const char UNKNOWN_CHAR = '�';

        private CipherDictionary dict;
        private CipherPredictionDictionary preDict; //<-- that's a pun :)
        private int[] data;

        private int curRow;
        private bool resetRow = false;

        public MainForm()
        {
            InitializeComponent();

            PrivateFontCollection pfc = InitFont();
            outBox.Font = new Font(pfc.Families[0], DefaultFont.Size, FontStyle.Regular);
        }

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        private static PrivateFontCollection InitFont()
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            int fontLength = Resources.Audiowide_Regular.Length;

            // create a buffer to read in to
            byte[] fontdata = Resources.Audiowide_Regular;

            // create an unsafe memory block for the font data
            IntPtr fdata = Marshal.AllocCoTaskMem(fontLength);

            // copy the bytes to the unsafe memory block
            Marshal.Copy(fontdata, 0, fdata, fontLength);

            // We HAVE to do this to register the font to the system (Weird .NET bug !)
            uint cFonts = 0;
            AddFontMemResourceEx(fdata, (uint)fontdata.Length, IntPtr.Zero, ref cFonts);

            // pass the font to the font collection
            pfc.AddMemoryFont(fdata, fontLength);

            // free up the unsafe memory
            Marshal.FreeCoTaskMem(fdata);
            return pfc;
        }

        public static int[] ReadImage(string file)
        {
            Bitmap bmp = new Bitmap(file);
            int[] data = new int[(bmp.Width / BLOCK_SIZE) * (bmp.Height / BLOCK_SIZE)];

            int offset = 0;
            for (int i = BLOCK_SIZE / 2; i < bmp.Height; i += BLOCK_SIZE)
            {
                for (int j = BLOCK_SIZE / 2; j < bmp.Width; j += BLOCK_SIZE, offset++)
                {
                    data[offset] = bmp.GetPixel(j, i).ToArgb();
                }
            }

            return data;
        }

        public static string Substitute(int[] data, CipherDictionary dict)
        {
            StringBuilder sb = new StringBuilder(data.Length);
            for (int i = 0; i < data.Length; i++)
            {
                char c;
                if (!dict.TryGetValue(data[i], out c))
                    c = UNKNOWN_CHAR;
                sb.Append(c);
            }
            return sb.ToString();
        }

        public void ReadDictFile(string file)
        {
            string[] lines = File.ReadAllLines(file);
            dict = new CipherDictionary(lines.Length);
            foreach (string line in lines)
            {
                if (line.Length == 10)
                {
                    char c = line[0];
                    if (c == 'C')
                        c = '\r';
                    else if (c == 'L')
                        c = '\n';
                    else if (c == 'E')
                        c = (char)26;
                    string hex = line.Substring(4, 6);
                    dict.Add(0xFF << 24 | Convert.ToInt32(hex, 16), c);
                }
            }
            if (data != null)
                dict.AddMissingValues(data);
            charsetView.DataSource = dict.BindingList;
        }

        public void SaveDictFile(CipherDictionary dict, string path)
        {
            try
            {
                StreamWriter writer = File.CreateText(path);
                foreach (CipherPair pair in dict)
                {
                    writer.WriteLine("{1} = {0:X6}", 0x00FFFFFF & pair.Code, GetPrintableForm(pair.Value));
                }
                writer.Close();
            }
            catch (IOException e)
            {
                throw new ArgumentException("IO error saving file", "path", e);
            }
        }

        private static char GetPrintableForm(char value)
        {
            if (!char.IsControl(value))
                return value;
            else switch (value)
                {
                    case '\r': return 'C';
                    case '\n': return 'L';
                    case (char)26: return 'E';
                    default: throw new ArgumentException("Unknown special value", "value");
                }
        }

        public void BestGuess()
        {
            CipherPair eof = null;
            if (data[data.Length - 1] == data[data.Length - 2] && data[data.Length - 1] == data[data.Length - 3])
            {
                eof = new CipherPair(data[data.Length - 1], (char)26);
            }

            var codelist = data.Distinct();
            var dicts = new List<CipherPredictionDictionary>();
            dicts.Add(CipherPredictionDictionary.SingleFrequencyPrediction(data, eof));
            int spaceCode = dicts[0].Find(p => p.MostLikely().Value == ' ').MostLikely().Code;
            dicts.Add(CipherPredictionDictionary.PairFrequencyPrediction(data, codelist, eof));
            dicts.Add(CipherPredictionDictionary.FirstLetterPrediction(data, spaceCode));
            dicts.Add(CipherPredictionDictionary.KnownWordPrediction(data, "<untranslatable>"));
            preDict = CipherPredictionDictionary.AggregateProbabilities(dicts, codelist);
            dict = preDict.MostLikely();
            dict.AddMissingValues(codelist);
            charsetView.DataSource = dict.BindingList;
        }

        private void UpdateOutput()
        {
            if (data != null)
            {
                outBox.Text = Substitute(data, dict);
            }
        }

        private void charsetView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
                e.Value = ((int)e.Value & 0x00FFFFFF).ToString("X6").ToUpper();
            else if ((char)e.Value == '\r')
                e.Value = "\\r";
            else if ((char)e.Value == '\n')
                e.Value = "\\n";
            else if ((char)e.Value == (char)26)
                e.Value = "EOF";
        }

        private void charsetView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            UpdateOutput();
        }

        private void charsetView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if ((string)e.Value == "\\n")
            {
                e.Value = '\n';
                e.ParsingApplied = true;
            }
            else if ((string)e.Value == "\\r")
            {
                e.Value = '\r';
                e.ParsingApplied = true;
            }
            else if ((string)e.Value == "EOF")
            {
                e.Value = (char)26;
                e.ParsingApplied = true;
            }
            else
            {
                e.Value = char.ToLower(((string)e.Value)[0]);
                e.ParsingApplied = true;
            }
        }

        private void textSizeBar_Scroll(object sender, EventArgs e)
        {
            outBox.ZoomFactor = (textSizeBar.Value / 100.0f);
        }

        private void wordWrapBox_CheckedChanged(object sender, EventArgs e)
        {
            outBox.WordWrap = wordWrapBox.Checked;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && outBox.SelectionLength > 0)
                ShowKnownWordDialog();
        }

        private void ShowKnownWordDialog()
        {
            int selectionStart = outBox.SelectionStart + RealCount(0, outBox.SelectionStart);
            int selectionLength = outBox.SelectionLength + RealCount(outBox.SelectionStart, outBox.SelectionLength);
            var diag = new KnownWordDialog(selectionLength, this.outBox.SelectedText);
            diag.ShowDialog(this);
            if (diag.Result == null)
                return;
            ProccessknownWord(selectionStart, diag.Result);
        }

        private void ProccessknownWord(int start, string word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                int code = this.data[start + i];
                dict.GetPairForCode(code).Value = word[i];
            }
            int curLoc = outBox.SelectionStart;
            dict.BindingList.ResetBindings();
            UpdateOutput();
            outBox.SelectionStart = curLoc;
        }

        /// <summary>
        /// Calculates the real number of characters in a given span of outBox, properly counting newlines as two characters
        /// </summary>
        private int RealCount(int start, int length)
        {
            string text = outBox.Text;
            int c = 0;
            for (int i = start; i < length; i++)
            {
                if (text[i] == '\n')
                    c++;
            }
            return c;
        }

        [DllImport("user32.dll", EntryPoint = "LockWindowUpdate", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr LockWindow(IntPtr Handle);

        private void charsetView_SelectionChanged(object sender, EventArgs e)
        {
            if (data == null)
            {
                return;
            }
            if (resetRow)
            {
                charsetView.CurrentCell = charsetView.Rows[curRow].Cells[0];
                resetRow = false;
            }
            else
            {
                LockWindow(this.Handle);
                ClearHighlight();
                CipherPair pair;
                if (charsetView.SelectedRows.Count > 0 && (pair = (CipherPair)charsetView.SelectedRows[0].DataBoundItem) != null)
                {
                    int newlines = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        if (data[i] == pair.Code)
                        {
                            outBox.Select(i - newlines, 1);
                            outBox.SelectionBackColor = Color.Yellow;
                        }
                        if (dict.GetPairForCode(data[i]).Value == '\r')
                            newlines++;
                    }
                    outBox.SelectionStart = 0;
                    outBox.SelectionLength = 0;
                    outBox.SelectionBackColor = Color.Black;
                }
                LockWindow(IntPtr.Zero);
            }
        }

        private void charsetView_Leave(object sender, EventArgs e)
        {
            ClearHighlight();
        }

        private void ClearHighlight()
        {
            outBox.SelectionStart = 0;
            outBox.SelectionLength = outBox.Text.Length;
            outBox.SelectionBackColor = outBox.BackColor;
            outBox.DeselectAll();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ClearHighlight();
        }

        private void charsetView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (charsetView.SelectedRows.Count > 0)
                {
                    charsetView.CurrentCell = charsetView.SelectedRows[0].Cells[1];
                    charsetView.BeginEdit(true);
                    e.Handled = true;
                }
                else
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                }

        }

        private void charsetView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!resetRow)
            {
                curRow = e.RowIndex;
                resetRow = true;
            }
        }

        private void saveCharacterSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveDictFile(dict, saveFileDialog.FileName);
            }
        }

        private void openSavedCharacterSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openCharsetDialog.ShowDialog() == DialogResult.OK)
            {
                ReadDictFile(openCharsetDialog.FileName);
                UpdateOutput();
            }
        }

        private void saveDecodedMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, Substitute(data, dict));
            }
        }

        private void openMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openImageDialog.ShowDialog() == DialogResult.OK)
            {
                data = ReadImage(openImageDialog.FileName);
                if (dict == null)
                    BestGuess();
                else
                    for (int i = 0; i < data.Length; i++)
                    {
                        if (!dict.ContainsKey(data[i]))
                            dict.Add(new CipherPair(data[i]));
                    }
                UpdateOutput();
                dict.BindingList.ResetBindings();
                this.Text = "Rodina Decryptor - " + Path.GetFileNameWithoutExtension(openImageDialog.FileName);
            }
        }

        private void howDoIUseThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new InstructionDialog().Show();
        }

        private void hintsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Hints().Show();
        }
    }
}