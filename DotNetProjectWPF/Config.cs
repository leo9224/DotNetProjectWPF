using System.IO;
using System.Text.Json.Nodes;

namespace DotNetProjectWPF
{
    class Config
    {
		private static string configFilePath = "./config.json";

		public static string? ApiUrl = GetString("api_url");
		public static bool IsFirstLaunch = GetBoolean("is_first_launch");
		public static int? RoomIdOfTheMachine = GetInt("room_id_of_the_machine");

		public static void UpdateApiUrl(string value)
		{
			ApiUrl = value;
		}

		public static void UpdateRoomIdOfTheMachine(int value)
		{
			RoomIdOfTheMachine = value;
		}

		private static JsonNode? ReadJsonFile(string filePath)
		{
			string jsonString = File.ReadAllText(filePath);

			try
			{
				return JsonNode.Parse(jsonString);
			}
			catch
			{
				return null;
			}
		}

		private static string? GetString(string key)
		{
			return ReadJsonFile(configFilePath)?[key]?.ToString();
		}

		private static int? GetInt(string key)
		{
			string? stringId = GetString(key);

			if (stringId == null)
			{
				return null;
			}
			else
			{
				return int.Parse(stringId);
			}
		}

		private static bool GetBoolean(string key)
		{
			JsonNode? jsonValue = ReadJsonFile(configFilePath)?[key];

			if (jsonValue != null)
			{
				try
				{
					bool value = (bool)jsonValue;
					return value;
				}
				catch
				{
					return true;
				}
			}
			else
			{
				return true;
			}
		}
	}
}
