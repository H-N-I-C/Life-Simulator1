public class DataHorse
{
	private string HorseName__BackingField;

	private int SuccessScore__BackingField;

	public string HorseName
	{
		get
		{
			return HorseName__BackingField;
		}
		set
		{
			HorseName__BackingField = value;
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
}
