namespace Main_Tools
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
            this.ErrorMessageLabel = new System.Windows.Forms.Label();
            this.ReasonLabel = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.IPLabel = new System.Windows.Forms.Label();
            this.OpenRouterSettingsLinkLabel = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // ErrorMessageLabel
            // 
            this.ErrorMessageLabel.AutoSize = true;
            this.ErrorMessageLabel.Font = new System.Drawing.Font("Yu Gothic UI", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorMessageLabel.Location = new System.Drawing.Point(168, 9);
            this.ErrorMessageLabel.Name = "ErrorMessageLabel";
            this.ErrorMessageLabel.Size = new System.Drawing.Size(553, 54);
            this.ErrorMessageLabel.TabIndex = 0;
            this.ErrorMessageLabel.Text = "Internet May Not Be Available";
            // 
            // ReasonLabel
            // 
            this.ReasonLabel.AutoSize = true;
            this.ReasonLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReasonLabel.Location = new System.Drawing.Point(15, 110);
            this.ReasonLabel.Name = "ReasonLabel";
            this.ReasonLabel.Size = new System.Drawing.Size(85, 25);
            this.ReasonLabel.TabIndex = 1;
            this.ReasonLabel.Text = "Reason:";
            // 
            // CloseButton
            // 
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Yi Baiti", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.Location = new System.Drawing.Point(654, 417);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(219, 50);
            this.CloseButton.TabIndex = 2;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 423);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(547, 31);
            this.label1.TabIndex = 3;
            this.label1.Text = "Rogers Support Number: 1 855 381-7839";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(651, 380);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(222, 34);
            this.label2.TabIndex = 4;
            this.label2.Text = "Click button below to close popup:\r\n(Ctrl + H also closes the popup)";
            // 
            // IPLabel
            // 
            this.IPLabel.AutoSize = true;
            this.IPLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IPLabel.Location = new System.Drawing.Point(17, 397);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(130, 17);
            this.IPLabel.TabIndex = 5;
            this.IPLabel.Text = "Your IP Address:";
            // 
            // OpenRouterSettingsLinkLabel
            // 
            this.OpenRouterSettingsLinkLabel.AutoSize = true;
            this.OpenRouterSettingsLinkLabel.Location = new System.Drawing.Point(22, 459);
            this.OpenRouterSettingsLinkLabel.Name = "OpenRouterSettingsLinkLabel";
            this.OpenRouterSettingsLinkLabel.Size = new System.Drawing.Size(109, 13);
            this.OpenRouterSettingsLinkLabel.TabIndex = 6;
            this.OpenRouterSettingsLinkLabel.TabStop = true;
            this.OpenRouterSettingsLinkLabel.Text = "Open Router Settings";
            this.OpenRouterSettingsLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OpenRouterSettingsLinkLabel_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(137, 459);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(87, 13);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Program Settings";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(885, 479);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.OpenRouterSettingsLinkLabel);
            this.Controls.Add(this.IPLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.ReasonLabel);
            this.Controls.Add(this.ErrorMessageLabel);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ErrorMessageLabel;
        private System.Windows.Forms.Label ReasonLabel;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label IPLabel;
        private System.Windows.Forms.LinkLabel OpenRouterSettingsLinkLabel;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

