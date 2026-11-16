using System;
using System.Collections.Generic;
using Game;

[Serializable]
public class SimZooAnimal
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, string> _003C_003E9__94_0;

		public static Func<SimZooAnimalDisease, string> _003C_003E9__95_0;

		internal string _003CGetCompatibleHabitatString_003Eb__94_0(string t)
		{
			return null;
		}

		internal string _003CGetAnimalDiseasesString_003Eb__95_0(SimZooAnimalDisease t)
		{
			return null;
		}
	}

	private bool Alive__BackingField;

	private string Name__BackingField;

	private string Type__BackingField;

	private string Breed__BackingField;

	private int Age__BackingField;

	private GENDER Gender__BackingField;

	private int YearsInZoo__BackingField;

	private ZOO_ANIMAL_DEATH_TYPE DeathType__BackingField;

	private int AgeAtDeath__BackingField;

	private bool IsPregnant__BackingField;

	private float _smarts;

	private float _craziness;

	private float _treatsAndSpendingTimeHappiness;

	private float _charisma;

	private float _diseaseFreeLevel;

	private float _healthBuff;

	private float _thisYearsHealthBuffIncrease;

	private List<SimZooAnimalDisease> _diseases;

	private List<SimZooAnimalDisease> _diseaseHistory;

	private List<string> _compatibleTradingBreeds;

	private int NumTimesTreatsAvailableThisYear__BackingField;

	private int NumTimesSpendTimeAvailableThisYear__BackingField;

	private int NumSellOffersAvailableThisYear__BackingField;

	private int NumTradeOffersAvailableThisYear__BackingField;

	private float _popularityNoZoo;

	private float _happinessNoZoo;

	private float _healthNoZoo;

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

	public string Type
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

	public GENDER Gender
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

	public int YearsInZoo
	{
		get
		{
			return YearsInZoo__BackingField;
		}
		set
		{
			YearsInZoo__BackingField = value;
		}
	}

	public ZOO_ANIMAL_DEATH_TYPE DeathType
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

	public bool IsPregnant
	{
		get
		{
			return IsPregnant__BackingField;
		}
		set
		{
			IsPregnant__BackingField = value;
		}
	}

	public int NumTimesTreatsAvailableThisYear
	{
		get
		{
			return NumTimesTreatsAvailableThisYear__BackingField;
		}
		set
		{
			NumTimesTreatsAvailableThisYear__BackingField = value;
		}
	}

	public int NumTimesSpendTimeAvailableThisYear
	{
		get
		{
			return NumTimesSpendTimeAvailableThisYear__BackingField;
		}
		set
		{
			NumTimesSpendTimeAvailableThisYear__BackingField = value;
		}
	}

	public int NumSellOffersAvailableThisYear
	{
		get
		{
			return NumSellOffersAvailableThisYear__BackingField;
		}
		set
		{
			NumSellOffersAvailableThisYear__BackingField = value;
		}
	}

	public int NumTradeOffersAvailableThisYear
	{
		get
		{
			return NumTradeOffersAvailableThisYear__BackingField;
		}
		set
		{
			NumTradeOffersAvailableThisYear__BackingField = value;
		}
	}

	public DataZooAnimal AnimalData => null;

	public SimZooAnimal(bool heroZoo, string breed = null, GENDER gender = GENDER.BOTH, float qualityLevel = 50f, bool isNewborn = false)
	{
	}

	public void IncrementAge()
	{
	}

	private void ProcessDiseases()
	{
	}

	public void CureDisease(SimZooAnimalDisease disease)
	{
	}

	public void ImpactDiseaseLevel(int impact)
	{
	}

	public void ImpactTreatAndSpendingHappiness(int impact)
	{
	}

	public void ImpactCharismaByAmount(int amount)
	{
	}

	public void ImpactCharisma(int impact)
	{
	}

	public float GetHealth(SimZoo zoo = null)
	{
		return 0f;
	}

	public float GetPopularity(SimZooHabitat assignedHabitat = null, SimZoo zoo = null)
	{
		return 0f;
	}

	public float GetHappiness(SimZoo zoo = null)
	{
		return 0f;
	}

	public float GetSmarts()
	{
		return 0f;
	}

	public float GetCraziness()
	{
		return 0f;
	}

	public float GetAnimalQualityLevel()
	{
		return 0f;
	}

	public float GetAnimalAnnualCost()
	{
		return 0f;
	}

	public bool CompatibleWithHabitatType(string habitatType)
	{
		return false;
	}

	public string PronounStringForType(PRONOUN_TYPE type)
	{
		return null;
	}

	public List<string> GetCompatibleTradingBreeds()
	{
		return null;
	}

	public float GetAnimalCoolScore()
	{
		return 0f;
	}

	private void CreateCompatibleTradingBreeds()
	{
	}

	public static string GetGenderString(GENDER gender, bool capitalize)
	{
		return null;
	}

	public static double GetAnimalMarketPrice(string animalBreed, float animalQualityLevel)
	{
		return 0.0;
	}

	public static SimZooAnimal GenerateNewHeroZooAnimal(SimZooHabitat habitat, int quality, string breed, GENDER gender)
	{
		return null;
	}

	public string GetCompatibleHabitatString()
	{
		return null;
	}

	public string GetAnimalDiseasesString()
	{
		return null;
	}

	public bool IsSick()
	{
		return false;
	}

	public SimZooAnimalDisease GetRandomActiveDisease()
	{
		return null;
	}

	public List<SimZooAnimalDisease> GetActiveDiseases()
	{
		return null;
	}

	public double GetTreatmentCostForDisease(SimZooAnimalDisease disease)
	{
		return 0.0;
	}

	public void AcquireDisease(SimZooAnimalDisease resultingDisease)
	{
	}

	public string EmojiString()
	{
		return null;
	}

	public void Impregnate()
	{
	}

	public SimZooAnimal GiveBirth(bool successful = true)
	{
		return null;
	}

	public void Die()
	{
	}

	private DataAccessoryHeadwear GetHeadwearAccessory()
	{
		return null;
	}

	public DataAccessoryHeadwear GetAccessoryHeadwearGift()
	{
		return null;
	}

	public void PositivelyImpactHealthBuff()
	{
	}
}
