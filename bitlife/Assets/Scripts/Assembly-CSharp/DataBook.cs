public class DataBook
{
	private string Name__BackingField;

	private string Author__BackingField;

	private string Genre__BackingField;

	private int Pages__BackingField;

	private int IntelligenceScore__BackingField;

	private int HappinessScore__BackingField;

	private int MinAge__BackingField;

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

	public string Author
	{
		get
		{
			return Author__BackingField;
		}
		set
		{
			Author__BackingField = value;
		}
	}

	public string Genre
	{
		get
		{
			return Genre__BackingField;
		}
		set
		{
			Genre__BackingField = value;
		}
	}

	public int Pages
	{
		get
		{
			return Pages__BackingField;
		}
		set
		{
			Pages__BackingField = value;
		}
	}

	public int IntelligenceScore
	{
		get
		{
			return IntelligenceScore__BackingField;
		}
		set
		{
			IntelligenceScore__BackingField = value;
		}
	}

	public int HappinessScore
	{
		get
		{
			return HappinessScore__BackingField;
		}
		set
		{
			HappinessScore__BackingField = value;
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
}
