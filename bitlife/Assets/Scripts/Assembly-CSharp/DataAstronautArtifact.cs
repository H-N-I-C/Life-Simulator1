public class DataAstronautArtifact
{
	private int Index__BackingField;

	private string Name__BackingField;

	private string ShortName__BackingField;

	private string Location__BackingField;

	private bool IndefiniteArticle__BackingField;

	private bool Plural__BackingField;

	private bool CanBePolished__BackingField;

	private int MinValue__BackingField;

	private int MaxValue__BackingField;

	private bool Appreciates__BackingField;

	private int CommonnessScore__BackingField;

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

	public string ShortName
	{
		get
		{
			return ShortName__BackingField;
		}
		set
		{
			ShortName__BackingField = value;
		}
	}

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

	public bool IndefiniteArticle
	{
		get
		{
			return IndefiniteArticle__BackingField;
		}
		set
		{
			IndefiniteArticle__BackingField = value;
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

	public bool CanBePolished
	{
		get
		{
			return CanBePolished__BackingField;
		}
		set
		{
			CanBePolished__BackingField = value;
		}
	}

	public int MinValue
	{
		get
		{
			return MinValue__BackingField;
		}
		set
		{
			MinValue__BackingField = value;
		}
	}

	public int MaxValue
	{
		get
		{
			return MaxValue__BackingField;
		}
		set
		{
			MaxValue__BackingField = value;
		}
	}

	public bool Appreciates
	{
		get
		{
			return Appreciates__BackingField;
		}
		set
		{
			Appreciates__BackingField = value;
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

	public string GetShortHandNameAndCapitalize(bool capitalize)
	{
		return null;
	}

	public string GetLongHandNameAndCapitalize(bool capitalize)
	{
		return null;
	}
}
