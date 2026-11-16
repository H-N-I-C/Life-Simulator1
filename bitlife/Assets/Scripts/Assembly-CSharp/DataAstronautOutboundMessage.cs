public class DataAstronautOutboundMessage
{
	private string Message__BackingField;

	private int SuccessScore__BackingField;

	private int CommonnessScore__BackingField;

	public string Message
	{
		get
		{
			return Message__BackingField;
		}
		set
		{
			Message__BackingField = value;
		}
	}

	public int SuccessScore
	{
		get
		{
			return SuccessScore__BackingField;
		}
		set
		{
			SuccessScore__BackingField = value;
		}
	}

	public int CommonnessScore
	{
		get
		{
			return CommonnessScore__BackingField;
		}
		set
		{
			CommonnessScore__BackingField = value;
		}
	}
}
