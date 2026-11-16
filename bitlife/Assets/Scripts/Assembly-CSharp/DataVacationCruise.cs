public class DataVacationCruise
{
	private string Name__BackingField;

	private string Continent__BackingField;

	private int CostScore__BackingField;

	private int AppealScore__BackingField;

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

	public string Continent
	{
		get
		{
			return Continent__BackingField;
		}
		set
		{
			Continent__BackingField = value;
		}
	}

	public int CostScore
	{
		get
		{
			return CostScore__BackingField;
		}
		set
		{
			CostScore__BackingField = value;
		}
	}

	public int AppealScore
	{
		get
		{
			return AppealScore__BackingField;
		}
		set
		{
			AppealScore__BackingField = value;
		}
	}

	public string Description()
	{
		return null;
	}

	public float CostWithCabin(int cabin)
	{
		return 0f;
	}

	public static string CruiseCabinStringForCabin(int cabin)
	{
		return null;
	}
}
