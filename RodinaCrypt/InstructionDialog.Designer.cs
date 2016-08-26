namespace RodinaCrypt
{
    partial class InstructionDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstructionDialog));
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
            this.instBox.Size = new System.Drawing.Size(703, 403);
            this.instBox.TabIndex = 0;
            this.instBox.TabStop = false;
            this.instBox.Text = resources.GetString("instBox.Text");
            // 
            // InstructionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 403);
            this.Controls.Add(this.instBox);
            this.Name = "InstructionDialog";
            this.Text = "Instructions";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox instBox;
    }
}