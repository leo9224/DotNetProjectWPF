using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using DotNetProjectLibrary.Authentication;

namespace DotNetProjectWPF.Pages
{
    public partial class AuthenticationPage : Page
    {
        private Frame MainFrame;
        private Page NavigateTo;

        public AuthenticationPage(Frame mainFrame, Page navigateTo)
        {
            MainFrame = mainFrame;
            NavigateTo = navigateTo;

            InitializeComponent();
        }

        private async void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            HttpClient HttpClient = new();

            try
            {
                HttpResponseMessage httpResponse = await HttpClient.GetAsync($"{Config.ApiUrl}/api/authentication/{EmailValue.Text}/{PasswordValue.Password}");
                if (httpResponse.IsSuccessStatusCode)
                {
                    string token = await httpResponse.Content.ReadAsStringAsync();
                    token = $"Bearer {token}";

                    MainFrame.Navigate(NavigateTo);
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
            MainFrame.GoBack();
            
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
