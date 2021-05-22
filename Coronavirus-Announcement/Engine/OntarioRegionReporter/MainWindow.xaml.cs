using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ontario_Color_Coded_Restrictions_API;

namespace OntarioRegionReporter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ResizeMode = ResizeMode.CanMinimize;
            string[] GreenRegions = await OntarioRegions.GetGreenRegions();
            string GreenRegionsString = "";
            foreach (string greenRegion in GreenRegions)
            {
                GreenRegionsString = GreenRegionsString + "\n" + greenRegion;
            }

            GreenRegionTextBlock.Text = GreenRegionsString;
            await ChangeTextBlock(await OntarioRegions.GetYellowRegions(), YellowZoneTextBlock);
            await ChangeTextBlock(await OntarioRegions.GetOrangeRegions(), OrangeZoneTextBlock);
            await ChangeTextBlock(await OntarioRegions.GetRedRegions(), RedZoneTextBlock);
            await ChangeTextBlock(await OntarioRegions.GetLockDownRegions(), LockdownRegionsTextBlock);
        }

        private async Task ChangeTextBlock(string[] Data, TextBlock textblock)
        {
            string DataString = "";
            foreach (string s in Data)
            {
                DataString = DataString + "\n" + s;
            }

            textblock.Text = DataString;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
