public class DataPrisonJob
{
	private string Name__BackingField;

	private string JobDescription__BackingField;

	private int DailyRate__BackingField;

	private int CommonnessScore__BackingField;

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

	public string JobDescription
	{
		get
		{
			return JobDescription__BackingField;
		}
		set
		{
			JobDescription__BackingField = value;
		}
	}

	public int DailyRate
	{
		get
		{
			return DailyRate__BackingField;
		}
		set
		{
			DailyRate__BackingField = value;
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

	public int DailyRateForCountry(DataCountry country)
	{
		return 0;
	}

	public float AnnualizedIncomeForCountry(DataCountry country)
	{
		return 0f;
	}
}
