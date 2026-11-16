using System.Collections.Generic;

namespace Tracking
{
	public class TempBitLife1asPreferredLanguageConfirmed : IGameTelemetryEvent
	{
		public const string AppStart = "app_start";

		public const string Settings = "settings";

		private readonly string _languageSelected;

		private readonly string _spawnLogic;

		public TempBitLife1asPreferredLanguageConfirmed(string languageCode, string spawnLogic)
		{
		}

		public Dictionary<string, string> GetData()
		{
			return null;
		}

		public Dictionary<string, object> GetDataCleverTap()
		{
			return null;
		}

		private static string GetPrimaryIso639Code(string languageCode)
		{
			return null;
		}
	}
}
