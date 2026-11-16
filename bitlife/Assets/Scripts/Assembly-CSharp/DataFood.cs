public class DataFood
{
	private string Name__BackingField;

	private int CommonnessScore__BackingField;

	private int SicknessProbabilityScore__BackingField;

	public string Name
	{
		get
		{
			return Name__BackingField;
		}
		set
		{
			Name__BackingField = value;
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

	public int SicknessProbabilityScore
	{
		get
		{
			return SicknessProbabilityScore__BackingField;
		}
		set
		{
			SicknessProbabilityScore__BackingField = value;
		}
	}
}
