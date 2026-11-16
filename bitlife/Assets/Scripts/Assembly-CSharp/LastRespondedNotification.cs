public class LastRespondedNotification
{
	private bool IsLocal__BackingField;

	private TelemetryData Data__BackingField;

	public bool IsLocal
	{
		get
		{
			return IsLocal__BackingField;
		}
		private set
		{
			IsLocal__BackingField = value;
		}
	}

	public TelemetryData Data
	{
		get
		{
			return Data__BackingField;
		}
		private set
		{
			Data__BackingField = value;
		}
	}

	public LastRespondedNotification(string jsonOrInt)
	{
	}
}
