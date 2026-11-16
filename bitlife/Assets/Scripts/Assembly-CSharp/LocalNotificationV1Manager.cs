using System;
using System.Collections.Generic;
using Backend.Authentication;

public class LocalNotificationV1Manager : ILocalNotificationManager
{
	protected readonly LocalNotificationV1Config _config;

	protected readonly List<LocalNotificationV1> _notifications;

	private readonly NotificationScheduler _scheduler;

	private readonly ILocalNotificationAdapter _adapter;

	private readonly NotificationLogger _logger;

	private static Action _actionDelayed;

	private static bool _serverReady;

	public LocalNotificationV1Manager(LocalNotificationV1Config config)
	{
	}

	private static void Init()
	{
	}

	private static void ServerReady(GetConfigResponse _)
	{
	}

	public void AddLastRespondedNotificationToAppStartData(Dictionary<string, string> appStartData)
	{
	}

	public void RegisterNotifications()
	{
	}

	public void CheckForIntent()
	{
	}

	public void SetLocalNotificationsOnClose()
	{
	}

	public void UpdateNotificationsLanguage()
	{
	}

	private void ApplyCachedTranslationsSynchronously(List<LocalNotificationV1> notifications)
	{
	}

	private void TranslateNotificationListAsync(List<LocalNotificationV1> notifications)
	{
	}

	private void ScheduleNotifications(List<LocalNotificationV1> notifications)
	{
	}
}
