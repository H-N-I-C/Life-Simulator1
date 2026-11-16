using System;

[Serializable]
public class SimRaceCarDriver : SimLover
{
	private int HeroAgeAtLastInjury__BackingField;

	private int HeroAgeAtLastInteractedWithHero__BackingField;

	private int HeroAgeAtLastAssaultHero__BackingField;

	private RACING_TRACK_CATEGORY Category__BackingField;

	private RACING_LICENSE License__BackingField;

	private string ReplacementForDriverName__BackingField;

	private SimRaceCar RaceCar__BackingField;

	private int RacesWon__BackingField;

	private int RacesLost__BackingField;

	private bool IsRival__BackingField;

	private int NumRacesWithHeroThisYear__BackingField;

	private float _skill;

	public int HeroAgeAtLastInjury
	{
		get
		{
			return HeroAgeAtLastInjury__BackingField;
		}
		set
		{
			HeroAgeAtLastInjury__BackingField = value;
		}
	}

	public int HeroAgeAtLastInteractedWithHero
	{
		get
		{
			return HeroAgeAtLastInteractedWithHero__BackingField;
		}
		set
		{
			HeroAgeAtLastInteractedWithHero__BackingField = value;
		}
	}

	public int HeroAgeAtLastAssaultHero
	{
		get
		{
			return HeroAgeAtLastAssaultHero__BackingField;
		}
		set
		{
			HeroAgeAtLastAssaultHero__BackingField = value;
		}
	}

	public RACING_TRACK_CATEGORY Category
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

	public RACING_LICENSE License
	{
		get
		{
			return License__BackingField;
		}
		set
		{
			License__BackingField = value;
		}
	}

	public string ReplacementForDriverName
	{
		get
		{
			return ReplacementForDriverName__BackingField;
		}
		set
		{
			ReplacementForDriverName__BackingField = value;
		}
	}

	public SimRaceCar RaceCar
	{
		get
		{
			return RaceCar__BackingField;
		}
		set
		{
			RaceCar__BackingField = value;
		}
	}

	public int RacesWon
	{
		get
		{
			return RacesWon__BackingField;
		}
		set
		{
			RacesWon__BackingField = value;
		}
	}

	public int RacesLost
	{
		get
		{
			return RacesLost__BackingField;
		}
		set
		{
			RacesLost__BackingField = value;
		}
	}

	public bool IsRival
	{
		get
		{
			return IsRival__BackingField;
		}
		set
		{
			IsRival__BackingField = value;
		}
	}

	public int NumRacesWithHeroThisYear
	{
		get
		{
			return NumRacesWithHeroThisYear__BackingField;
		}
		set
		{
			NumRacesWithHeroThisYear__BackingField = value;
		}
	}

	public SimRaceCarDriver(RACING_TRACK_CATEGORY category, RACING_LICENSE license, bool initHere)
		: base(initHere: false)
	{
	}

	public override void IncrementAge()
	{
	}

	public float GetSkill()
	{
		return 0f;
	}

	public void SetSkill(float value)
	{
	}

	public void AssignRaceCar(CAR_TYPE forcedCircuitCarType = (CAR_TYPE)0)
	{
	}

	public new string RelationshipStatusString()
	{
		return null;
	}

	private void AssignAttributes(bool isUpgrade = false)
	{
	}

	public void UpgradeToLicense(RACING_LICENSE newLicense)
	{
	}
}
