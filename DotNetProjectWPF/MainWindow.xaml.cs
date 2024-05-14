using System.ComponentModel;
using System.Windows;
using DotNetProjectWPF.Pages;

namespace DotNetProjectWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Closing += MainWindowClosing;

            if (Config.IsFirstLaunch)
            {
				MainFrame.Navigate(new SettingsPage(MainFrame));
                Config.IsFirstLaunch = false;
			}
            else
            {
				Hide();
				MainFrame.Navigate(new MainPage(MainFrame));
			}
        }

        private void MainWindowClosing(object? sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void OpenNotifyIconMenuItemClick(object sender, RoutedEventArgs e)
        {
            Show();
        }

        private void ExitNotifyIconMenuItemClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}