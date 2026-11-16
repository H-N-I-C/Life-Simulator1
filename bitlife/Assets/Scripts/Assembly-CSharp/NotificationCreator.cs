using System;
using System.Collections.Generic;
using Game;

public class NotificationCreator
{
	private const string GeneralSubcategory = "general_callback_to_play";

	private const string BitPassSubcategory = "BitPass";

	private readonly LocalNotificationV1Config _config;

	private readonly List<LocalNotificationV1> _notifications;

	public NotificationCreator(LocalNotificationV1Config config, List<LocalNotificationV1> notifications)
	{
	}

	public void CreateFromSchedule(List<ScheduledEntry> schedule, DataLocalNotificationV1Text filter, Life life)
	{
	}

	private static void PrepareBitPass(Dictionary<string, string> bracketDictionary)
	{
	}

	private void AddNotification(DataLocalNotificationV1Text filter, BracketParser parser, DateTime time, NotificationSubcategory subcategory)
	{
	}

	private static string GetSubcategoryName(NotificationSubcategory subcategory)
	{
		return null;
	}
}
