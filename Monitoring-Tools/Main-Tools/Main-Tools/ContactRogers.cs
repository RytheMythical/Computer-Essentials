using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Main_Tools.Properties;

namespace Main_Tools
{
    public partial class ContactRogers : Form
    {
        public ContactRogers()
        {
            InitializeComponent();
        }
        private async void PlaySound(Stream location)
        {
            SoundPlayer dew = new SoundPlayer(location);
            dew.Play();
        }
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
        private async Task PlaySoundSync(Stream location)
        {
            SoundPlayer dew = new SoundPlayer(location);
            await Task.Factory.StartNew(() => { dew.PlaySync(); });
        }
        private async void ContactRogers_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            TopMost = true;
            ControlBox = false;
            await PlaySoundSync(Resources.PleaseContact);
            string ISP = InternetCheck.InternetServiceProvider;
            if (ISP.Contains("Carry") || ISP.Contains("carry"))
            {
                await PlaySoundSync(Resources.CarryTelecom);
            }
            else if (ISP.Contains("Rogers") || ISP.Contains("rogers"))
            {
                await PlaySoundSync(Resources.RogersISP);
            }
            else if (ISP.Contains("Fido") || ISP.Contains("fido"))
            {
                await PlaySoundSync(Resources.Fido);
            }
            else if (ISP.Contains("Bell") || ISP.Contains("bell"))
            {
                await PlaySoundSync(Resources.BellCanada);
            }
            else if (ISP.Contains("Virgin Mobile") || ISP.Contains("Virgin") || ISP.Contains("virgin"))
            {
                await PlaySoundSync(Resources.VirginMobile);
            }
            else if (ISP == "")
            {
                await PlaySoundSync(Resources.NotConnected);
            }
            else
            {
                await PlaySoundSync(Resources.ConnectedToVPN);
            }

            await PlaySoundSync(Resources.IfYouStillDoNotHaveInternet);
            for (int i = 10; i > 0; i = i - 1)
            {
                label1.Text = "Closing in:\n" + i + " seconds";
                await Task.Delay(1000);
            }
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
