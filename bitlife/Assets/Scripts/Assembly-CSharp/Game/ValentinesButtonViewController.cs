using Backend.Authentication;

namespace Game
{
	public class ValentinesButtonViewController
	{
		private const ACTIVE_BUTTON_TYPE ActiveButtonType = ACTIVE_BUTTON_TYPE.VALENTINES_INFO;

		private static bool _isInitialized;

		public static int ClicksAllowed => 0;

		public static string PrefabName => null;

		public static void Init()
		{
		}

		private static void GameSettingsAvailable(GetConfigResponse gameSettings)
		{
		}

		private static void CheckActiveButton()
		{
		}

		private static bool Condition()
		{
			return false;
		}

		private static bool TryDownload()
		{
			return false;
		}
	}
}
