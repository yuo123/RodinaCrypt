using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RodinaCrypt
{
    public partial class KnownWordDialog : Form
    {
        public string Result { get; private set; }

        private int m_length;

        private KnownWordDialog()
        {
            InitializeComponent();
            this.Result = null;
        }

        public KnownWordDialog(int length, string existing) : this()
        {
            m_length = length;
            lengthLabel.Text = string.Format("({0} characters)", length);
            inputBox.MaxLength = length;
            inputBox.Text = existing;
        }

        private void inputBox_TextChanged(object sender, EventArgs e)
        {
            int prevHeight = inputBox.Height;
            inputBox.Height = (int)Math.Ceiling(this.CreateGraphics().MeasureString(inputBox.Text, inputBox.Font).Height) + inputBox.Margin.Vertical;
            this.Height += inputBox.Height - prevHeight;
        }

        private void confirmBtn_Click(object sender, EventArgs e)
        {
            if (inputBox.Text.Length != m_length)
                MessageBox.Show(string.Format("Incorrect length. {0} Characters expected, input was {1}", m_length, inputBox.Text.Length));
            else
            {
                Result = inputBox.Text;
                this.Close();
            }
        }
    }
}
