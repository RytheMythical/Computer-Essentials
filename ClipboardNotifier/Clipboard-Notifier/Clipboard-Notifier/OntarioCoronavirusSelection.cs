using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ontario_Coronavirus_API;
using TeachAssistAPI;

namespace Clipboard_Notifier
{
    public partial class OntarioCoronavirusSelection : Form
    {
        public OntarioCoronavirusSelection()
        {
            InitializeComponent();
            Closing += OntarioCoronavirusSelection_Closing;
        }

        private void OntarioCoronavirusSelection_Closing(object sender, CancelEventArgs e)
        {
            if (NeedToClose == false)
            {
                e.Cancel = true;
            }
        }

        private bool NeedToClose = false;

        private void CloseForm()
        {
            NeedToClose = true;
            Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            Visible = false;
            button2.Enabled = false;
            string SetClipboard = await Ontario_Coronavirus.GetTodaysCases();
            Clipboard.SetText(SetClipboard);
            CloseForm();
        }

        private async Task SetClipboard(string Input, Button button)
        {
            button.Enabled = false;
            Visible = false;
            string SetClipboard = Input;
            Clipboard.SetText(SetClipboard);
            CloseForm();
        }
        private async void button3_Click(object sender, EventArgs e)
        {
            await SetClipboard(await Ontario_Coronavirus.GetTotalDeaths(),button3);
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await SetClipboard(await Ontario_Coronavirus.GetHospitalized(),button4);
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            await SetClipboard(await Ontario_Coronavirus.GetPendingTests(), button5);
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            await SetClipboard(await Ontario_Coronavirus.GetYesterdaysTests(), button6);
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            await SetClipboard(await Ontario_Coronavirus.GetTotalCases(), button7);
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            string SelectedItem = DemographicsComboBox.Text;
            if (SelectedItem == "Male")
            {
                await SetClipboard(await Ontario_Coronavirus.GetCasesByDemographics.Male(), button8);
            }
            else if (SelectedItem == "Female")
            {
                await SetClipboard(await Ontario_Coronavirus.GetCasesByDemographics.Female(), button8);
            }
            else if (SelectedItem == "19 and under")
            {
                await SetClipboard(await Ontario_Coronavirus.GetCasesByDemographics.NineteenAndUnder(), button8);
            }
            else if (SelectedItem == "20-39")
            {
                await SetClipboard(await Ontario_Coronavirus.GetCasesByDemographics.TwentytoThirtyNine(), button8);
            }
            else if (SelectedItem == "40-59")
            {
                await SetClipboard(await Ontario_Coronavirus.GetCasesByDemographics.FourtyToFiftyNine(), button8);
            }
            else if (SelectedItem == "60-79")
            {
                await SetClipboard(await Ontario_Coronavirus.GetCasesByDemographics.SixtyToSeventyNine(), button8);
            }
            else if (SelectedItem == "80 and over")
            {
                await SetClipboard(await Ontario_Coronavirus.GetCasesByDemographics.EightyAndOver(), button8);
            }
            
        }

        private async void button9_Click(object sender, EventArgs e)
        {
            await SetClipboard(await Ontario_Coronavirus.GetRecoveredCases(), button9);
        }

        private async void button10_Click(object sender, EventArgs e)
        {
            await SetClipboard(await Ontario_Coronavirus.GetSevenDayAverage(), button9);
        }

        private async void button11_Click(object sender, EventArgs e)
        {
            try
            {
                await SetClipboard(await Ontario_Coronavirus.GetAverageCasesByRange(Int32.Parse(DaysTextBox.Text)), button11);
            }
            catch (Exception exception)
            {
                
            }
        }
    }
}
