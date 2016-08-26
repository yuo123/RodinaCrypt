namespace RodinaCrypt
{
    partial class Hints
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
            this.instBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // instBox
            // 
            this.instBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.instBox.Location = new System.Drawing.Point(0, 0);
            this.instBox.Multiline = true;
            this.instBox.Name = "instBox";
            this.instBox.ReadOnly = true;
            this.instBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.instBox.Size = new System.Drawing.Size(343, 102);
            this.instBox.TabIndex = 0;
            this.instBox.TabStop = false;
            this.instBox.Text = "Hints:\r\n\r\nThe first line of 1w.png is \"fellow:\" (without the quotation marks)\r\n\r\n" +
    "The second line of 201.png is \"most high regional leader:\"";
            // 
            // Hints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 102);
            this.Controls.Add(this.instBox);
            this.Name = "Hints";
            this.Text = "Hints";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox instBox;
    }
}