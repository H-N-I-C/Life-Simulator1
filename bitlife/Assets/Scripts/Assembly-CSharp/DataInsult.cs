public class DataInsult
{
	private string Insult__BackingField;

	private string Plural__BackingField;

	private int Gender__BackingField;

	private bool Clean__BackingField;

	private bool Vampire__BackingField;

	public string Insult
	{
		get
		{
			return Insult__BackingField;
		}
		set
		{
			Insult__BackingField = value;
		}
	}

	public string Plural
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

	public bool Clean
	{
		get
		{
			return Clean__BackingField;
		}
		set
		{
			Clean__BackingField = value;
		}
	}

	public bool Vampire
	{
		get
		{
			return Vampire__BackingField;
		}
		set
		{
			Vampire__BackingField = value;
		}
	}
}
