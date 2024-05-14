using System.Management;
using System.Net.NetworkInformation;
using System.Net;

namespace DotNetProjectWPF
{
	internal class Utils
	{
		public static string GetMachineName()
		{
			return Environment.MachineName;
		}

		public static string GetMacAddress()
		{
			string macAddress = "";

			foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
			{
				if (networkInterface.OperationalStatus == OperationalStatus.Up)
				{
					macAddress += networkInterface.GetPhysicalAddress().ToString();
					break;
				}
			}
			return macAddress;
		}

		public static string GetIpAddress()
		{
			string hostName = Dns.GetHostName();

			string ipAddress = Dns.GetHostAddresses(hostName)[0].ToString();

			return ipAddress;
		}

		public static string GetOsPlatform()
		{
			using (var searcher = new ManagementObjectSearcher("SELECT Caption, Version FROM Win32_OperatingSystem"))
			{
				foreach (var os in searcher.Get())
				{
					return $"{os["Caption"]}";
				}
			}
			return "Unknown OS";
		}

		public static string GetOsVersion()
		{
			return Environment.OSVersion.Version.ToString();
		}
	}
}
