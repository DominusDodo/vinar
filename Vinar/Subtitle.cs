﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vinar
{
    public partial class Subtitle : UserControl
    {
        private DateTime dt;

        public event EventHandler TextBoxGotFocus;
        public event EventHandler TextBoxLostFocus;
        public event EventHandler TimestampDoubleClicked;

        public string Content
        {
            get { return textBoxContent.Text; }
            set { textBoxContent.Text = value; }
        }

        public int ContentCursor
        {
            get { return textBoxContent.SelectionStart; }
            set { textBoxContent.SelectionStart = value; }
        }

        public DateTime Timestamp
        {
            get { return dt; }
            set
            {
                dt = value;
                labelTimestamp.Text = String.Format("{0}:{1:d2}:{2:d2}.{3:d2}", dt.Hour, dt.Minute, dt.Second, dt.Millisecond / 10);
            }
        }

        public bool TimestampEditing
        {
            get { return labelTimestamp.ForeColor == SystemColors.ControlText; }
            set { labelTimestamp.ForeColor = value ? Color.Blue : SystemColors.ControlText; }
        }

        public Subtitle()
        {
            InitializeComponent();

            textBoxContent.GotFocus += (object sender, EventArgs e) => TextBoxGotFocus?.Invoke(this, e);
            textBoxContent.LostFocus += (object sender, EventArgs e) => TextBoxLostFocus?.Invoke(this, e);
            labelTimestamp.DoubleClick += (object sender, EventArgs e) => TimestampDoubleClicked?.Invoke(this, e);
        }

        private void textBoxContent_TextChanged(object sender, EventArgs e)
        {
            textBoxContent.Height = TextRenderer.MeasureText(
                textBoxContent.Text == "" ? " " : textBoxContent.Text,
                textBoxContent.Font,
                new Size(textBoxContent.ClientSize.Width, 1000),
                TextFormatFlags.WordBreak
            ).Height + textBoxContent.Margin.Top + textBoxContent.Margin.Bottom;

            Height = textBoxContent.Height + 7;
        }
    }
}