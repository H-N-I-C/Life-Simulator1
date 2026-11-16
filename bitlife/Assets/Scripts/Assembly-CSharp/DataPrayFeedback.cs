public class DataPrayFeedback
{
	private string Log__BackingField;

	private bool Pray__BackingField;

	private bool Curse__BackingField;

	private int CommonnessScore__BackingField;

	public string Log
	{
		get
		{
			return Log__BackingField;
		}
		set
		{
			Log__BackingField = value;
		}
	}

	public bool Pray
	{
		get
		{
			return Pray__BackingField;
		}
		set
		{
			Pray__BackingField = value;
		}
	}

	public bool Curse
	{
		get
		{
			return Curse__BackingField;
		}
		set
		{
			Curse__BackingField = value;
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
