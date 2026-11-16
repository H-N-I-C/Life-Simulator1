namespace GameAnalyticsSDK.iOS
{
	public class GameAnalyticsATTClient
	{
		private static readonly GameAnalyticsATTClient Instance__BackingField;

		private static IGameAnalyticsATTListener _attListener;

		public static GameAnalyticsATTClient Instance => Instance__BackingField;

		private GameAnalyticsATTClient()
		{
		}

		private static void GameAnalyticsATTListenerNotDetermined()
		{
		}

		private static void GameAnalyticsATTListenerRestricted()
		{
		}

		private static void GameAnalyticsATTListenerDenied()
		{
		}

		private static void GameAnalyticsATTListenerAuthorized()
		{
		}

		public void RequestTrackingAuthorization(IGameAnalyticsATTListener listener)
		{
		}
	}
}
