using System.Collections.Generic;

public class NotificationLogger
{
	private readonly LocalNotificationV1Config _config;

	private static PlayerPrefsBoolRelease _permissionPref;

	private static PlayerPrefsBoolRelease _promptShownPref;

	public PlayerPrefsBoolRelease PermissionPref => null;

	private static PlayerPrefsBoolRelease PromptShownPref => null;

	public NotificationLogger(LocalNotificationV1Config config)
	{
	}

	public void AddLastRespondedNotificationToAppStartData(string notificationDataJson, Dictionary<string, string> appStartData)
	{
	}

	public bool HasShownPrompt()
	{
		return false;
	}

	public void HandleIsPermissionGranted(bool granted)
	{
	}

	public void UpdatePermissionChanged(NOTIFICATION_PERMISSION permission)
	{
	}

	public static void Log(string message)
	{
	}

	public static void LogError(string message)
	{
	}

	private static void SetData(Dictionary<string, string> appStartData, string key, string value)
	{
	}
}
