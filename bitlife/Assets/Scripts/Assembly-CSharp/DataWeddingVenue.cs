public class DataWeddingVenue
{
	private string Name__BackingField;

	private bool UsesDefiniteArticle__BackingField;

	private int CostScore__BackingField;

	private int LoverHappinessScore__BackingField;

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

	public int LoverHappinessScore
	{
		get
		{
			return LoverHappinessScore__BackingField;
		}
		set
		{
			LoverHappinessScore__BackingField = value;
		}
	}

	public string VenueDescription()
	{
		return null;
	}

	public float Cost()
	{
		return 0f;
	}
}
