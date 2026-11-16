public class DataActorExtraRole
{
	private string Role__BackingField;

	private int Gender__BackingField;

	private int MinAge__BackingField;

	private int MaxAge__BackingField;

	public string Role
	{
		get
		{
			return Role__BackingField;
		}
		set
		{
			Role__BackingField = value;
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
