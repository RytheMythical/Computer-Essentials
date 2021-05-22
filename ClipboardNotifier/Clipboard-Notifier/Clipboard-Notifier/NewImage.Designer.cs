
namespace Clipboard_Notifier
{
    partial class NewImage
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
            this.ClipboardPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ClipboardPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ClipboardPictureBox
            // 
            this.ClipboardPictureBox.Location = new System.Drawing.Point(12, 12);
            this.ClipboardPictureBox.Name = "ClipboardPictureBox";
            this.ClipboardPictureBox.Size = new System.Drawing.Size(689, 427);
            this.ClipboardPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ClipboardPictureBox.TabIndex = 0;
            this.ClipboardPictureBox.TabStop = false;
            // 
            // NewImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(713, 451);
            this.Controls.Add(this.ClipboardPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewImage";
            this.ShowIcon = false;
            this.Text = "New Image";
            this.Load += new System.EventHandler(this.NewImage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ClipboardPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ClipboardPictureBox;
    }
}