using Backend.Data;

namespace Backend.Service
{
	public class SettingsService
	{
		public static string ResourceString;

		private readonly SettingsData _settingsData;

		private readonly ConfigDataModel _config;

		public ConfigDataModel DataModel => null;

		public string Password => null;

		public string Url => null;

		public string UrlData => null;

		public string UrlReceipt => null;

		public string UrlToken => null;

		public string UrlMyStuff => null;

		public string RefreshToken => null;

		public string ShopUrl => null;

		public string SeasonsUrl => null;

		public SettingsService(string data, ConfigDataModel config)
		{
		}
	}
}
