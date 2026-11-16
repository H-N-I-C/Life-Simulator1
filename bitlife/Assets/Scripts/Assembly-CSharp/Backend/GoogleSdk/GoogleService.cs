namespace Backend.GoogleSdk
{
	public abstract class GoogleService : GoogleServiceBase
	{
		public static void Login()
		{
		}

		public static GoogleAuthResponse GetAuthResponse()
		{
			return null;
		}

		public static void CancelLoginRequest()
		{
		}

		public static void Logout()
		{
		}

		public static void RefreshUserLogin()
		{
		}

		public static bool ShouldRefresh()
		{
			return false;
		}

		public static void Init()
		{
		}
	}
}
