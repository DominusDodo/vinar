namespace Vinar
{
    partial class Vinar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Vinar));
            this.panelSubtitles = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.openSubtitlesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSubtitlesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSubtitlesAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.createNarrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subtitlesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transcribeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.insertBeforeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertAfterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeWithNextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeWithPreviousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialogSubtitles = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialogSubtitles = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialogVideo = new System.Windows.Forms.OpenFileDialog();
            this.timerPlayback = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.saveFileDialogVideo = new System.Windows.Forms.SaveFileDialog();
            this.axWindowsMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSubtitles
            // 
            this.panelSubtitles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelSubtitles.AutoScroll = true;
            this.panelSubtitles.Location = new System.Drawing.Point(12, 27);
            this.panelSubtitles.Name = "panelSubtitles";
            this.panelSubtitles.Size = new System.Drawing.Size(390, 370);
            this.panelSubtitles.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.subtitlesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(927, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openVideoToolStripMenuItem,
            this.toolStripMenuItem2,
            this.openSubtitlesToolStripMenuItem,
            this.saveSubtitlesToolStripMenuItem,
            this.saveSubtitlesAsToolStripMenuItem,
            this.toolStripMenuItem3,
            this.createNarrationToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openVideoToolStripMenuItem
            // 
            this.openVideoToolStripMenuItem.Name = "openVideoToolStripMenuItem";
            this.openVideoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.openVideoToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.openVideoToolStripMenuItem.Text = "Load video...";
            this.openVideoToolStripMenuItem.Click += new System.EventHandler(this.openVideoToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(237, 6);
            // 
            // openSubtitlesToolStripMenuItem
            // 
            this.openSubtitlesToolStripMenuItem.Name = "openSubtitlesToolStripMenuItem";
            this.openSubtitlesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openSubtitlesToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.openSubtitlesToolStripMenuItem.Text = "Open subtitles...";
            this.openSubtitlesToolStripMenuItem.Click += new System.EventHandler(this.openSubtitlesToolStripMenuItem_Click);
            // 
            // saveSubtitlesToolStripMenuItem
            // 
            this.saveSubtitlesToolStripMenuItem.Name = "saveSubtitlesToolStripMenuItem";
            this.saveSubtitlesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveSubtitlesToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.saveSubtitlesToolStripMenuItem.Text = "Save subtitles...";
            this.saveSubtitlesToolStripMenuItem.Click += new System.EventHandler(this.saveSubtitlesToolStripMenuItem_Click);
            // 
            // saveSubtitlesAsToolStripMenuItem
            // 
            this.saveSubtitlesAsToolStripMenuItem.Name = "saveSubtitlesAsToolStripMenuItem";
            this.saveSubtitlesAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveSubtitlesAsToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.saveSubtitlesAsToolStripMenuItem.Text = "Save subtitles as...";
            this.saveSubtitlesAsToolStripMenuItem.Click += new System.EventHandler(this.saveSubtitlesAsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(237, 6);
            // 
            // createNarrationToolStripMenuItem
            // 
            this.createNarrationToolStripMenuItem.Name = "createNarrationToolStripMenuItem";
            this.createNarrationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.createNarrationToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.createNarrationToolStripMenuItem.Text = "Create narration...";
            this.createNarrationToolStripMenuItem.Click += new System.EventHandler(this.createNarrationToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            // 
            // subtitlesToolStripMenuItem
            // 
            this.subtitlesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transcribeToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.toolStripMenuItem1,
            this.insertBeforeToolStripMenuItem,
            this.insertAfterToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.mergeWithNextToolStripMenuItem,
            this.mergeWithPreviousToolStripMenuItem,
            this.splitToolStripMenuItem});
            this.subtitlesToolStripMenuItem.Name = "subtitlesToolStripMenuItem";
            this.subtitlesToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.subtitlesToolStripMenuItem.Text = "Subtitles";
            // 
            // transcribeToolStripMenuItem
            // 
            this.transcribeToolStripMenuItem.Enabled = false;
            this.transcribeToolStripMenuItem.Name = "transcribeToolStripMenuItem";
            this.transcribeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.transcribeToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.transcribeToolStripMenuItem.Text = "Transcribe";
            this.transcribeToolStripMenuItem.Click += new System.EventHandler(this.transcribeToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(256, 6);
            // 
            // insertBeforeToolStripMenuItem
            // 
            this.insertBeforeToolStripMenuItem.Name = "insertBeforeToolStripMenuItem";
            this.insertBeforeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Insert)));
            this.insertBeforeToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.insertBeforeToolStripMenuItem.Text = "Insert before";
            this.insertBeforeToolStripMenuItem.Click += new System.EventHandler(this.insertBeforeToolStripMenuItem_Click);
            // 
            // insertAfterToolStripMenuItem
            // 
            this.insertAfterToolStripMenuItem.Name = "insertAfterToolStripMenuItem";
            this.insertAfterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)));
            this.insertAfterToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.insertAfterToolStripMenuItem.Text = "Insert after";
            this.insertAfterToolStripMenuItem.Click += new System.EventHandler(this.insertAfterToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // mergeWithNextToolStripMenuItem
            // 
            this.mergeWithNextToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Desktop;
            this.mergeWithNextToolStripMenuItem.Name = "mergeWithNextToolStripMenuItem";
            this.mergeWithNextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.mergeWithNextToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.mergeWithNextToolStripMenuItem.Text = "Merge with next";
            this.mergeWithNextToolStripMenuItem.Click += new System.EventHandler(this.mergeWithNextToolStripMenuItem_Click);
            // 
            // mergeWithPreviousToolStripMenuItem
            // 
            this.mergeWithPreviousToolStripMenuItem.Name = "mergeWithPreviousToolStripMenuItem";
            this.mergeWithPreviousToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.M)));
            this.mergeWithPreviousToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.mergeWithPreviousToolStripMenuItem.Text = "Merge with previous";
            this.mergeWithPreviousToolStripMenuItem.Click += new System.EventHandler(this.mergeWithPreviousToolStripMenuItem_Click);
            // 
            // splitToolStripMenuItem
            // 
            this.splitToolStripMenuItem.Name = "splitToolStripMenuItem";
            this.splitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.splitToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.splitToolStripMenuItem.Text = "Split at cursor";
            this.splitToolStripMenuItem.Click += new System.EventHandler(this.splitToolStripMenuItem_Click);
            // 
            // openFileDialogSubtitles
            // 
            this.openFileDialogSubtitles.Filter = "Subtitle files (*.vnr)|*.vnr";
            this.openFileDialogSubtitles.Title = "Select a subtitle file";
            // 
            // saveFileDialogSubtitles
            // 
            this.saveFileDialogSubtitles.Filter = "Subtitle files (*.vnr)|*.vnr";
            this.saveFileDialogSubtitles.Title = "Save subtitles";
            // 
            // openFileDialogVideo
            // 
            this.openFileDialogVideo.Filter = "Video files (*.mp4)|*.mp4";
            // 
            // timerPlayback
            // 
            this.timerPlayback.Tick += new System.EventHandler(this.timerPlayback_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 400);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(927, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel.Text = " Ready";
            // 
            // saveFileDialogVideo
            // 
            this.saveFileDialogVideo.Filter = "Video files (*.mp4)|*.mp4";
            this.saveFileDialogVideo.Title = "Generate narrated video";
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer.Enabled = true;
            this.axWindowsMediaPlayer.Location = new System.Drawing.Point(408, 27);
            this.axWindowsMediaPlayer.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer.Size = new System.Drawing.Size(507, 370);
            this.axWindowsMediaPlayer.TabIndex = 5;
            // 
            // Vinar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 422);
            this.Controls.Add(this.axWindowsMediaPlayer);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelSubtitles);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Vinar";
            this.Text = "Vinar";
            this.Load += new System.EventHandler(this.Vinar_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelSubtitles;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSubtitlesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openVideoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSubtitlesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subtitlesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertBeforeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertAfterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mergeWithNextToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialogSubtitles;
        private System.Windows.Forms.SaveFileDialog saveFileDialogSubtitles;
        private System.Windows.Forms.OpenFileDialog openFileDialogVideo;
        private System.Windows.Forms.Timer timerPlayback;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem transcribeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mergeWithPreviousToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem splitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNarrationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem saveSubtitlesAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.SaveFileDialog saveFileDialogVideo;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer;
    }
}

