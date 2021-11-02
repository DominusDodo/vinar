
namespace Vinar
{
    partial class AboutView
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
            this.tabControlVinar = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBoxLicence = new System.Windows.Forms.TextBox();
            this.listBoxDependencies = new System.Windows.Forms.ListBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.tabControlVinar.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlVinar
            // 
            this.tabControlVinar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlVinar.Controls.Add(this.tabPage1);
            this.tabControlVinar.Controls.Add(this.tabPage2);
            this.tabControlVinar.Location = new System.Drawing.Point(13, 13);
            this.tabControlVinar.Name = "tabControlVinar";
            this.tabControlVinar.SelectedIndex = 0;
            this.tabControlVinar.Size = new System.Drawing.Size(775, 412);
            this.tabControlVinar.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(767, 386);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "About";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(661, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vinar provides a means to replace the narration of a video, modifying the words s" +
    "poken and their timings, and substituting synthetic speech.\r\n\r\nIt was created by" +
    " a team of four named Group M.";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBoxLicence);
            this.tabPage2.Controls.Add(this.listBoxDependencies);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(767, 386);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Licences";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBoxLicence
            // 
            this.textBoxLicence.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLicence.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxLicence.Location = new System.Drawing.Point(213, 7);
            this.textBoxLicence.Multiline = true;
            this.textBoxLicence.Name = "textBoxLicence";
            this.textBoxLicence.ReadOnly = true;
            this.textBoxLicence.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLicence.Size = new System.Drawing.Size(548, 368);
            this.textBoxLicence.TabIndex = 1;
            this.textBoxLicence.Text = "Select a dependency to show its licence.";
            // 
            // listBoxDependencies
            // 
            this.listBoxDependencies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxDependencies.FormattingEnabled = true;
            this.listBoxDependencies.Location = new System.Drawing.Point(7, 7);
            this.listBoxDependencies.Name = "listBoxDependencies";
            this.listBoxDependencies.Size = new System.Drawing.Size(200, 368);
            this.listBoxDependencies.TabIndex = 0;
            this.listBoxDependencies.SelectedIndexChanged += new System.EventHandler(this.listBoxDependencies_SelectedIndexChanged);
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(709, 431);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // AboutView
            // 
            this.AcceptButton = this.buttonClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(800, 461);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.tabControlVinar);
            this.Name = "AboutView";
            this.ShowIcon = false;
            this.Text = "About Vinar";
            this.Load += new System.EventHandler(this.AboutView_Load);
            this.tabControlVinar.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlVinar;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxDependencies;
        private System.Windows.Forms.TextBox textBoxLicence;
        private System.Windows.Forms.Button buttonClose;
    }
}