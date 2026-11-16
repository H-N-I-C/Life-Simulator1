public class SimHackingTarget
{
	private SimPerson Person__BackingField;

	private string Description__BackingField;

	private string DropDownDisplayName__BackingField;

	private int Gender__BackingField;

	private float MoneyScore__BackingField;

	public SimPerson Person
	{
		get
		{
			return Person__BackingField;
		}
		set
		{
			Person__BackingField = value;
		}
	}

	public string Description
	{
		get
		{
			return Description__BackingField;
		}
		set
		{
			Description__BackingField = value;
		}
	}

	public string DropDownDisplayName
	{
		get
		{
			return DropDownDisplayName__BackingField;
		}
		set
		{
			DropDownDisplayName__BackingField = value;
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

	public float MoneyScore
	{
		get
		{
			return MoneyScore__BackingField;
		}
		set
		{
			MoneyScore__BackingField = value;
		}
	}
}
