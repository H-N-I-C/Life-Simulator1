public class DataWitchRemedy
{
	private string Name__BackingField;

	private int EffectivenessScore__BackingField;

	private int RiskScore__BackingField;

	private int CostScore__BackingField;

	private DataDiseaseType TreatsDisease__BackingField;

	private int RandomCostComponent__BackingField;

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

	public int EffectivenessScore
	{
		get
		{
			return EffectivenessScore__BackingField;
		}
		set
		{
			EffectivenessScore__BackingField = value;
		}
	}

	public int RiskScore
	{
		get
		{
			return RiskScore__BackingField;
		}
		set
		{
			RiskScore__BackingField = value;
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

	public DataDiseaseType TreatsDisease
	{
		get
		{
			return TreatsDisease__BackingField;
		}
		set
		{
			TreatsDisease__BackingField = value;
		}
	}

	public int RandomCostComponent
	{
		get
		{
			return RandomCostComponent__BackingField;
		}
		set
		{
			RandomCostComponent__BackingField = value;
		}
	}

	public float Cost()
	{
		return 0f;
	}
}
