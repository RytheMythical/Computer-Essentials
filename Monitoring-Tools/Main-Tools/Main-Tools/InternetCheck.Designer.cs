namespace Main_Tools
{
    partial class InternetCheck
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
            this.button1 = new System.Windows.Forms.Button();
            this.webControl1 = new EO.WinForm.WebControl();
            this.MbpsTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BigBTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ServiceProviderLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(883, 570);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 40);
            this.button1.TabIndex = 2;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // webControl1
            // 
            this.webControl1.BackColor = System.Drawing.Color.White;
            this.webControl1.Location = new System.Drawing.Point(12, 12);
            this.webControl1.Name = "webControl1";
            this.webControl1.Size = new System.Drawing.Size(995, 552);
            this.webControl1.TabIndex = 3;
            this.webControl1.Text = "webControl1";
            // 
            // MbpsTextBox
            // 
            this.MbpsTextBox.Location = new System.Drawing.Point(168, 590);
            this.MbpsTextBox.Name = "MbpsTextBox";
            this.MbpsTextBox.Size = new System.Drawing.Size(121, 20);
            this.MbpsTextBox.TabIndex = 4;
            this.MbpsTextBox.TextChanged += new System.EventHandler(this.MbpsTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(286, 567);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Convert Mbps to MBps";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(295, 593);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Mbps";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(331, 593);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "=";
            // 
            // BigBTextBox
            // 
            this.BigBTextBox.Location = new System.Drawing.Point(356, 590);
            this.BigBTextBox.Name = "BigBTextBox";
            this.BigBTextBox.Size = new System.Drawing.Size(93, 20);
            this.BigBTextBox.TabIndex = 8;
            this.BigBTextBox.TextChanged += new System.EventHandler(this.BigBTextBox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(455, 593);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "MBps";
            // 
            // ServiceProviderLabel
            // 
            this.ServiceProviderLabel.AutoSize = true;
            this.ServiceProviderLabel.Location = new System.Drawing.Point(516, 584);
            this.ServiceProviderLabel.Name = "ServiceProviderLabel";
            this.ServiceProviderLabel.Size = new System.Drawing.Size(113, 13);
            this.ServiceProviderLabel.TabIndex = 10;
            this.ServiceProviderLabel.Text = "Your Service Provider:";
            // 
            // InternetCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(1019, 622);
            this.Controls.Add(this.ServiceProviderLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BigBTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MbpsTextBox);
            this.Controls.Add(this.webControl1);
            this.Controls.Add(this.button1);
            this.Name = "InternetCheck";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.InternetCheck_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private EO.WinForm.WebControl webControl1;
        private System.Windows.Forms.TextBox MbpsTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox BigBTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label ServiceProviderLabel;
    }
}