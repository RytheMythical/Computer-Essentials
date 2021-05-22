namespace InternetSpeedChecker
{
    partial class Form1
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
            this.DismissLabel = new System.Windows.Forms.LinkLabel();
            this.DownloadLabel = new System.Windows.Forms.Label();
            this.UploadLabel = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.PingLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DismissLabel
            // 
            this.DismissLabel.AutoSize = true;
            this.DismissLabel.Location = new System.Drawing.Point(1181, 9);
            this.DismissLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.DismissLabel.Name = "DismissLabel";
            this.DismissLabel.Size = new System.Drawing.Size(80, 25);
            this.DismissLabel.TabIndex = 3;
            this.DismissLabel.TabStop = true;
            this.DismissLabel.Text = "Dismiss";
            this.DismissLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DismissLabel_LinkClicked);
            // 
            // DownloadLabel
            // 
            this.DownloadLabel.AutoSize = true;
            this.DownloadLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.DownloadLabel.Location = new System.Drawing.Point(12, 9);
            this.DownloadLabel.Name = "DownloadLabel";
            this.DownloadLabel.Size = new System.Drawing.Size(126, 46);
            this.DownloadLabel.TabIndex = 4;
            this.DownloadLabel.Text = "label1";
            // 
            // UploadLabel
            // 
            this.UploadLabel.AutoSize = true;
            this.UploadLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.UploadLabel.Location = new System.Drawing.Point(435, 9);
            this.UploadLabel.Name = "UploadLabel";
            this.UploadLabel.Size = new System.Drawing.Size(126, 46);
            this.UploadLabel.TabIndex = 5;
            this.UploadLabel.Text = "label1";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 25;
            this.listBox1.Location = new System.Drawing.Point(12, 77);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(1246, 479);
            this.listBox1.TabIndex = 6;
            // 
            // PingLabel
            // 
            this.PingLabel.AutoSize = true;
            this.PingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.PingLabel.Location = new System.Drawing.Point(865, 9);
            this.PingLabel.Name = "PingLabel";
            this.PingLabel.Size = new System.Drawing.Size(126, 46);
            this.PingLabel.TabIndex = 7;
            this.PingLabel.Text = "label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 564);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 42);
            this.button1.TabIndex = 8;
            this.button1.Text = "Copy";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1276, 618);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.PingLabel);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.UploadLabel);
            this.Controls.Add(this.DownloadLabel);
            this.Controls.Add(this.DismissLabel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Internet Settings";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.LinkLabel DismissLabel;
        private System.Windows.Forms.Label DownloadLabel;
        private System.Windows.Forms.Label UploadLabel;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label PingLabel;
        private System.Windows.Forms.Button button1;
    }
}

