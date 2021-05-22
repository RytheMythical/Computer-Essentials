
namespace GitHub_Large_Uploader
{
    partial class GitHub_Tools
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
            this.label1 = new System.Windows.Forms.Label();
            this.PersonalAccessTokenTextBox = new System.Windows.Forms.TextBox();
            this.PersonalAccessLoginButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.NewRepositoryNameTextBox = new System.Windows.Forms.TextBox();
            this.CreateRepositoryButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.GenerateRepositoryButton = new System.Windows.Forms.Button();
            this.LogoutButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(184, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(304, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Paste personal access token key:";
            // 
            // PersonalAccessTokenTextBox
            // 
            this.PersonalAccessTokenTextBox.Location = new System.Drawing.Point(18, 34);
            this.PersonalAccessTokenTextBox.Name = "PersonalAccessTokenTextBox";
            this.PersonalAccessTokenTextBox.Size = new System.Drawing.Size(595, 30);
            this.PersonalAccessTokenTextBox.TabIndex = 1;
            // 
            // PersonalAccessLoginButton
            // 
            this.PersonalAccessLoginButton.Location = new System.Drawing.Point(613, 24);
            this.PersonalAccessLoginButton.Name = "PersonalAccessLoginButton";
            this.PersonalAccessLoginButton.Size = new System.Drawing.Size(113, 40);
            this.PersonalAccessLoginButton.TabIndex = 2;
            this.PersonalAccessLoginButton.Text = "Login";
            this.PersonalAccessLoginButton.UseVisualStyleBackColor = true;
            this.PersonalAccessLoginButton.Click += new System.EventHandler(this.PersonalAccessLoginButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(225, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(211, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "New Repository Name:";
            // 
            // NewRepositoryNameTextBox
            // 
            this.NewRepositoryNameTextBox.Location = new System.Drawing.Point(18, 102);
            this.NewRepositoryNameTextBox.Name = "NewRepositoryNameTextBox";
            this.NewRepositoryNameTextBox.Size = new System.Drawing.Size(595, 30);
            this.NewRepositoryNameTextBox.TabIndex = 4;
            // 
            // CreateRepositoryButton
            // 
            this.CreateRepositoryButton.Location = new System.Drawing.Point(613, 92);
            this.CreateRepositoryButton.Name = "CreateRepositoryButton";
            this.CreateRepositoryButton.Size = new System.Drawing.Size(113, 40);
            this.CreateRepositoryButton.TabIndex = 5;
            this.CreateRepositoryButton.Text = "Create";
            this.CreateRepositoryButton.UseVisualStyleBackColor = true;
            this.CreateRepositoryButton.Click += new System.EventHandler(this.CreateRepositoryButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(20, 140);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(593, 125);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.GenerateRepositoryButton);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(585, 87);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(630, 228);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 37);
            this.button1.TabIndex = 7;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GenerateRepositoryButton
            // 
            this.GenerateRepositoryButton.Location = new System.Drawing.Point(8, 4);
            this.GenerateRepositoryButton.Name = "GenerateRepositoryButton";
            this.GenerateRepositoryButton.Size = new System.Drawing.Size(241, 38);
            this.GenerateRepositoryButton.TabIndex = 0;
            this.GenerateRepositoryButton.Text = "Generate new repository";
            this.GenerateRepositoryButton.UseVisualStyleBackColor = true;
            this.GenerateRepositoryButton.Click += new System.EventHandler(this.GenerateRepositoryButton_Click);
            // 
            // LogoutButton
            // 
            this.LogoutButton.Location = new System.Drawing.Point(630, 178);
            this.LogoutButton.Name = "LogoutButton";
            this.LogoutButton.Size = new System.Drawing.Size(95, 37);
            this.LogoutButton.TabIndex = 8;
            this.LogoutButton.Text = "Logout";
            this.LogoutButton.UseVisualStyleBackColor = true;
            this.LogoutButton.Click += new System.EventHandler(this.LogoutButton_Click);
            // 
            // GitHub_Tools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(738, 269);
            this.Controls.Add(this.LogoutButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.CreateRepositoryButton);
            this.Controls.Add(this.NewRepositoryNameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PersonalAccessLoginButton);
            this.Controls.Add(this.PersonalAccessTokenTextBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "GitHub_Tools";
            this.ShowIcon = false;
            this.Text = "GitHub Tools";
            this.TopMost = true;
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PersonalAccessTokenTextBox;
        private System.Windows.Forms.Button PersonalAccessLoginButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox NewRepositoryNameTextBox;
        private System.Windows.Forms.Button CreateRepositoryButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button GenerateRepositoryButton;
        private System.Windows.Forms.Button LogoutButton;
    }
}