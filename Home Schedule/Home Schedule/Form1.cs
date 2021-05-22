using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Home_Schedule
{
    public partial class Form1 : Form
    {
        bool Closing = false;
        public Form1()
        {
            InitializeComponent();
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            FormClosing += (sender, args) =>
            {
                if (Closing == false)
                {
                    args.Cancel = true;
                }
            };
        }

        public List<DateTime> StoredDateList = new List<DateTime>();

        /// <summary>
        /// False if the date is not stored
        /// True if the date is stored
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool DateCheck(DateTime date)
        {
            bool Return = false;
            if (StoredDateList.Contains(date))
            {
                Return = true;
            }
            else
            {
                StoredDateList.Add(date);
            }
            return Return;
        }

        public bool DateCheckNow()
        {
            return DateCheck(DateTime.Now);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            TopMost = true;
            Visible = false;
            ShowInTaskbar = false;
        }
    }
}
