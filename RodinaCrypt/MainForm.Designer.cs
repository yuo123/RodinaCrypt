namespace RodinaCrypt
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label label1;
            System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
            System.Windows.Forms.MenuStrip menuStrip1;
            System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
            this.wordWrapBox = new System.Windows.Forms.CheckBox();
            this.textSizeBar = new System.Windows.Forms.TrackBar();
            this.outBox = new System.Windows.Forms.RichTextBox();
            this.openMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSavedCharacterSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveDecodedMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCharacterSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.charsetView = new System.Windows.Forms.DataGridView();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openCharsetDialog = new System.Windows.Forms.OpenFileDialog();
            this.codeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cypherDictionaryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
            label1 = new System.Windows.Forms.Label();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textSizeBar)).BeginInit();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.charsetView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cypherDictionaryBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.Dock = System.Windows.Forms.DockStyle.Fill;
            label1.Location = new System.Drawing.Point(3, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(81, 39);
            label1.TabIndex = 3;
            label1.Text = "Text Size:";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(this.wordWrapBox, 0, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(this.textSizeBar, 1, 0);
            tableLayoutPanel1.Controls.Add(this.outBox, 0, 2);
            tableLayoutPanel1.Location = new System.Drawing.Point(512, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(478, 500);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // wordWrapBox
            // 
            this.wordWrapBox.AutoSize = true;
            this.wordWrapBox.Checked = true;
            this.wordWrapBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.wordWrapBox.Location = new System.Drawing.Point(3, 42);
            this.wordWrapBox.Name = "wordWrapBox";
            this.wordWrapBox.Size = new System.Drawing.Size(81, 17);
            this.wordWrapBox.TabIndex = 5;
            this.wordWrapBox.Text = "Word Wrap";
            this.wordWrapBox.UseVisualStyleBackColor = true;
            this.wordWrapBox.CheckedChanged += new System.EventHandler(this.wordWrapBox_CheckedChanged);
            // 
            // textSizeBar
            // 
            this.textSizeBar.AutoSize = false;
            this.textSizeBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textSizeBar.LargeChange = 25;
            this.textSizeBar.Location = new System.Drawing.Point(90, 3);
            this.textSizeBar.Maximum = 400;
            this.textSizeBar.Minimum = 50;
            this.textSizeBar.Name = "textSizeBar";
            tableLayoutPanel1.SetRowSpan(this.textSizeBar, 2);
            this.textSizeBar.Size = new System.Drawing.Size(385, 56);
            this.textSizeBar.SmallChange = 5;
            this.textSizeBar.TabIndex = 2;
            this.textSizeBar.Value = 100;
            this.textSizeBar.Scroll += new System.EventHandler(this.textSizeBar_Scroll);
            // 
            // outBox
            // 
            tableLayoutPanel1.SetColumnSpan(this.outBox, 2);
            this.outBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outBox.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outBox.HideSelection = false;
            this.outBox.Location = new System.Drawing.Point(3, 65);
            this.outBox.Name = "outBox";
            this.outBox.ReadOnly = true;
            this.outBox.Size = new System.Drawing.Size(472, 432);
            this.outBox.TabIndex = 0;
            this.outBox.Text = "";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            fileToolStripMenuItem});
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(990, 24);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMessageToolStripMenuItem,
            this.openSavedCharacterSetToolStripMenuItem,
            this.saveDecodedMessageToolStripMenuItem,
            this.saveCharacterSetToolStripMenuItem});
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openMessageToolStripMenuItem
            // 
            this.openMessageToolStripMenuItem.Name = "openMessageToolStripMenuItem";
            this.openMessageToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.openMessageToolStripMenuItem.Text = "Open Message...";
            this.openMessageToolStripMenuItem.Click += new System.EventHandler(this.openMessageToolStripMenuItem_Click);
            // 
            // openSavedCharacterSetToolStripMenuItem
            // 
            this.openSavedCharacterSetToolStripMenuItem.Name = "openSavedCharacterSetToolStripMenuItem";
            this.openSavedCharacterSetToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.openSavedCharacterSetToolStripMenuItem.Text = "Open Saved Character Set...";
            this.openSavedCharacterSetToolStripMenuItem.Click += new System.EventHandler(this.openSavedCharacterSetToolStripMenuItem_Click);
            // 
            // saveDecodedMessageToolStripMenuItem
            // 
            this.saveDecodedMessageToolStripMenuItem.Name = "saveDecodedMessageToolStripMenuItem";
            this.saveDecodedMessageToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.saveDecodedMessageToolStripMenuItem.Text = "Save Decoded Message...";
            this.saveDecodedMessageToolStripMenuItem.Click += new System.EventHandler(this.saveDecodedMessageToolStripMenuItem_Click);
            // 
            // saveCharacterSetToolStripMenuItem
            // 
            this.saveCharacterSetToolStripMenuItem.Name = "saveCharacterSetToolStripMenuItem";
            this.saveCharacterSetToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.saveCharacterSetToolStripMenuItem.Text = "Save Character Set...";
            this.saveCharacterSetToolStripMenuItem.Click += new System.EventHandler(this.saveCharacterSetToolStripMenuItem_Click);
            // 
            // charsetView
            // 
            this.charsetView.AllowUserToAddRows = false;
            this.charsetView.AllowUserToDeleteRows = false;
            this.charsetView.AllowUserToResizeRows = false;
            this.charsetView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.charsetView.AutoGenerateColumns = false;
            this.charsetView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.charsetView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.charsetView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codeDataGridViewTextBoxColumn,
            this.valueDataGridViewTextBoxColumn});
            this.charsetView.DataSource = this.cypherDictionaryBindingSource;
            this.charsetView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.charsetView.Location = new System.Drawing.Point(392, 0);
            this.charsetView.MultiSelect = false;
            this.charsetView.Name = "charsetView";
            this.charsetView.RowHeadersVisible = false;
            this.charsetView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.charsetView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.charsetView.Size = new System.Drawing.Size(117, 500);
            this.charsetView.TabIndex = 1;
            this.charsetView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.charsetView_CellEndEdit);
            this.charsetView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.charsetView_CellFormatting);
            this.charsetView.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.charsetView_CellParsing);
            this.charsetView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.charsetView_CellValueChanged);
            this.charsetView.SelectionChanged += new System.EventHandler(this.charsetView_SelectionChanged);
            this.charsetView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.charsetView_KeyDown);
            this.charsetView.Leave += new System.EventHandler(this.charsetView_Leave);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "txt";
            // 
            // openCharsetDialog
            // 
            this.openCharsetDialog.Filter = "Text Files|*.txt|All Files|*.*";
            // 
            // codeDataGridViewTextBoxColumn
            // 
            this.codeDataGridViewTextBoxColumn.DataPropertyName = "Code";
            this.codeDataGridViewTextBoxColumn.HeaderText = "Code";
            this.codeDataGridViewTextBoxColumn.MaxInputLength = 6;
            this.codeDataGridViewTextBoxColumn.Name = "codeDataGridViewTextBoxColumn";
            this.codeDataGridViewTextBoxColumn.ReadOnly = true;
            this.codeDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.codeDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.codeDataGridViewTextBoxColumn.Width = 57;
            // 
            // valueDataGridViewTextBoxColumn
            // 
            this.valueDataGridViewTextBoxColumn.DataPropertyName = "Value";
            this.valueDataGridViewTextBoxColumn.HeaderText = "Value";
            this.valueDataGridViewTextBoxColumn.MaxInputLength = 10;
            this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
            this.valueDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.valueDataGridViewTextBoxColumn.Width = 40;
            // 
            // cypherDictionaryBindingSource
            // 
            this.cypherDictionaryBindingSource.DataSource = typeof(RodinaCrypt.CipherDictionary);
            // 
            // openImageDialog
            // 
            this.openImageDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif;" +
    " *.png";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 500);
            this.Controls.Add(tableLayoutPanel1);
            this.Controls.Add(this.charsetView);
            this.Controls.Add(menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = menuStrip1;
            this.Name = "MainForm";
            this.Text = "Rodina Decryptor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textSizeBar)).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.charsetView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cypherDictionaryBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox outBox;
        private System.Windows.Forms.DataGridView charsetView;
        private System.Windows.Forms.BindingSource cypherDictionaryBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
        private System.Windows.Forms.TrackBar textSizeBar;
        private System.Windows.Forms.CheckBox wordWrapBox;
        private System.Windows.Forms.ToolStripMenuItem openMessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSavedCharacterSetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveDecodedMessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCharacterSetToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openCharsetDialog;
        private System.Windows.Forms.OpenFileDialog openImageDialog;
    }
}

