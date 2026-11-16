public class DataAccessory
{
	private int Type__BackingField;

	private int Index__BackingField;

	private string Category__BackingField;

	private string Name__BackingField;

	private string Color__BackingField;

	private int CommonnessScore__BackingField;

	private int Gender__BackingField;

	private int MinAge__BackingField;

	private int MaxAge__BackingField;

	private int MinCraziness__BackingField;

	public int Type
	{
		get
		{
			return Type__BackingField;
		}
		set
		{
			Type__BackingField = value;
		}
	}

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

	public string Category
	{
		get
		{
			return Category__BackingField;
		}
		set
		{
			Category__BackingField = value;
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

	public string Color
	{
		get
		{
			return Color__BackingField;
		}
		set
		{
			Color__BackingField = value;
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

	public int Gender
	{
		get
		{
			return Gender__BackingField;
		}
		set
		{
			Gender__BackingField = value;
		}
	}

	public int MinAge
	{
		get
		{
			return MinAge__BackingField;
		}
		set
		{
			MinAge__BackingField = value;
		}
	}

	public int MaxAge
	{
		get
		{
			return MaxAge__BackingField;
		}
		set
		{
			MaxAge__BackingField = value;
		}
	}

	public int MinCraziness
	{
		get
		{
			return MinCraziness__BackingField;
		}
		set
		{
			MinCraziness__BackingField = value;
		}
	}

	public string GetColorString()
	{
		return null;
	}
}
