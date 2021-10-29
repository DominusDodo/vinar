namespace Vinar
{
    partial class Subtitle
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelTimestamp = new System.Windows.Forms.Label();
            this.textBoxContent = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelTimestamp
            // 
            this.labelTimestamp.AutoSize = true;
            this.labelTimestamp.Location = new System.Drawing.Point(4, 6);
            this.labelTimestamp.Name = "labelTimestamp";
            this.labelTimestamp.Size = new System.Drawing.Size(58, 13);
            this.labelTimestamp.TabIndex = 0;
            this.labelTimestamp.Text = "0:00:00.00";
            // 
            // textBoxContent
            // 
            this.textBoxContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxContent.Location = new System.Drawing.Point(68, 3);
            this.textBoxContent.Multiline = true;
            this.textBoxContent.Name = "textBoxContent";
            this.textBoxContent.Size = new System.Drawing.Size(328, 46);
            this.textBoxContent.TabIndex = 1;
            this.textBoxContent.Text = "...";
            this.textBoxContent.TextChanged += new System.EventHandler(this.textBoxContent_TextChanged);
            // 
            // Subtitle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxContent);
            this.Controls.Add(this.labelTimestamp);
            this.Name = "Subtitle";
            this.Size = new System.Drawing.Size(400, 53);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTimestamp;
        private System.Windows.Forms.TextBox textBoxContent;
    }
}
