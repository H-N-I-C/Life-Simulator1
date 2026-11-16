namespace Tracking
{
	public enum TrackingBackendFlow
	{
		LoginStart = 0,
		LoginStartWithApple = 1,
		LoginStartWithGoogle = 2,
		LoginFailed = 3,
		LoginSuccess = 4,
		LoginExpired = 5,
		Logout = 6,
		LoginAtStart = 7,
		LoginProfile = 8,
		LoginRefreshDataWarning = 9,
		LoginDisconnectedWarning = 10,
		NewDataOverwrite = 11,
		NewDataIgnore = 12,
		LoginGuest = 13
	}
}
