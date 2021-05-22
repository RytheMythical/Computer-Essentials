using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clipboard_Notifier
{
    public partial class NewImage : Form
    {
        public NewImage()
        {
            InitializeComponent();
        }

        private async void NewImage_Load(object sender, EventArgs e)
        {
            TopMost = true;
            PlaceLowerRight();
            ClipboardPictureBox.Image = Clipboard.GetImage();
            await Task.Delay(3000);
            Close();
        }
        private void PlaceLowerRight()
        {
            //Determine "rightmost" screen
            Screen rightmost = Screen.AllScreens[0];
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.Right > rightmost.WorkingArea.Right)
                    rightmost = screen;
            }

            this.Left = rightmost.WorkingArea.Right - this.Width;
            this.Top = rightmost.WorkingArea.Bottom - this.Height;
        }

    }
}
