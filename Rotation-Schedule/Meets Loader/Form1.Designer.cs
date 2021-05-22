
namespace Meets_Loader
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
            this.webControl1 = new EO.WinForm.WebControl();
            this.webView1 = new EO.WebBrowser.WebView();
            this.MuteAlarmCheckBox = new System.Windows.Forms.CheckBox();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.TurnOnCaptionsLabel = new System.Windows.Forms.Label();
            this.RaiseVolumeCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // webControl1
            // 
            this.webControl1.BackColor = System.Drawing.Color.White;
            this.webControl1.Location = new System.Drawing.Point(5, 7);
            this.webControl1.Name = "webControl1";
            this.webControl1.Size = new System.Drawing.Size(1455, 584);
            this.webControl1.TabIndex = 0;
            this.webControl1.Text = "webControl1";
            this.webControl1.WebView = this.webView1;
            // 
            // webView1
            // 
            this.webView1.InputMsgFilter = null;
            this.webView1.ObjectForScripting = null;
            this.webView1.Title = null;
            // 
            // MuteAlarmCheckBox
            // 
            this.MuteAlarmCheckBox.AutoSize = true;
            this.MuteAlarmCheckBox.Location = new System.Drawing.Point(1380, 616);
            this.MuteAlarmCheckBox.Name = "MuteAlarmCheckBox";
            this.MuteAlarmCheckBox.Size = new System.Drawing.Size(79, 17);
            this.MuteAlarmCheckBox.TabIndex = 1;
            this.MuteAlarmCheckBox.Text = "Mute Alarm";
            this.MuteAlarmCheckBox.UseVisualStyleBackColor = true;
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(1177, 610);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(183, 26);
            this.RefreshButton.TabIndex = 2;
            this.RefreshButton.Text = "Rejoin Meeting";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.BackColor = System.Drawing.Color.Yellow;
            this.StatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.StatusLabel.Location = new System.Drawing.Point(763, 611);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(64, 25);
            this.StatusLabel.TabIndex = 3;
            this.StatusLabel.Text = "label1";
            // 
            // TurnOnCaptionsLabel
            // 
            this.TurnOnCaptionsLabel.AutoSize = true;
            this.TurnOnCaptionsLabel.BackColor = System.Drawing.Color.Red;
            this.TurnOnCaptionsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 100F);
            this.TurnOnCaptionsLabel.Location = new System.Drawing.Point(28, 184);
            this.TurnOnCaptionsLabel.Name = "TurnOnCaptionsLabel";
            this.TurnOnCaptionsLabel.Size = new System.Drawing.Size(1395, 153);
            this.TurnOnCaptionsLabel.TabIndex = 4;
            this.TurnOnCaptionsLabel.Text = "TURN ON CAPTIONS";
            this.TurnOnCaptionsLabel.Click += new System.EventHandler(this.TurnOnCaptionsLabel_Click);
            // 
            // RaiseVolumeCheckBox
            // 
            this.RaiseVolumeCheckBox.AutoSize = true;
            this.RaiseVolumeCheckBox.Checked = true;
            this.RaiseVolumeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RaiseVolumeCheckBox.Location = new System.Drawing.Point(1380, 597);
            this.RaiseVolumeCheckBox.Name = "RaiseVolumeCheckBox";
            this.RaiseVolumeCheckBox.Size = new System.Drawing.Size(120, 17);
            this.RaiseVolumeCheckBox.TabIndex = 5;
            this.RaiseVolumeCheckBox.Text = "Raise Alarm Volume";
            this.RaiseVolumeCheckBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1471, 645);
            this.Controls.Add(this.RaiseVolumeCheckBox);
            this.Controls.Add(this.TurnOnCaptionsLabel);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.MuteAlarmCheckBox);
            this.Controls.Add(this.webControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Google Meets";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EO.WinForm.WebControl webControl1;
        private EO.WebBrowser.WebView webView1;
        private System.Windows.Forms.CheckBox MuteAlarmCheckBox;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Label TurnOnCaptionsLabel;
        private System.Windows.Forms.CheckBox RaiseVolumeCheckBox;
    }
}

