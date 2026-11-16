using System;

[Serializable]
public class TelemetryData
{
	private string Subcategory__BackingField;

	private string NotificationID__BackingField;

	public string Subcategory
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

	public string NotificationID
	{
		get
		{
			return NotificationID__BackingField;
		}
		private set
		{
			NotificationID__BackingField = value;
		}
	}

	public TelemetryData(string subcategory, string notificationID)
	{
	}
}
