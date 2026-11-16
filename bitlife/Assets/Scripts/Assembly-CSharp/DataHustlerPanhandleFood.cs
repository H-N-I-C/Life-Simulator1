public class DataHustlerPanhandleFood
{
	private int Index__BackingField;

	private string Item__BackingField;

	private bool Plural__BackingField;

	private bool UsesIndefiniteArticle__BackingField;

	private int HealthImpact__BackingField;

	private int HappinessImpact__BackingField;

	private int PoisonRiskScore__BackingField;

	public int Index
	{
		get
		{
			return Index__BackingField;
		}
		set
		{
			Index__BackingField = value;
		}
	}

	public string Item
	{
		get
		{
			return Item__BackingField;
		}
		set
		{
			Item__BackingField = value;
		}
	}

	public bool Plural
	{
		get
		{
			return Plural__BackingField;
		}
		set
		{
			Plural__BackingField = value;
		}
	}

	public bool UsesIndefiniteArticle
	{
		get
		{
			return UsesIndefiniteArticle__BackingField;
		}
		set
		{
			UsesIndefiniteArticle__BackingField = value;
		}
	}

	public int HealthImpact
	{
		get
		{
			return HealthImpact__BackingField;
		}
		set
		{
			HealthImpact__BackingField = value;
		}
	}

	public int HappinessImpact
	{
		get
		{
			return HappinessImpact__BackingField;
		}
		set
		{
			HappinessImpact__BackingField = value;
		}
	}

	public int PoisonRiskScore
	{
		get
		{
			return PoisonRiskScore__BackingField;
		}
		set
		{
			PoisonRiskScore__BackingField = value;
		}
	}

	public string GetStringWithArticleIfApplicable()
	{
		return null;
	}
}
