using System;

public class LocalNotificationV1
{
	private DateTime IntendedFireDate__BackingField;

	private string DataJson__BackingField;

	private string Title__BackingField;

	private string Body__BackingField;

	public DateTime IntendedFireDate
	{
		get
		{
			return IntendedFireDate__BackingField;
		}
		private set
		{
			IntendedFireDate__BackingField = value;
		}
	}

	public string DataJson
	{
		get
		{
			return DataJson__BackingField;
		}
		private set
		{
			DataJson__BackingField = value;
		}
	}

	public string Title
	{
		get
		{
			return Title__BackingField;
		}
		private set
		{
			Title__BackingField = value;
		}
	}

	public string Body
	{
		get
		{
			return Body__BackingField;
		}
		private set
		{
			Body__BackingField = value;
		}
	}

	public LocalNotificationV1(DateTime intendedFireDate, TelemetryData data, string title, string body)
	{
	}

	public void UpdateTranslatedTexts(string title, string body)
	{
	}

	public override string ToString()
	{
		return null;
	}
}
