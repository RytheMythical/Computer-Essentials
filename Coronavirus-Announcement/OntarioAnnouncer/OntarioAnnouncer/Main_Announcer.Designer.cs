namespace OntarioAnnouncer
{
    partial class Main_Announcer
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
            this.CloseButton = new System.Windows.Forms.Button();
            this.StatsListBox = new System.Windows.Forms.ListBox();
            this.DemographicsCasesListBox = new System.Windows.Forms.ListBox();
            this.TodaysCasesLabel = new System.Windows.Forms.Label();
            this.EstimateLabel = new System.Windows.Forms.Label();
            this.SevenDayAverage = new System.Windows.Forms.Label();
            this.NewsTextBox = new System.Windows.Forms.RichTextBox();
            this.PositiveRateLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.LongTermCareStatusList = new System.Windows.Forms.ListBox();
            this.DeathLabel = new System.Windows.Forms.Label();
            this.UKVariantLabel = new System.Windows.Forms.Label();
            this.VariantsListBox = new System.Windows.Forms.ListBox();
            this.HospitalInformationListBox = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.VaccineEligibilityLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(207, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(767, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "NEW COVID-19 INFORMATION IN ONTARIO!";
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(1149, 1);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(117, 34);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // StatsListBox
            // 
            this.StatsListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.StatsListBox.FormattingEnabled = true;
            this.StatsListBox.ItemHeight = 16;
            this.StatsListBox.Location = new System.Drawing.Point(12, 73);
            this.StatsListBox.Name = "StatsListBox";
            this.StatsListBox.Size = new System.Drawing.Size(393, 164);
            this.StatsListBox.TabIndex = 2;
            // 
            // DemographicsCasesListBox
            // 
            this.DemographicsCasesListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.DemographicsCasesListBox.FormattingEnabled = true;
            this.DemographicsCasesListBox.ItemHeight = 16;
            this.DemographicsCasesListBox.Location = new System.Drawing.Point(420, 73);
            this.DemographicsCasesListBox.Name = "DemographicsCasesListBox";
            this.DemographicsCasesListBox.Size = new System.Drawing.Size(301, 164);
            this.DemographicsCasesListBox.TabIndex = 3;
            // 
            // TodaysCasesLabel
            // 
            this.TodaysCasesLabel.AutoSize = true;
            this.TodaysCasesLabel.BackColor = System.Drawing.Color.Yellow;
            this.TodaysCasesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F);
            this.TodaysCasesLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TodaysCasesLabel.Location = new System.Drawing.Point(13, 271);
            this.TodaysCasesLabel.Name = "TodaysCasesLabel";
            this.TodaysCasesLabel.Size = new System.Drawing.Size(671, 63);
            this.TodaysCasesLabel.TabIndex = 4;
            this.TodaysCasesLabel.Text = "Todays Cases: => Loading";
            // 
            // EstimateLabel
            // 
            this.EstimateLabel.AutoSize = true;
            this.EstimateLabel.BackColor = System.Drawing.Color.Aqua;
            this.EstimateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.EstimateLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.EstimateLabel.Location = new System.Drawing.Point(12, 337);
            this.EstimateLabel.Name = "EstimateLabel";
            this.EstimateLabel.Size = new System.Drawing.Size(445, 39);
            this.EstimateLabel.TabIndex = 6;
            this.EstimateLabel.Text = "Estimated Cases Tomorrow:";
            // 
            // SevenDayAverage
            // 
            this.SevenDayAverage.AutoSize = true;
            this.SevenDayAverage.BackColor = System.Drawing.Color.Fuchsia;
            this.SevenDayAverage.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.SevenDayAverage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SevenDayAverage.Location = new System.Drawing.Point(12, 379);
            this.SevenDayAverage.Name = "SevenDayAverage";
            this.SevenDayAverage.Size = new System.Drawing.Size(329, 39);
            this.SevenDayAverage.TabIndex = 7;
            this.SevenDayAverage.Text = "Seven Day Average:";
            // 
            // NewsTextBox
            // 
            this.NewsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.NewsTextBox.Location = new System.Drawing.Point(447, 498);
            this.NewsTextBox.Name = "NewsTextBox";
            this.NewsTextBox.Size = new System.Drawing.Size(275, 114);
            this.NewsTextBox.TabIndex = 8;
            this.NewsTextBox.Text = "";
            // 
            // PositiveRateLabel
            // 
            this.PositiveRateLabel.AutoSize = true;
            this.PositiveRateLabel.BackColor = System.Drawing.Color.Black;
            this.PositiveRateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.PositiveRateLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.PositiveRateLabel.Location = new System.Drawing.Point(12, 542);
            this.PositiveRateLabel.Name = "PositiveRateLabel";
            this.PositiveRateLabel.Size = new System.Drawing.Size(227, 39);
            this.PositiveRateLabel.TabIndex = 9;
            this.PositiveRateLabel.Text = "Positive Rate:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(9, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(281, 31);
            this.label2.TabIndex = 10;
            this.label2.Text = "General Information:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(414, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(308, 31);
            this.label3.TabIndex = 11;
            this.label3.Text = "Cases by gender, age:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(9, 238);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(307, 31);
            this.label4.TabIndex = 12;
            this.label4.Text = "Information of the day:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(484, 464);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(200, 31);
            this.label5.TabIndex = 13;
            this.label5.Text = "Special News:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(857, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(325, 31);
            this.label6.TabIndex = 14;
            this.label6.Text = "Long Term Care Status:";
            // 
            // LongTermCareStatusList
            // 
            this.LongTermCareStatusList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.LongTermCareStatusList.FormattingEnabled = true;
            this.LongTermCareStatusList.ItemHeight = 16;
            this.LongTermCareStatusList.Location = new System.Drawing.Point(773, 73);
            this.LongTermCareStatusList.Name = "LongTermCareStatusList";
            this.LongTermCareStatusList.Size = new System.Drawing.Size(552, 164);
            this.LongTermCareStatusList.TabIndex = 15;
            // 
            // DeathLabel
            // 
            this.DeathLabel.AutoSize = true;
            this.DeathLabel.BackColor = System.Drawing.Color.Red;
            this.DeathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.DeathLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.DeathLabel.Location = new System.Drawing.Point(12, 582);
            this.DeathLabel.Name = "DeathLabel";
            this.DeathLabel.Size = new System.Drawing.Size(212, 39);
            this.DeathLabel.TabIndex = 21;
            this.DeathLabel.Text = "New Deaths:";
            // 
            // UKVariantLabel
            // 
            this.UKVariantLabel.AutoSize = true;
            this.UKVariantLabel.BackColor = System.Drawing.Color.Crimson;
            this.UKVariantLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.UKVariantLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.UKVariantLabel.Location = new System.Drawing.Point(12, 418);
            this.UKVariantLabel.Name = "UKVariantLabel";
            this.UKVariantLabel.Size = new System.Drawing.Size(151, 39);
            this.UKVariantLabel.TabIndex = 22;
            this.UKVariantLabel.Text = "Variants:";
            // 
            // VariantsListBox
            // 
            this.VariantsListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.VariantsListBox.FormattingEnabled = true;
            this.VariantsListBox.ItemHeight = 16;
            this.VariantsListBox.Location = new System.Drawing.Point(14, 465);
            this.VariantsListBox.Name = "VariantsListBox";
            this.VariantsListBox.Size = new System.Drawing.Size(224, 68);
            this.VariantsListBox.TabIndex = 23;
            // 
            // HospitalInformationListBox
            // 
            this.HospitalInformationListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.HospitalInformationListBox.FormattingEnabled = true;
            this.HospitalInformationListBox.ItemHeight = 16;
            this.HospitalInformationListBox.Location = new System.Drawing.Point(773, 274);
            this.HospitalInformationListBox.Name = "HospitalInformationListBox";
            this.HospitalInformationListBox.Size = new System.Drawing.Size(552, 164);
            this.HospitalInformationListBox.TabIndex = 24;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(767, 240);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(285, 31);
            this.label8.TabIndex = 25;
            this.label8.Text = "Hospital Information:";
            // 
            // VaccineEligibilityLabel
            // 
            this.VaccineEligibilityLabel.AutoSize = true;
            this.VaccineEligibilityLabel.BackColor = System.Drawing.Color.Red;
            this.VaccineEligibilityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F);
            this.VaccineEligibilityLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.VaccineEligibilityLabel.Location = new System.Drawing.Point(772, 441);
            this.VaccineEligibilityLabel.Name = "VaccineEligibilityLabel";
            this.VaccineEligibilityLabel.Size = new System.Drawing.Size(299, 29);
            this.VaccineEligibilityLabel.TabIndex = 26;
            this.VaccineEligibilityLabel.Text = "Vaccine Eligibility For L3R:";
            // 
            // Main_Announcer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(1337, 620);
            this.Controls.Add(this.VaccineEligibilityLabel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.HospitalInformationListBox);
            this.Controls.Add(this.VariantsListBox);
            this.Controls.Add(this.UKVariantLabel);
            this.Controls.Add(this.DeathLabel);
            this.Controls.Add(this.LongTermCareStatusList);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PositiveRateLabel);
            this.Controls.Add(this.NewsTextBox);
            this.Controls.Add(this.SevenDayAverage);
            this.Controls.Add(this.EstimateLabel);
            this.Controls.Add(this.TodaysCasesLabel);
            this.Controls.Add(this.DemographicsCasesListBox);
            this.Controls.Add(this.StatsListBox);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "Main_Announcer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Coronavirus Cases";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.ListBox StatsListBox;
        private System.Windows.Forms.ListBox DemographicsCasesListBox;
        private System.Windows.Forms.Label TodaysCasesLabel;
        private System.Windows.Forms.Label EstimateLabel;
        private System.Windows.Forms.Label SevenDayAverage;
        private System.Windows.Forms.RichTextBox NewsTextBox;
        private System.Windows.Forms.Label PositiveRateLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox LongTermCareStatusList;
        private System.Windows.Forms.Label DeathLabel;
        private System.Windows.Forms.Label UKVariantLabel;
        private System.Windows.Forms.ListBox VariantsListBox;
        private System.Windows.Forms.ListBox HospitalInformationListBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label VaccineEligibilityLabel;
    }
}

