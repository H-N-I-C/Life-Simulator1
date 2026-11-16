using System;

[Serializable]
public class SimLuxuryCharity
{
	private string Name__BackingField;

	private string Abbreviation__BackingField;

	private bool IsBadCharity__BackingField;

	private int HeroAgeAtLastMajorDonation__BackingField;

	private float _reputation;

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

	public string Abbreviation
	{
		get
		{
			return Abbreviation__BackingField;
		}
		set
		{
			Abbreviation__BackingField = value;
		}
	}

	public bool IsBadCharity
	{
		get
		{
			return IsBadCharity__BackingField;
		}
		set
		{
			IsBadCharity__BackingField = value;
		}
	}

	public int HeroAgeAtLastMajorDonation
	{
		get
		{
			return HeroAgeAtLastMajorDonation__BackingField;
		}
		set
		{
			HeroAgeAtLastMajorDonation__BackingField = value;
		}
	}

	public SimLuxuryCharity(bool badCharity)
	{
	}

	public void IncrementAge()
	{
	}

	public float GetReputation()
	{
		return 0f;
	}
}
