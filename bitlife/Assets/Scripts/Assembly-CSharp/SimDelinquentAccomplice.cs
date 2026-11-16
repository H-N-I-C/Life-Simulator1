public class SimDelinquentAccomplice
{
	private int PersonType__BackingField;

	private SimPerson Person__BackingField;

	private string DescriptiveName__BackingField;

	private string ListDisplayName__BackingField;

	private string RandomPersonDescriptionWithoutArticle__BackingField;

	private int Gender__BackingField;

	public int PersonType
	{
		get
		{
			return PersonType__BackingField;
		}
		set
		{
			PersonType__BackingField = value;
		}
	}

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

	public string DescriptiveName
	{
		get
		{
			return DescriptiveName__BackingField;
		}
		set
		{
			DescriptiveName__BackingField = value;
		}
	}

	public string ListDisplayName
	{
		get
		{
			return ListDisplayName__BackingField;
		}
		set
		{
			ListDisplayName__BackingField = value;
		}
	}

	public string RandomPersonDescriptionWithoutArticle
	{
		get
		{
			return RandomPersonDescriptionWithoutArticle__BackingField;
		}
		set
		{
			RandomPersonDescriptionWithoutArticle__BackingField = value;
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

	public string DescriptiveNameForFeedback()
	{
		return null;
	}

	public string DescriptiveNameForNarrative()
	{
		return null;
	}
}
