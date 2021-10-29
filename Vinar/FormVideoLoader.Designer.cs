namespace Vinar
{
    partial class FormVideoLoader
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
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.progressBarGenerating = new System.Windows.Forms.ProgressBar();
            this.openFileDialogVideo = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(232, 37);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(114, 23);
            this.buttonGenerate.TabIndex = 0;
            this.buttonGenerate.Text = "Generate transcript";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Video path:";
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(80, 10);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(185, 20);
            this.textBoxPath.TabIndex = 2;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(271, 7);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse.TabIndex = 3;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // progressBarGenerating
            // 
            this.progressBarGenerating.Location = new System.Drawing.Point(16, 37);
            this.progressBarGenerating.Name = "progressBarGenerating";
            this.progressBarGenerating.Size = new System.Drawing.Size(210, 23);
            this.progressBarGenerating.TabIndex = 4;
            // 
            // openFileDialogVideo
            // 
            this.openFileDialogVideo.FileName = "openFileDialog1";
            // 
            // FormVideoLoader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 67);
            this.Controls.Add(this.progressBarGenerating);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonGenerate);
            this.Name = "FormVideoLoader";
            this.Text = "  ";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ProgressBar progressBarGenerating;
        private System.Windows.Forms.OpenFileDialog openFileDialogVideo;
    }
}