using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Ontario_COVID_19_Audio_Version;

namespace AudioMerger
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
