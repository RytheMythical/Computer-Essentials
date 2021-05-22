
namespace Rotation_Schedule
{
    partial class TransitionFrom
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
            this.TransitioningLabel = new System.Windows.Forms.Label();
            this.TransitionTo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(100, 465);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(877, 45);
            this.progressBar1.TabIndex = 0;
            // 
            // TransitioningLabel
            // 
            this.TransitioningLabel.AutoSize = true;
            this.TransitioningLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.TransitioningLabel.Location = new System.Drawing.Point(41, 221);
            this.TransitioningLabel.Name = "TransitioningLabel";
            this.TransitioningLabel.Size = new System.Drawing.Size(126, 46);
            this.TransitioningLabel.TabIndex = 1;
            this.TransitioningLabel.Text = "label1";
            // 
            // TransitionTo
            // 
            this.TransitionTo.AutoSize = true;
            this.TransitionTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.TransitionTo.Location = new System.Drawing.Point(744, 234);
            this.TransitionTo.Name = "TransitionTo";
            this.TransitionTo.Size = new System.Drawing.Size(126, 46);
            this.TransitionTo.TabIndex = 2;
            this.TransitionTo.Text = "label1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox1.Image = global::Rotation_Schedule.Properties.Resources.png_clipart_right_arrow_simple_rounded_arrow_right_icons_logos_emojis_arrows;
            this.pictureBox1.Location = new System.Drawing.Point(404, 178);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(253, 156);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F);
            this.label1.Location = new System.Drawing.Point(119, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(834, 76);
            this.label1.TabIndex = 4;
            this.label1.Text = "Transitioning to next period";
            // 
            // TransitionFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 545);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.TransitionTo);
            this.Controls.Add(this.TransitioningLabel);
            this.Controls.Add(this.progressBar1);
            this.Name = "TransitionFrom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transition";
            this.Load += new System.EventHandler(this.Transition_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label TransitioningLabel;
        private System.Windows.Forms.Label TransitionTo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
    }
}