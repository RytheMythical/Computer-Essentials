namespace GitHub_Large_Uploader
{
    partial class Automation_Settings
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.DecryptionKeyTextBox = new System.Windows.Forms.RichTextBox();
            this.EncryptFilesCheckBox = new System.Windows.Forms.CheckBox();
            this.Base64CheckBox = new System.Windows.Forms.CheckBox();
            this.DoneButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(4, 6);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(546, 274);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.DecryptionKeyTextBox);
            this.tabPage1.Controls.Add(this.EncryptFilesCheckBox);
            this.tabPage1.Controls.Add(this.Base64CheckBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(538, 245);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General Automation";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(156, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Please paste your decryption key below:";
            // 
            // DecryptionKeyTextBox
            // 
            this.DecryptionKeyTextBox.Enabled = false;
            this.DecryptionKeyTextBox.Location = new System.Drawing.Point(23, 93);
            this.DecryptionKeyTextBox.Name = "DecryptionKeyTextBox";
            this.DecryptionKeyTextBox.Size = new System.Drawing.Size(508, 141);
            this.DecryptionKeyTextBox.TabIndex = 2;
            this.DecryptionKeyTextBox.Text = "";
            // 
            // EncryptFilesCheckBox
            // 
            this.EncryptFilesCheckBox.AutoSize = true;
            this.EncryptFilesCheckBox.Location = new System.Drawing.Point(23, 52);
            this.EncryptFilesCheckBox.Name = "EncryptFilesCheckBox";
            this.EncryptFilesCheckBox.Size = new System.Drawing.Size(127, 21);
            this.EncryptFilesCheckBox.TabIndex = 1;
            this.EncryptFilesCheckBox.Text = "Encrypt All Files";
            this.EncryptFilesCheckBox.UseVisualStyleBackColor = true;
            this.EncryptFilesCheckBox.CheckedChanged += new System.EventHandler(this.EncryptFilesCheckBox_CheckedChanged);
            // 
            // Base64CheckBox
            // 
            this.Base64CheckBox.AutoSize = true;
            this.Base64CheckBox.Location = new System.Drawing.Point(23, 15);
            this.Base64CheckBox.Name = "Base64CheckBox";
            this.Base64CheckBox.Size = new System.Drawing.Size(178, 21);
            this.Base64CheckBox.TabIndex = 0;
            this.Base64CheckBox.Text = "Base64 Encode all Files";
            this.Base64CheckBox.UseVisualStyleBackColor = true;
            // 
            // DoneButton
            // 
            this.DoneButton.Location = new System.Drawing.Point(409, 285);
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.Size = new System.Drawing.Size(140, 33);
            this.DoneButton.TabIndex = 1;
            this.DoneButton.Text = "Done";
            this.DoneButton.UseVisualStyleBackColor = true;
            this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
            // 
            // Automation_Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(551, 326);
            this.Controls.Add(this.DoneButton);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Automation_Settings";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Automation Settings";
            this.Load += new System.EventHandler(this.Automation_Settings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button DoneButton;
        private System.Windows.Forms.CheckBox Base64CheckBox;
        private System.Windows.Forms.RichTextBox DecryptionKeyTextBox;
        private System.Windows.Forms.CheckBox EncryptFilesCheckBox;
        private System.Windows.Forms.Label label1;
    }
}