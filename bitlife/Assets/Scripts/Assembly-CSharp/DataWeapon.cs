public class DataWeapon
{
	private string Name__BackingField;

	private bool Plural__BackingField;

	private string Verb__BackingField;

	private int DangerScore__BackingField;

	private bool FoundInPrison__BackingField;

	private bool Medieval__BackingField;

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

	public string Verb
	{
		get
		{
			return Verb__BackingField;
		}
		set
		{
			Verb__BackingField = value;
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

	public bool FoundInPrison
	{
		get
		{
			return FoundInPrison__BackingField;
		}
		set
		{
			FoundInPrison__BackingField = value;
		}
	}

	public bool Medieval
	{
		get
		{
			return Medieval__BackingField;
		}
		set
		{
			Medieval__BackingField = value;
		}
	}

	public string GetNameWithIndefiniteArticle()
	{
		return null;
	}
}
