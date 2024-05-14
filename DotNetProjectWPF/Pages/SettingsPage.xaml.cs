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

        public SettingsPage(Frame mainFrame)
        {
            MainFrame = mainFrame;

            MainFrame.Navigate(new AuthenticationPage(MainFrame, this));

            InitializeComponent();

            ApiUrl.Text = Config.ApiUrl;
            RoomIdOfTheMachine.Text = Config.RoomIdOfTheMachine.ToString();
        }

        private void UpdateApiUrlButtonClick(object sender, RoutedEventArgs e)
        {
            if (!ApiUrl.IsEnabled)
            {
				ApiUrl.IsEnabled = true;
                UpdateApiUrlButton.Content = "Save";
            }
            else
            {
				ApiUrl.IsEnabled = false;
				UpdateApiUrlButton.Content = "Update";
                Config.UpdateApiUrl(ApiUrl.Text);
            }
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
			}
			else
			{
				textBox.IsEnabled = false;
				button.Content = "Update";
				updateValueFunction();
			}
		}

		private async void SaveButtonClick(object sender, RoutedEventArgs e)
		{
			if (Config.ApiUrl != null && Config.RoomIdOfTheMachine != null)
			{
				try
				{
					MainFrame.GoBack();
				}
				catch
				{
					MainFrame.Navigate(new MainPage(MainFrame));
				}
				finally
				{
					HttpClient HttpClient = new();

					Computer computer = new Computer { name = Utils.GetMachineName(), os=Utils.GetOsPlatform(), os_version=Utils.GetOsVersion(), room_id=Config.RoomIdOfTheMachine, status="ON"};

					try
					{
						HttpContent content = new StringContent(JsonConvert.SerializeObject(computer), Encoding.UTF8, MediaTypeHeaderValue.Parse("application/json"));
						HttpResponseMessage httpResponse = await HttpClient.PostAsync($"{Config.ApiUrl}/api/computer", content);
						if (httpResponse.IsSuccessStatusCode)
						{
							
						}
						else
						{
							
						}
					}
					catch (Exception exception)
					{

					}
				}
			}
			else
			{
				NullValue.Visibility = Visibility.Visible;
			}
		}
	}
}
