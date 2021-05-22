
namespace Rotation_Schedule
{
    partial class InClass
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.RemainingTimeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 304);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(694, 39);
            this.progressBar1.TabIndex = 0;
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.TitleLabel.Location = new System.Drawing.Point(12, 9);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(334, 46);
            this.TitleLabel.TabIndex = 1;
            this.TitleLabel.Text = "You are in period:";
            // 
            // RemainingTimeLabel
            // 
            this.RemainingTimeLabel.AutoSize = true;
            this.RemainingTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.RemainingTimeLabel.Location = new System.Drawing.Point(12, 242);
            this.RemainingTimeLabel.Name = "RemainingTimeLabel";
            this.RemainingTimeLabel.Size = new System.Drawing.Size(320, 46);
            this.RemainingTimeLabel.TabIndex = 2;
            this.RemainingTimeLabel.Text = "Remaining Time:";
            // 
            // InClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(718, 355);
            this.Controls.Add(this.RemainingTimeLabel);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.progressBar1);
            this.Name = "InClass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InClass";
            this.Load += new System.EventHandler(this.InClass_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label RemainingTimeLabel;
    }
}