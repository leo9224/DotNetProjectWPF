using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using DotNetProjectLibrary.Models;
using DotNetProjectLibrary.Authentication;
using Newtonsoft.Json;

namespace DotNetProjectWPF.Pages
{
    public partial class ContactPage : Page
    {
        private Frame MainFrame;

        private string RoomName;
        private string ComputerName;

        public ContactPage(Frame mainFrame)
        {
            InitializeComponent();

            MainFrame = mainFrame;

            RoomName = "Room name";
            ComputerName = Utils.GetMachineName();

            RoomValue.Text = RoomName;
            ComputerValue.Text = ComputerName;
        }

        private void CheckValidEmailAndMessageAndSite()
        {
            if (Authentication.CheckValidEmail(EmailValue.Text) && !string.IsNullOrWhiteSpace(MessageValue.Text) && SiteValue.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(RoomValue.Text))
            {
                SendButton.IsEnabled = true;
            }
            else
            {
                SendButton.IsEnabled = false;
            }
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                MainFrame.GoBack();
            }
            catch
            {

            }
        }

        private void EmailTextChange(object sender, TextChangedEventArgs e)
        {
            CheckValidEmailAndMessageAndSite();
        }

        private void MessageTextChange(object sender, RoutedEventArgs e)
        {
            CheckValidEmailAndMessageAndSite();
        }

        private async void SendButtonClick(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new();

            Ticket ticket = new Ticket { site_name = SiteValue.Text, room_name = RoomName, computer_name = ComputerName, email = EmailValue.Text, message = MessageValue.Text };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(ticket), Encoding.UTF8, MediaTypeHeaderValue.Parse("application/json"));
            try
            {
                HttpResponseMessage httpResponse = await httpClient.PostAsync($"{Config.ApiUrl}/api/ticket", content);
                MainFrame.Navigate(new MainPage(MainFrame));
            }
            catch
            {

            }
        }

        private void SiteTextChange(object sender, TextChangedEventArgs e)
        {
            CheckValidEmailAndMessageAndSite();
        }

        private void SiteSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            CheckValidEmailAndMessageAndSite();
        }
    }
}
