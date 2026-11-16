using System;

[Serializable]
public class SimCoworker : SimPerson, IComparable
{
	private int Type__BackingField;

	private int WorkplaceType__BackingField;

	private int HeroAgeAtLastInsultHero__BackingField;

	private string LastInsultToHero__BackingField;

	private int HeroAgeAtLastBehavedUnprofessionalToHero__BackingField;

	private string LastUnprofessionalBehavior__BackingField;

	private int HeroAgeAtLastAssaultHero__BackingField;

	private int HeroAgeAtLastSeduceHero__BackingField;

	private int HeroAgeAtLastBehavedInappropriateToHero__BackingField;

	private int HeroAgeAtLastPrankedHero__BackingField;

	private int HeroAgeAtLastRumorStartedAboutHero__BackingField;

	private int HeroAgeAtLastAnnoyedHero__BackingField;

	private int HeroAgeAtLastBusted__BackingField;

	private int TimesHeroMessedWith__BackingField;

	private int TimesHeroPranked__BackingField;

	private float Att_coolness__BackingField;

	private float Att_professionalism__BackingField;

	private float Att_strictness__BackingField;

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

	public int WorkplaceType
	{
		get
		{
			return WorkplaceType__BackingField;
		}
		set
		{
			WorkplaceType__BackingField = value;
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

	public int HeroAgeAtLastBehavedUnprofessionalToHero
	{
		get
		{
			return HeroAgeAtLastBehavedUnprofessionalToHero__BackingField;
		}
		set
		{
			HeroAgeAtLastBehavedUnprofessionalToHero__BackingField = value;
		}
	}

	public string LastUnprofessionalBehavior
	{
		get
		{
			return LastUnprofessionalBehavior__BackingField;
		}
		set
		{
			LastUnprofessionalBehavior__BackingField = value;
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

	public int HeroAgeAtLastBehavedInappropriateToHero
	{
		get
		{
			return HeroAgeAtLastBehavedInappropriateToHero__BackingField;
		}
		set
		{
			HeroAgeAtLastBehavedInappropriateToHero__BackingField = value;
		}
	}

	public int HeroAgeAtLastPrankedHero
	{
		get
		{
			return HeroAgeAtLastPrankedHero__BackingField;
		}
		set
		{
			HeroAgeAtLastPrankedHero__BackingField = value;
		}
	}

	public int HeroAgeAtLastRumorStartedAboutHero
	{
		get
		{
			return HeroAgeAtLastRumorStartedAboutHero__BackingField;
		}
		set
		{
			HeroAgeAtLastRumorStartedAboutHero__BackingField = value;
		}
	}

	public int HeroAgeAtLastAnnoyedHero
	{
		get
		{
			return HeroAgeAtLastAnnoyedHero__BackingField;
		}
		set
		{
			HeroAgeAtLastAnnoyedHero__BackingField = value;
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

	public int TimesHeroMessedWith
	{
		get
		{
			return TimesHeroMessedWith__BackingField;
		}
		set
		{
			TimesHeroMessedWith__BackingField = value;
		}
	}

	public int TimesHeroPranked
	{
		get
		{
			return TimesHeroPranked__BackingField;
		}
		set
		{
			TimesHeroPranked__BackingField = value;
		}
	}

	public float Att_coolness
	{
		get
		{
			return Att_coolness__BackingField;
		}
		set
		{
			Att_coolness__BackingField = value;
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

	private SimCoworker()
	{
	}

	public SimCoworker(bool initHere)
	{
	}

	public override void IncrementAge()
	{
	}

	public override void ImpactRelationshipStrength(int impactScore)
	{
	}

	public string NameStringAndIncludeFullName(bool fullName)
	{
		return null;
	}

	public string RelationshipStatusStringAndCapitalize(bool capitalize)
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

	public bool OutranksHero()
	{
		return false;
	}

	public void ImpactCoolness(int impactScore)
	{
	}

	public void ImpactProfessionalism(int impactScore)
	{
	}

	public int CompareTo(object obj)
	{
		return 0;
	}
}
