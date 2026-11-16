using System;

[Serializable]
public class SimArtifact : SimDiscovery
{
	private int Id__BackingField;

	private string Origin__BackingField;

	private string ArtifactName__BackingField;

	private int HeroAgeLastPlayWithArtifact__BackingField;

	private int StartingCondition__BackingField;

	private float Condition__BackingField;

	private bool Appreciates__BackingField;

	private float StartingValue__BackingField;

	private float CurrentValue__BackingField;

	private int HeroAgeAcquiredArtifact__BackingField;

	private bool GivenAsIAPUnlock__BackingField;

	public int Id
	{
		get
		{
			return Id__BackingField;
		}
		set
		{
			Id__BackingField = value;
		}
	}

	public string Origin
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

	public string ArtifactName
	{
		get
		{
			return ArtifactName__BackingField;
		}
		set
		{
			ArtifactName__BackingField = value;
		}
	}

	public int HeroAgeLastPlayWithArtifact
	{
		get
		{
			return HeroAgeLastPlayWithArtifact__BackingField;
		}
		set
		{
			HeroAgeLastPlayWithArtifact__BackingField = value;
		}
	}

	public int StartingCondition
	{
		get
		{
			return StartingCondition__BackingField;
		}
		set
		{
			StartingCondition__BackingField = value;
		}
	}

	public float Condition
	{
		get
		{
			return Condition__BackingField;
		}
		set
		{
			Condition__BackingField = value;
		}
	}

	public bool Appreciates
	{
		get
		{
			return Appreciates__BackingField;
		}
		set
		{
			Appreciates__BackingField = value;
		}
	}

	public float StartingValue
	{
		get
		{
			return StartingValue__BackingField;
		}
		set
		{
			StartingValue__BackingField = value;
		}
	}

	public float CurrentValue
	{
		get
		{
			return CurrentValue__BackingField;
		}
		set
		{
			CurrentValue__BackingField = value;
		}
	}

	public int HeroAgeAcquiredArtifact
	{
		get
		{
			return HeroAgeAcquiredArtifact__BackingField;
		}
		set
		{
			HeroAgeAcquiredArtifact__BackingField = value;
		}
	}

	public DataAstronautArtifact ArtifactData => null;

	public bool GivenAsIAPUnlock
	{
		get
		{
			return GivenAsIAPUnlock__BackingField;
		}
		set
		{
			GivenAsIAPUnlock__BackingField = value;
		}
	}

	public override bool IncrementAge()
	{
		return false;
	}

	public string GetOriginName(bool useLower = true)
	{
		return null;
	}

	public string GetArtifactLongName()
	{
		return null;
	}

	public void ImpactConditionByAmount(int amount)
	{
	}

	public void UpdateCurrentValueForNonAppreciatingArtifact()
	{
	}

	public int GetYearsWithDiscovery()
	{
		return 0;
	}

	public int GetArtifactOrigin()
	{
		return 0;
	}

	public override string EmojiString()
	{
		return null;
	}

	public bool CanBePolished()
	{
		return false;
	}

	public float PolishCost()
	{
		return 0f;
	}
}
