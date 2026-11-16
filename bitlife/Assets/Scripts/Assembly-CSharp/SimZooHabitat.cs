using System;
using System.Collections.Generic;
using Game;

[Serializable]
public class SimZooHabitat
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimZooAnimal, bool> _003C_003E9__52_0;

		public static Func<SimZooAnimal, float> _003C_003E9__55_0;

		public static Predicate<SimZooAnimal> _003C_003E9__59_0;

		public static Func<SimZooAnimal, float> _003C_003E9__64_0;

		public static Func<DataZooHabitatType, string> _003C_003E9__67_0;

		public static Func<SimZooAnimal, float> _003C_003E9__68_0;

		internal bool _003CRemoveDeadAnimals_003Eb__52_0(SimZooAnimal t)
		{
			return false;
		}

		internal float _003CGetPopularity_003Eb__55_0(SimZooAnimal t)
		{
			return 0f;
		}

		internal bool _003CAddAnimal_003Eb__59_0(SimZooAnimal x)
		{
			return false;
		}

		internal float _003CGetAnimalsUpkeepValue_003Eb__64_0(SimZooAnimal t)
		{
			return 0f;
		}

		internal string _003CPossibleHabitats_003Eb__67_0(DataZooHabitatType t)
		{
			return null;
		}

		internal float _003CGetAverageAnimalsHealth_003Eb__68_0(SimZooAnimal x)
		{
			return 0f;
		}
	}

	private sealed class _003C_003Ec__DisplayClass63_0
	{
		public SimZooHabitat _003C_003E4__this;

		public SimZoo zoo;

		internal float _003CGetMostPopularAnimal_003Eb__0(SimZooAnimal t)
		{
			return 0f;
		}
	}

	private string Name__BackingField;

	private int Age__BackingField;

	private string Type__BackingField;

	private int MaxAnimalSlots__BackingField;

	private int NumTimesAnimalOfferedToFillSlotAvailableThisYear__BackingField;

	private int NumTimesCleanedHabitat__BackingField;

	private bool AnimalAlreadyMurderedInHabitatThisYear__BackingField;

	private float CleanCondition__BackingField;

	private List<SimZooAnimal> AnimalsList__BackingField;

	private SimZooHabitatFeature Feature__BackingField;

	private double Cost__BackingField;

	private double AnnualUpkeepCost__BackingField;

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

	public int MaxAnimalSlots
	{
		get
		{
			return MaxAnimalSlots__BackingField;
		}
		set
		{
			MaxAnimalSlots__BackingField = value;
		}
	}

	public int NumTimesAnimalOfferedToFillSlotAvailableThisYear
	{
		get
		{
			return NumTimesAnimalOfferedToFillSlotAvailableThisYear__BackingField;
		}
		set
		{
			NumTimesAnimalOfferedToFillSlotAvailableThisYear__BackingField = value;
		}
	}

	public int NumTimesCleanedHabitat
	{
		get
		{
			return NumTimesCleanedHabitat__BackingField;
		}
		set
		{
			NumTimesCleanedHabitat__BackingField = value;
		}
	}

	public bool AnimalAlreadyMurderedInHabitatThisYear
	{
		get
		{
			return AnimalAlreadyMurderedInHabitatThisYear__BackingField;
		}
		set
		{
			AnimalAlreadyMurderedInHabitatThisYear__BackingField = value;
		}
	}

	public float CleanCondition
	{
		get
		{
			return CleanCondition__BackingField;
		}
		set
		{
			CleanCondition__BackingField = value;
		}
	}

	public List<SimZooAnimal> AnimalsList
	{
		get
		{
			return AnimalsList__BackingField;
		}
		set
		{
			AnimalsList__BackingField = value;
		}
	}

	public SimZooHabitatFeature Feature
	{
		get
		{
			return Feature__BackingField;
		}
		set
		{
			Feature__BackingField = value;
		}
	}

	public double Cost
	{
		get
		{
			return Cost__BackingField;
		}
		set
		{
			Cost__BackingField = value;
		}
	}

	public double AnnualUpkeepCost
	{
		get
		{
			return AnnualUpkeepCost__BackingField;
		}
		set
		{
			AnnualUpkeepCost__BackingField = value;
		}
	}

	public DataZooHabitatType HabitatData => null;

	public SimZooHabitat(string type = null)
	{
	}

	public void IncrementAge()
	{
	}

	private void RemoveDeadAnimals()
	{
	}

	public float GetLivingCondition()
	{
		return 0f;
	}

	public void ImpactLivingCondition(int impact)
	{
	}

	public float GetPopularity()
	{
		return 0f;
	}

	public string GetHabitatTypeString()
	{
		return null;
	}

	public bool HasVacancy()
	{
		return false;
	}

	public bool AnimalExists(SimZooAnimal animal)
	{
		return false;
	}

	public void AddAnimal(SimZooAnimal animal)
	{
	}

	public void RemoveAnimal(SimZooAnimal animal)
	{
	}

	public void AddFeature(SimZooHabitatFeature feature)
	{
	}

	public void RemoveFeature()
	{
	}

	public SimZooAnimal GetMostPopularAnimal(SimZoo zoo = null)
	{
		return null;
	}

	public double GetAnimalsUpkeepValue()
	{
		return 0.0;
	}

	public SimZooAnimal GetFoodChainAnimalForAnimal(SimZooAnimal animal, int minFoodChainDifference)
	{
		return null;
	}

	public int GetFeatureHappinessScore()
	{
		return 0;
	}

	public static List<string> PossibleHabitats()
	{
		return null;
	}

	public float GetAverageAnimalsHealth()
	{
		return 0f;
	}
}
