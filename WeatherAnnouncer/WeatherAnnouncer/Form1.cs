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
using WeatherAnnouncer.Properties;

namespace WeatherAnnouncer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;
            SoundPlayer First = new SoundPlayer(Resources.Markham);
            First.PlaySync();
            SoundPlayer Seconds = new SoundPlayer(Resources.AskWeather);
            Seconds.PlaySync();
            Application.Exit();
        }
    }
}
