using System.Collections.Generic;
using Game;

public class NotificationScheduler
{
	private readonly LocalNotificationV1Config _config;

	private readonly List<LocalNotificationV1> _notifications;

	private readonly NotificationCreator _creator;

	public NotificationScheduler(LocalNotificationV1Config config, List<LocalNotificationV1> notifications)
	{
	}

	private void SetNotificationsByLife(Life life)
	{
	}

	public bool TrySetNotificationsByLife(Life life)
	{
		return false;
	}

	private DataLocalNotificationV1Text CreateFilterByLife(Life life)
	{
		return null;
	}
}
