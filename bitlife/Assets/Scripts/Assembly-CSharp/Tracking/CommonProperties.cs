using System.Collections.Generic;

namespace Tracking
{
	public static class CommonProperties
	{
		private static string AppsFlyerID => null;

		private static string UserExternalAccountConnection => null;

		private static string BitLifeUserID => null;

		private static string DeviceID => null;

		private static string Build => null;

		private static string LivesCompleted => null;

		private static string IsProductionEnvironment => null;

		private static string Platform => null;

		private static string Country => null;

		private static string SessionGUID => null;

		private static string EventGUID => null;

		private static string DeviceLanguage => null;

		private static string AppLanguage => null;

		private static string CurrentLifeAge => null;

		private static string CurrentLifeIsFamous => null;

		private static string YearsFamous => null;

		private static string CurrentLifeIsRoyal => null;

		private static string CurrentLifeIsInPrison => null;

		private static string CurrentLifeIsVampire => null;

		private static string CurrentLifeIsMovieDirector => null;

		private static string NavigationLayerA => null;

		private static string NavigationLayerB => null;

		private static string NavigationLayerC => null;

		private static bool SeasonActive => false;

		private static string Streak => null;

		private static string SeasonName => null;

		private static string SeasonDay => null;

		private static string IsBitpassBuyer => null;

		private static string IsBitizenBuyer => null;

		private static string IsBossModeBuyer => null;

		private static string CurrentLifeIsUltimateFighter => null;

		private static string GetCurrentLifeSpecialLifeID()
		{
			return null;
		}

		public static Dictionary<string, string> GetData()
		{
			return null;
		}

		public static Dictionary<string, string> GetData(IGameTelemetryEvent gameTelemetryEvent)
		{
			return null;
		}
	}
}
