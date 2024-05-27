using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using DotNetProjectLibrary.Authentication;
using Newtonsoft.Json.Linq;

namespace DotNetProjectWPF.Pages
{
    public partial class AuthenticationPage : Page
    {
        private Frame MainFrame;

        public AuthenticationPage(Frame mainFrame)
        {
            MainFrame = mainFrame;

            InitializeComponent();
        }

        private async void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new();

            try
            {
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"{Config.ApiUrl}/api/authentication/{EmailValue.Text}/{PasswordValue.Password}");
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringResponse = await httpResponseMessage.Content.ReadAsStringAsync();
                    JObject jsonResponse = JObject.Parse(stringResponse);

                    string token = $"Bearer {jsonResponse.GetValue("token")}";
                    int userId = jsonResponse.GetValue("user_id")!.ToObject<int>();

                    token = $"Bearer {token}";

                    MainFrame.Navigate(new SettingsPage(MainFrame, token));
                }
                else
                {
                    InvalidCredentialsText.Visibility = Visibility.Visible;
                }
            }
            catch (Exception exception)
            {

            }
        }

        private void CheckValidEmailAndPassword()
        {
            if (Authentication.CheckValidEmail(EmailValue.Text) && Authentication.CheckValidPassword(EmailValue.Text))
            {
                LoginButton.IsEnabled = true;
            }
            else
            {
                LoginButton.IsEnabled = false;
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
            CheckValidEmailAndPassword();
        }

        private void PasswordTextChange(object sender, RoutedEventArgs e)
        {
            CheckValidEmailAndPassword();
        }
    }
}
