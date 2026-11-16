public class DataDiet
{
	private string Name__BackingField;

	private bool UsesDefiniteArticle__BackingField;

	private int MonthlyCost__BackingField;

	private int HealthImpact__BackingField;

	private int HappinessImpact__BackingField;

	private int AppearanceImpact__BackingField;

	private string AssociatedDiseaseName__BackingField;

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

	public bool UsesDefiniteArticle
	{
		get
		{
			return UsesDefiniteArticle__BackingField;
		}
		set
		{
			UsesDefiniteArticle__BackingField = value;
		}
	}

	public int MonthlyCost
	{
		get
		{
			return MonthlyCost__BackingField;
		}
		set
		{
			MonthlyCost__BackingField = value;
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

	public int AppearanceImpact
	{
		get
		{
			return AppearanceImpact__BackingField;
		}
		set
		{
			AppearanceImpact__BackingField = value;
		}
	}

	public string AssociatedDiseaseName
	{
		get
		{
			return AssociatedDiseaseName__BackingField;
		}
		set
		{
			AssociatedDiseaseName__BackingField = value;
		}
	}

	public string NameWithArticle()
	{
		return null;
	}
}
