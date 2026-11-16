public class DataUsernameNoun
{
	private string Noun__BackingField;

	private int Gender__BackingField;

	private int MinAge__BackingField;

	private int MaxAge__BackingField;

	public string Noun
	{
		get
		{
			return Noun__BackingField;
		}
		set
		{
			Noun__BackingField = value;
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
}
