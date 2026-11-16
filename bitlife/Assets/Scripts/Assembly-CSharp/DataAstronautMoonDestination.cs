public class DataAstronautMoonDestination
{
	private string Location__BackingField;

	private int CommonnessScore__BackingField;

	private int DangerScore__BackingField;

	private bool UsesDefiniteArticle__BackingField;

	public string Location
	{
		get
		{
			return Location__BackingField;
		}
		set
		{
			Location__BackingField = value;
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

	public int DangerScore
	{
		get
		{
			return DangerScore__BackingField;
		}
		set
		{
			DangerScore__BackingField = value;
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
}
