using System;

public class ScheduledEntry
{
	private DateTime Time__BackingField;

	private NotificationSubcategory Subcategory__BackingField;

	public DateTime Time
	{
		get
		{
			return Time__BackingField;
		}
		private set
		{
			Time__BackingField = value;
		}
	}

	public NotificationSubcategory Subcategory
	{
		get
		{
			return Subcategory__BackingField;
		}
		private set
		{
			Subcategory__BackingField = value;
		}
	}

	public ScheduledEntry(DateTime time, NotificationSubcategory subcategory)
	{
	}
}
