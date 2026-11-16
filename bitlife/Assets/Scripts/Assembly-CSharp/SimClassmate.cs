using System;

[Serializable]
public class SimClassmate : SimPerson
{
	private string ActivityString__BackingField;

	private string CliqueString__BackingField;

	private string FratString__BackingField;

	private int HeroAgeAtLastInsultHero__BackingField;

	private int HeroAgeAtLastInsulted__BackingField;

	private string LastInsultToHero__BackingField;

	private int HeroAgeAtLastAssaultHero__BackingField;

	private int HeroAgeAtLastBullyHero__BackingField;

	private int HeroAgeAtLastActUp__BackingField;

	private int HeroAgeAtLastAskedToCheat__BackingField;

	private int HeroAgeAtLastCheated__BackingField;

	private int HeroAgeAtLastSkippedSchool__BackingField;

	private int HeroAgeAtLastBusted__BackingField;

	private bool EverBulliedHero__BackingField;

	private float Att_grades__BackingField;

	private float Att_popularity__BackingField;

	public string ActivityString
	{
		get
		{
			return ActivityString__BackingField;
		}
		set
		{
			ActivityString__BackingField = value;
		}
	}

	public string CliqueString
	{
		get
		{
			return CliqueString__BackingField;
		}
		set
		{
			CliqueString__BackingField = value;
		}
	}

	public string FratString
	{
		get
		{
			return FratString__BackingField;
		}
		set
		{
			FratString__BackingField = value;
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

	public int HeroAgeAtLastInsulted
	{
		get
		{
			return HeroAgeAtLastInsulted__BackingField;
		}
		set
		{
			HeroAgeAtLastInsulted__BackingField = value;
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

	public int HeroAgeAtLastBullyHero
	{
		get
		{
			return HeroAgeAtLastBullyHero__BackingField;
		}
		set
		{
			HeroAgeAtLastBullyHero__BackingField = value;
		}
	}

	public int HeroAgeAtLastActUp
	{
		get
		{
			return HeroAgeAtLastActUp__BackingField;
		}
		set
		{
			HeroAgeAtLastActUp__BackingField = value;
		}
	}

	public int HeroAgeAtLastAskedToCheat
	{
		get
		{
			return HeroAgeAtLastAskedToCheat__BackingField;
		}
		set
		{
			HeroAgeAtLastAskedToCheat__BackingField = value;
		}
	}

	public int HeroAgeAtLastCheated
	{
		get
		{
			return HeroAgeAtLastCheated__BackingField;
		}
		set
		{
			HeroAgeAtLastCheated__BackingField = value;
		}
	}

	public int HeroAgeAtLastSkippedSchool
	{
		get
		{
			return HeroAgeAtLastSkippedSchool__BackingField;
		}
		set
		{
			HeroAgeAtLastSkippedSchool__BackingField = value;
		}
	}

	public int HeroAgeAtLastBusted
	{
		get
		{
			return HeroAgeAtLastBusted__BackingField;
		}
		set
		{
			HeroAgeAtLastBusted__BackingField = value;
		}
	}

	public bool EverBulliedHero
	{
		get
		{
			return EverBulliedHero__BackingField;
		}
		set
		{
			EverBulliedHero__BackingField = value;
		}
	}

	public float Att_grades
	{
		get
		{
			return Att_grades__BackingField;
		}
		set
		{
			Att_grades__BackingField = value;
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

	private SimClassmate()
	{
	}

	public SimClassmate(bool initHere)
	{
	}

	public override void IncrementAge()
	{
	}

	public string GetNameStringForSchoolType(int schoolType)
	{
		return null;
	}

	public bool DidSomethingWorthReporting()
	{
		return false;
	}

	public override int FeelingsForHeroScore()
	{
		return 0;
	}

	public bool QualifiesForActivity(DataSchoolActivity activityData)
	{
		return false;
	}

	public bool QualifiesForCliqueBasedOnActivity(DataSchoolClique cliqueData, DataSchoolActivity activityData)
	{
		return false;
	}

	public bool QualifiesForFrat(DataSchoolFrat fratData)
	{
		return false;
	}

	public void ImpactGrades(int impactScore)
	{
	}

	public void ImpactPopularity(int impactScore)
	{
	}
}
