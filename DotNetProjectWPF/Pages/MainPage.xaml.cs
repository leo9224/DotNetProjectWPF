using System.Windows;
using System.Windows.Controls;

namespace DotNetProjectWPF.Pages
{
    public partial class MainPage : Page
    {
        Frame MainFrame;

        public MainPage(Frame mainFrame)
        {
            MainFrame = mainFrame;

            InitializeComponent();

            DeviceName.Text = $"Name: {Utils.GetMachineName()}";
            MacAddress.Text = $"MAC Address: {Utils.GetMacAddress()}";
            IpAddress.Text = $"IP Address: {Utils.GetIpAddress()}";
            OsPlatform.Text = $"OS: {Utils.GetOsPlatform()}";
            OsVersion.Text = $"Version: {Utils.GetOsVersion()}";
        }

		private void SettingsButtonClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SettingsPage(MainFrame));
        }

        private void FAQButtonClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new FAQPage(MainFrame));
        }
    }
}
