public class OnboardingCandidate
{
	private int Gender__BackingField;

	private SimPersonName Name__BackingField;

	private DataCountry Country__BackingField;

	private string City__BackingField;

	private string CustomLiveId__BackingField;

	private int EmojiAge__BackingField;

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

	public SimPersonName Name
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

	public DataCountry Country
	{
		get
		{
			return Country__BackingField;
		}
		set
		{
			Country__BackingField = value;
		}
	}

	public string City
	{
		get
		{
			return City__BackingField;
		}
		set
		{
			City__BackingField = value;
		}
	}

	public string CustomLiveId
	{
		get
		{
			return CustomLiveId__BackingField;
		}
		set
		{
			CustomLiveId__BackingField = value;
		}
	}

	public int EmojiAge
	{
		get
		{
			return EmojiAge__BackingField;
		}
		set
		{
			EmojiAge__BackingField = value;
		}
	}

	public OnboardingCandidate()
	{
	}

	public OnboardingCandidate(DataCountry theCountry, int theGender)
	{
	}

	private string Description()
	{
		return null;
	}

	public int GetAge()
	{
		return 0;
	}
}
