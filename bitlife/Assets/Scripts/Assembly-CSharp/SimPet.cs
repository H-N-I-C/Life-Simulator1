using System;
using System.Collections;

[Serializable]
public class SimPet : IComparable
{
	private string Breed__BackingField;

	private int Animal__BackingField;

	private string Name__BackingField;

	private bool Alive__BackingField;

	private int Gender__BackingField;

	private int Age__BackingField;

	private string ColorString__BackingField;

	public string PlaceOfResidenceString;

	[NonSerialized]
	private SimPlace _PlaceOfResidence;

	private int RelationshipStatus__BackingField;

	private string EmojiFileName__BackingField;

	private int Source__BackingField;

	private bool HeroIsFinanciallyResponsible__BackingField;

	private int YearsOwned__BackingField;

	private ArrayList _DiseaseArray__BackingField;

	private ArrayList _DiseaseHistoryArray__BackingField;

	private int AgeAtDeath__BackingField;

	private int DeathType__BackingField;

	private string DeathDescriptionStringScenario__BackingField;

	private string DeathDescriptionStringNarrative__BackingField;

	private int YearsSinceDeath__BackingField;

	private int YearsSinceEscape__BackingField;

	private int NumTimesAttackedHero__BackingField;

	private int NumTimesAttackedOtherPerson__BackingField;

	private int NumTimesAttackedOtherPet__BackingField;

	private float Att_happiness__BackingField;

	private float Att_health__BackingField;

	private float Att_intelligence__BackingField;

	private float Att_craziness__BackingField;

	private float Att_danger__BackingField;

	private float HeroRelationshipStrength__BackingField;

	private int HeroAgeAtLastSpendTime__BackingField;

	private int HeroAgeAtLastWalk__BackingField;

	private int HeroAgeAtLastBathe__BackingField;

	private int HeroAgeAtLastTreat__BackingField;

	private int HeroAgeAtLastVet__BackingField;

	public string Breed
	{
		get
		{
			return Breed__BackingField;
		}
		set
		{
			Breed__BackingField = value;
		}
	}

	public int Animal
	{
		get
		{
			return Animal__BackingField;
		}
		set
		{
			Animal__BackingField = value;
		}
	}

	public DataPetType PetType
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

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

	public bool Alive
	{
		get
		{
			return Alive__BackingField;
		}
		set
		{
			Alive__BackingField = value;
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

	public int Age
	{
		get
		{
			return Age__BackingField;
		}
		set
		{
			Age__BackingField = value;
		}
	}

	public string ColorString
	{
		get
		{
			return ColorString__BackingField;
		}
		set
		{
			ColorString__BackingField = value;
		}
	}

	public SimPlace PlaceOfResidence
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int RelationshipStatus
	{
		get
		{
			return RelationshipStatus__BackingField;
		}
		set
		{
			RelationshipStatus__BackingField = value;
		}
	}

	public string EmojiFileName
	{
		get
		{
			return EmojiFileName__BackingField;
		}
		set
		{
			EmojiFileName__BackingField = value;
		}
	}

	public int Source
	{
		get
		{
			return Source__BackingField;
		}
		set
		{
			Source__BackingField = value;
		}
	}

	public bool HeroIsFinanciallyResponsible
	{
		get
		{
			return HeroIsFinanciallyResponsible__BackingField;
		}
		set
		{
			HeroIsFinanciallyResponsible__BackingField = value;
		}
	}

	public int YearsOwned
	{
		get
		{
			return YearsOwned__BackingField;
		}
		set
		{
			YearsOwned__BackingField = value;
		}
	}

	private ArrayList _DiseaseArray
	{
		get
		{
			return _DiseaseArray__BackingField;
		}
		set
		{
			_DiseaseArray__BackingField = value;
		}
	}

	public ArrayList DiseaseArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _DiseaseHistoryArray
	{
		get
		{
			return _DiseaseHistoryArray__BackingField;
		}
		set
		{
			_DiseaseHistoryArray__BackingField = value;
		}
	}

	public ArrayList DiseaseHistoryArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int AgeAtDeath
	{
		get
		{
			return AgeAtDeath__BackingField;
		}
		set
		{
			AgeAtDeath__BackingField = value;
		}
	}

	public int DeathType
	{
		get
		{
			return DeathType__BackingField;
		}
		set
		{
			DeathType__BackingField = value;
		}
	}

	public string DeathDescriptionStringScenario
	{
		get
		{
			return DeathDescriptionStringScenario__BackingField;
		}
		set
		{
			DeathDescriptionStringScenario__BackingField = value;
		}
	}

	public string DeathDescriptionStringNarrative
	{
		get
		{
			return DeathDescriptionStringNarrative__BackingField;
		}
		set
		{
			DeathDescriptionStringNarrative__BackingField = value;
		}
	}

	public int YearsSinceDeath
	{
		get
		{
			return YearsSinceDeath__BackingField;
		}
		set
		{
			YearsSinceDeath__BackingField = value;
		}
	}

	public int YearsSinceEscape
	{
		get
		{
			return YearsSinceEscape__BackingField;
		}
		set
		{
			YearsSinceEscape__BackingField = value;
		}
	}

	public int NumTimesAttackedHero
	{
		get
		{
			return NumTimesAttackedHero__BackingField;
		}
		set
		{
			NumTimesAttackedHero__BackingField = value;
		}
	}

	public int NumTimesAttackedOtherPerson
	{
		get
		{
			return NumTimesAttackedOtherPerson__BackingField;
		}
		set
		{
			NumTimesAttackedOtherPerson__BackingField = value;
		}
	}

	public int NumTimesAttackedOtherPet
	{
		get
		{
			return NumTimesAttackedOtherPet__BackingField;
		}
		set
		{
			NumTimesAttackedOtherPet__BackingField = value;
		}
	}

	public float Att_happiness
	{
		get
		{
			return Att_happiness__BackingField;
		}
		set
		{
			Att_happiness__BackingField = value;
		}
	}

	public float Att_health
	{
		get
		{
			return Att_health__BackingField;
		}
		set
		{
			Att_health__BackingField = value;
		}
	}

	public float Att_intelligence
	{
		get
		{
			return Att_intelligence__BackingField;
		}
		set
		{
			Att_intelligence__BackingField = value;
		}
	}

	public float Att_craziness
	{
		get
		{
			return Att_craziness__BackingField;
		}
		set
		{
			Att_craziness__BackingField = value;
		}
	}

	public float Att_danger
	{
		get
		{
			return Att_danger__BackingField;
		}
		set
		{
			Att_danger__BackingField = value;
		}
	}

	public float HeroRelationshipStrength
	{
		get
		{
			return HeroRelationshipStrength__BackingField;
		}
		set
		{
			HeroRelationshipStrength__BackingField = value;
		}
	}

	public int HeroAgeAtLastSpendTime
	{
		get
		{
			return HeroAgeAtLastSpendTime__BackingField;
		}
		set
		{
			HeroAgeAtLastSpendTime__BackingField = value;
		}
	}

	public int HeroAgeAtLastWalk
	{
		get
		{
			return HeroAgeAtLastWalk__BackingField;
		}
		set
		{
			HeroAgeAtLastWalk__BackingField = value;
		}
	}

	public int HeroAgeAtLastBathe
	{
		get
		{
			return HeroAgeAtLastBathe__BackingField;
		}
		set
		{
			HeroAgeAtLastBathe__BackingField = value;
		}
	}

	public int HeroAgeAtLastTreat
	{
		get
		{
			return HeroAgeAtLastTreat__BackingField;
		}
		set
		{
			HeroAgeAtLastTreat__BackingField = value;
		}
	}

	public int HeroAgeAtLastVet
	{
		get
		{
			return HeroAgeAtLastVet__BackingField;
		}
		set
		{
			HeroAgeAtLastVet__BackingField = value;
		}
	}

	private SimPet()
	{
	}

	public SimPet(DataPetType thePetType, int theSource, SimPlace thePlace)
	{
	}

	public GENDER GetGender()
	{
		return GENDER.MALE;
	}

	public string Description()
	{
		return null;
	}

	public float PetValue()
	{
		return 0f;
	}

	public float MonthlyExpense()
	{
		return 0f;
	}

	public bool OwnedByHero()
	{
		return false;
	}

	public float AttackPropensityScore()
	{
		return 0f;
	}

	public float EscapePropensityScore()
	{
		return 0f;
	}

	public float DisciplinePropensityScore()
	{
		return 0f;
	}

	public float DesirabilityScore()
	{
		return 0f;
	}

	public float ResponsivenessToTrainingScore()
	{
		return 0f;
	}

	public float PredatorScore()
	{
		return 0f;
	}

	public float PreyScore()
	{
		return 0f;
	}

	public void IncrementAge()
	{
	}

	public void IncrementYearsSinceDeath()
	{
	}

	public void IncrementYearsSinceEscape()
	{
	}

	public void AnnualAttributeAdjustment()
	{
	}

	public void Die()
	{
	}

	public void DetermineDeathIfApplicable()
	{
	}

	public void UpdateDeathType(PET_DEATH_TYPE type)
	{
	}

	public void UpdateDeathType(int theDeathType)
	{
	}

	public void UpdateDeathWithFatalDisease(SimPetDisease theDisease)
	{
	}

	public void UpdateDeathWithOutdoorAnimal(SimOutdoorAnimal animal)
	{
	}

	public void UpdateDeathWithOutdoorPlant(SimOutdoorPlant plant)
	{
	}

	public void UpdateDeathWithMurderByPetTypeAndAttackVerb(DataPetType otherPetType, string attackVerb)
	{
	}

	public int ImpactOnHeroHappinessWhenDead()
	{
		return 0;
	}

	public void ImpactRelationshipStrength(int impactScore)
	{
	}

	public void ImpactHappiness(int impactScore)
	{
	}

	public void ImpactHealth(int impactScore)
	{
	}

	public void ImpactIntelligence(int impactScore)
	{
	}

	public void ImpactCraziness(int impactScore)
	{
	}

	public string FullName()
	{
		return null;
	}

	public string PetString()
	{
		return null;
	}

	public string AgeString()
	{
		return null;
	}

	public string PronounStringForType(PRONOUN_TYPE pronounType)
	{
		return null;
	}

	public string PronounStringForType(int type)
	{
		return null;
	}

	public string GetPossibleName()
	{
		return null;
	}

	public int CompareTo(object otherObject)
	{
		return 0;
	}
}
