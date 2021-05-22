
namespace Manual_Class_Join
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
            this.ClassList = new System.Windows.Forms.ListBox();
            this.JoinButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.CustomJoin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.CustomMeetLinkBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ClassList
            // 
            this.ClassList.Enabled = false;
            this.ClassList.FormattingEnabled = true;
            this.ClassList.Location = new System.Drawing.Point(12, 12);
            this.ClassList.Name = "ClassList";
            this.ClassList.Size = new System.Drawing.Size(282, 160);
            this.ClassList.TabIndex = 0;
            // 
            // JoinButton
            // 
            this.JoinButton.Enabled = false;
            this.JoinButton.Location = new System.Drawing.Point(12, 198);
            this.JoinButton.Name = "JoinButton";
            this.JoinButton.Size = new System.Drawing.Size(282, 40);
            this.JoinButton.TabIndex = 1;
            this.JoinButton.Text = "Join Class";
            this.JoinButton.UseVisualStyleBackColor = true;
            this.JoinButton.Click += new System.EventHandler(this.JoinButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(21, 242);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(264, 19);
            this.progressBar1.TabIndex = 2;
            // 
            // CustomJoin
            // 
            this.CustomJoin.Enabled = false;
            this.CustomJoin.Location = new System.Drawing.Point(12, 284);
            this.CustomJoin.Name = "CustomJoin";
            this.CustomJoin.Size = new System.Drawing.Size(282, 40);
            this.CustomJoin.TabIndex = 3;
            this.CustomJoin.Text = "Join Class";
            this.CustomJoin.UseVisualStyleBackColor = true;
            this.CustomJoin.Click += new System.EventHandler(this.CustomJoin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 267);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Custom Meet Link:";
            // 
            // CustomMeetLinkBox
            // 
            this.CustomMeetLinkBox.Location = new System.Drawing.Point(107, 263);
            this.CustomMeetLinkBox.Name = "CustomMeetLinkBox";
            this.CustomMeetLinkBox.Size = new System.Drawing.Size(178, 20);
            this.CustomMeetLinkBox.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 331);
            this.Controls.Add(this.CustomMeetLinkBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CustomJoin);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.JoinButton);
            this.Controls.Add(this.ClassList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Manual Class Joiner";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ClassList;
        private System.Windows.Forms.Button JoinButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button CustomJoin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CustomMeetLinkBox;
    }
}

