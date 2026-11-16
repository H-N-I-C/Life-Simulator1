using System;

[Serializable]
public class SimSportsInjury
{
	private string DescriptiveType__BackingField;

	[NonSerialized]
	public DataSportsInjury _Data;

	private int InjuryIndex__BackingField;

	private int Origin__BackingField;

	private bool Current__BackingField;

	private int AgeAtOrigin__BackingField;

	private int AgeAtOrganicHeal__BackingField;

	private bool CareerEnding__BackingField;

	private int AgeAtLastRehab__BackingField;

	private int AgeAtLastSurgery__BackingField;

	private int AgeAtLastPlayWithInjury__BackingField;

	public string DescriptiveType
	{
		get
		{
			return DescriptiveType__BackingField;
		}
		set
		{
			DescriptiveType__BackingField = value;
		}
	}

	public DataSportsInjury Data
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int InjuryIndex
	{
		get
		{
			return InjuryIndex__BackingField;
		}
		set
		{
			InjuryIndex__BackingField = value;
		}
	}

	public int Origin
	{
		get
		{
			return Origin__BackingField;
		}
		set
		{
			Origin__BackingField = value;
		}
	}

	public bool Current
	{
		get
		{
			return Current__BackingField;
		}
		set
		{
			Current__BackingField = value;
		}
	}

	public int AgeAtOrigin
	{
		get
		{
			return AgeAtOrigin__BackingField;
		}
		set
		{
			AgeAtOrigin__BackingField = value;
		}
	}

	public int AgeAtOrganicHeal
	{
		get
		{
			return AgeAtOrganicHeal__BackingField;
		}
		set
		{
			AgeAtOrganicHeal__BackingField = value;
		}
	}

	public bool CareerEnding
	{
		get
		{
			return CareerEnding__BackingField;
		}
		set
		{
			CareerEnding__BackingField = value;
		}
	}

	public int AgeAtLastRehab
	{
		get
		{
			return AgeAtLastRehab__BackingField;
		}
		set
		{
			AgeAtLastRehab__BackingField = value;
		}
	}

	public int AgeAtLastSurgery
	{
		get
		{
			return AgeAtLastSurgery__BackingField;
		}
		set
		{
			AgeAtLastSurgery__BackingField = value;
		}
	}

	public int AgeAtLastPlayWithInjury
	{
		get
		{
			return AgeAtLastPlayWithInjury__BackingField;
		}
		set
		{
			AgeAtLastPlayWithInjury__BackingField = value;
		}
	}

	public SimSportsInjury(DataSportsInjury theData, int theOrigin)
	{
	}

	public string GetTypeWithArticle(bool useArticle)
	{
		return null;
	}
}
