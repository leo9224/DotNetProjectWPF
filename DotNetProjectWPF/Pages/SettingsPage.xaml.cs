using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using DotNetProjectLibrary.Models;
using Newtonsoft.Json;

namespace DotNetProjectWPF.Pages
{
    public partial class SettingsPage : Page
    {
        Frame MainFrame;
		string Token;

        public SettingsPage(Frame mainFrame, string token)
        {
            MainFrame = mainFrame;
			Token = token;

            MainFrame.Navigate(new AuthenticationPage(MainFrame));

            InitializeComponent();

            ApiUrl.Text = Config.ApiUrl;
            RoomIdOfTheMachine.Text = Config.RoomIdOfTheMachine.ToString();
        }

        private void UpdateApiUrlButtonClick(object sender, RoutedEventArgs e)
        {
			UpdateButtonClick(ApiUrl, UpdateApiUrlButton, () => Config.UpdateApiUrl(ApiUrl.Text));
        }

		private void UpdateRoomIdOfTheMachineButtonClick(object sender, RoutedEventArgs e)
		{
			string stringId = RoomIdOfTheMachine.Text;
			int id = 0;

			try
			{
				if (stringId != "")
				{
					id = int.Parse(stringId);
                }
                UpdateButtonClick(RoomIdOfTheMachine, UpdateRoomIdOfTheMachineButton, () => Config.UpdateRoomIdOfTheMachine(id));
                IdNotInt.Visibility = Visibility.Collapsed;
            }
			catch (Exception exception)
			{
				IdNotInt.Visibility = Visibility.Visible;
			}
		}

		private void UpdateButtonClick(TextBox textBox, Button button, Action updateValueFunction)
        {
			if (!textBox.IsEnabled)
			{
				textBox.IsEnabled = true;
				button.Content = "Save";
                SaveButton.IsEnabled = false;
            }
			else
			{
				textBox.IsEnabled = false;
				button.Content = "Update";
				updateValueFunction();
                SaveButton.IsEnabled = true;
            }
		}

		private async void SaveButtonClick(object sender, RoutedEventArgs e)
		{
			if (Config.ApiUrl != null && Config.RoomIdOfTheMachine != null)
			{
				HttpClient httpClient = new();
                httpClient.DefaultRequestHeaders.Add("Authorization", Token);

                Computer computer = new Computer { name = Utils.GetMachineName(), os=Utils.GetOsPlatform(), os_version=Utils.GetOsVersion(), room_id=Config.RoomIdOfTheMachine, status="ON"};

				HttpContent content = new StringContent(JsonConvert.SerializeObject(computer), Encoding.UTF8, MediaTypeHeaderValue.Parse("application/json"));
				try
				{
                    HttpResponseMessage httpResponse = await httpClient.PostAsync($"{Config.ApiUrl}/api/computer", content);
                    MainFrame.Navigate(new MainPage(MainFrame));
                }
                catch
				{

				}
			}
			else
			{
				NullValue.Visibility = Visibility.Visible;
			}
		}
	}
}
