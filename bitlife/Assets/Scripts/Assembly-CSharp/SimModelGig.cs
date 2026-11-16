using System;

[Serializable]
public class SimModelGig
{
	private MODEL_GIG_TYPE GigType__BackingField;

	private MODEL_CAMPAIGN_TYPE CampaignType__BackingField;

	private MODEL_INDUSTRY Industry__BackingField;

	private string Category__BackingField;

	private string Venue__BackingField;

	private double Compensation__BackingField;

	private int HeroAgeAtGig__BackingField;

	private string Product__BackingField;

	private string ClientName__BackingField;

	private float Performance__BackingField;

	private float PublicReception__BackingField;

	private float Profile__BackingField;

	private int MinDesiredAge__BackingField;

	private int MaxDesiredAge__BackingField;

	private GENDER DesiredGender__BackingField;

	private string ChosenCareerType__BackingField;

	private MODEL_CAREER_TYPE MinCareerType__BackingField;

	public MODEL_GIG_TYPE GigType
	{
		get
		{
			return GigType__BackingField;
		}
		set
		{
			GigType__BackingField = value;
		}
	}

	public MODEL_CAMPAIGN_TYPE CampaignType
	{
		get
		{
			return CampaignType__BackingField;
		}
		set
		{
			CampaignType__BackingField = value;
		}
	}

	public MODEL_INDUSTRY Industry
	{
		get
		{
			return Industry__BackingField;
		}
		set
		{
			Industry__BackingField = value;
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

	public string Venue
	{
		get
		{
			return Venue__BackingField;
		}
		set
		{
			Venue__BackingField = value;
		}
	}

	public double Compensation
	{
		get
		{
			return Compensation__BackingField;
		}
		set
		{
			Compensation__BackingField = value;
		}
	}

	public int HeroAgeAtGig
	{
		get
		{
			return HeroAgeAtGig__BackingField;
		}
		set
		{
			HeroAgeAtGig__BackingField = value;
		}
	}

	public string Product
	{
		get
		{
			return Product__BackingField;
		}
		set
		{
			Product__BackingField = value;
		}
	}

	public string ClientName
	{
		get
		{
			return ClientName__BackingField;
		}
		set
		{
			ClientName__BackingField = value;
		}
	}

	public float Performance
	{
		get
		{
			return Performance__BackingField;
		}
		set
		{
			Performance__BackingField = value;
		}
	}

	public float PublicReception
	{
		get
		{
			return PublicReception__BackingField;
		}
		set
		{
			PublicReception__BackingField = value;
		}
	}

	public float Profile
	{
		get
		{
			return Profile__BackingField;
		}
		set
		{
			Profile__BackingField = value;
		}
	}

	public int MinDesiredAge
	{
		get
		{
			return MinDesiredAge__BackingField;
		}
		set
		{
			MinDesiredAge__BackingField = value;
		}
	}

	public int MaxDesiredAge
	{
		get
		{
			return MaxDesiredAge__BackingField;
		}
		set
		{
			MaxDesiredAge__BackingField = value;
		}
	}

	public GENDER DesiredGender
	{
		get
		{
			return DesiredGender__BackingField;
		}
		set
		{
			DesiredGender__BackingField = value;
		}
	}

	public string ChosenCareerType
	{
		get
		{
			return ChosenCareerType__BackingField;
		}
		set
		{
			ChosenCareerType__BackingField = value;
		}
	}

	public MODEL_CAREER_TYPE MinCareerType
	{
		get
		{
			return MinCareerType__BackingField;
		}
		set
		{
			MinCareerType__BackingField = value;
		}
	}

	public static double GenerateExtraCompensationForReputation(double reputation, double currentValue)
	{
		return 0.0;
	}
}
