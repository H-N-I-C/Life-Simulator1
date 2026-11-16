using GameAnalyticsSDK;

namespace CandyBusiness.Managers
{
	public class GameAnalyticsManager
	{
		private static string GA_GAME_KEY;

		private static string GA_GAME_SECRET;

		private static bool initialized;

		public static void StartGameAnalytics(APP_PLATFORM _platform, bool _devMode)
		{
		}

		public static void LogPurchaseEventForProduct(object product)
		{
		}

		public static bool LogFunnelEvent(GAProgressionStatus ga, string evt, string phase)
		{
			return false;
		}
	}
}
