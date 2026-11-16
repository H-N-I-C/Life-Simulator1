using System;

[Serializable]
public class SimTeacher : SimPerson
{
	private int Type__BackingField;

	private int YearsAsActiveTeacher__BackingField;

	private bool HasDoctorate__BackingField;

	private int HeroAgeAtLastInsultHero__BackingField;

	private string LastInsultToHero__BackingField;

	private int HeroAgeAtLastCallOut__BackingField;

	private int LastCallOutInappropriateness__BackingField;

	private int HeroAgeAtLastAssaultHero__BackingField;

	private int HeroAgeAtLastSuckUp__BackingField;

	private int HeroAgeAtLastFlirt__BackingField;

	private int HeroAgeAtLastSeduceHero__BackingField;

	private float Att_strictness__BackingField;

	private float Att_popularity__BackingField;

	private float Att_professionalism__BackingField;

	public int Type
	{
		get
		{
			return Type__BackingField;
		}
		set
		{
			Type__BackingField = value;
		}
	}

	public int YearsAsActiveTeacher
	{
		get
		{
			return YearsAsActiveTeacher__BackingField;
		}
		set
		{
			YearsAsActiveTeacher__BackingField = value;
		}
	}

	public bool HasDoctorate
	{
		get
		{
			return HasDoctorate__BackingField;
		}
		set
		{
			HasDoctorate__BackingField = value;
		}
	}

	public int HeroAgeAtLastInsultHero
	{
		get
		{
			return HeroAgeAtLastInsultHero__BackingField;
		}
		set
		{
			HeroAgeAtLastInsultHero__BackingField = value;
		}
	}

	public string LastInsultToHero
	{
		get
		{
			return LastInsultToHero__BackingField;
		}
		set
		{
			LastInsultToHero__BackingField = value;
		}
	}

	public int HeroAgeAtLastCallOut
	{
		get
		{
			return HeroAgeAtLastCallOut__BackingField;
		}
		set
		{
			HeroAgeAtLastCallOut__BackingField = value;
		}
	}

	public int LastCallOutInappropriateness
	{
		get
		{
			return LastCallOutInappropriateness__BackingField;
		}
		set
		{
			LastCallOutInappropriateness__BackingField = value;
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

	public int HeroAgeAtLastSuckUp
	{
		get
		{
			return HeroAgeAtLastSuckUp__BackingField;
		}
		set
		{
			HeroAgeAtLastSuckUp__BackingField = value;
		}
	}

	public int HeroAgeAtLastFlirt
	{
		get
		{
			return HeroAgeAtLastFlirt__BackingField;
		}
		set
		{
			HeroAgeAtLastFlirt__BackingField = value;
		}
	}

	public int HeroAgeAtLastSeduceHero
	{
		get
		{
			return HeroAgeAtLastSeduceHero__BackingField;
		}
		set
		{
			HeroAgeAtLastSeduceHero__BackingField = value;
		}
	}

	public float Att_strictness
	{
		get
		{
			return Att_strictness__BackingField;
		}
		set
		{
			Att_strictness__BackingField = value;
		}
	}

	public float Att_popularity
	{
		get
		{
			return Att_popularity__BackingField;
		}
		set
		{
			Att_popularity__BackingField = value;
		}
	}

	public float Att_professionalism
	{
		get
		{
			return Att_professionalism__BackingField;
		}
		set
		{
			Att_professionalism__BackingField = value;
		}
	}

	private SimTeacher()
	{
	}

	public SimTeacher(bool initHere)
	{
	}

	public override void IncrementAge()
	{
	}

	public override void ImpactRelationshipStrength(int impactScore)
	{
	}

	public string GetNameStringForSchoolType(int schoolType)
	{
		return null;
	}

	public string GetTeacherTypeStringAndCapitalize(bool capitalize)
	{
		return null;
	}

	public bool IsAdministrator()
	{
		return false;
	}

	public bool DidSomethingWorthReporting()
	{
		return false;
	}

	public override int FeelingsForHeroScore()
	{
		return 0;
	}

	public void ImpactStrictness(int impactScore)
	{
	}

	public void ImpactPopularity(int impactScore)
	{
	}
}
