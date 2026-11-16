using System;
using System.Collections.Generic;

[Serializable]
public class SimZooCompetitor
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimZooAnimal, bool> _003C_003E9__29_0;

		public static Func<SimZooAnimal, bool> _003C_003E9__30_0;

		public static Func<SimZooAnimal, float> _003C_003E9__31_0;

		internal bool _003CExistingRareAnimal_003Eb__29_0(SimZooAnimal x)
		{
			return false;
		}

		internal bool _003CAddRareAnimal_003Eb__30_0(SimZooAnimal x)
		{
			return false;
		}

		internal float _003CGetAppeal_003Eb__31_0(SimZooAnimal t)
		{
			return 0f;
		}
	}

	private string Name__BackingField;

	private int Age__BackingField;

	private int AgeLastUpdatedCompetitorAnimals__BackingField;

	private double BankBalance__BackingField;

	private List<SimZooAnimal> _animalsList__BackingField;

	private List<SimZooRequestedAnimal> _requestedAnimalsList__BackingField;

	private float _appeal;

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

	public int AgeLastUpdatedCompetitorAnimals
	{
		get
		{
			return AgeLastUpdatedCompetitorAnimals__BackingField;
		}
		set
		{
			AgeLastUpdatedCompetitorAnimals__BackingField = value;
		}
	}

	public double BankBalance
	{
		get
		{
			return BankBalance__BackingField;
		}
		set
		{
			BankBalance__BackingField = value;
		}
	}

	private List<SimZooAnimal> _animalsList
	{
		get
		{
			return _animalsList__BackingField;
		}
		set
		{
			_animalsList__BackingField = value;
		}
	}

	private List<SimZooRequestedAnimal> _requestedAnimalsList
	{
		get
		{
			return _requestedAnimalsList__BackingField;
		}
		set
		{
			_requestedAnimalsList__BackingField = value;
		}
	}

	public SimZooCompetitor(string name)
	{
	}

	public void IncrementAge()
	{
	}

	private void RefreshRequestedZooAnimals()
	{
	}

	private void RefreshZooAnimals()
	{
	}

	public SimZooAnimal ExistingRareAnimal()
	{
		return null;
	}

	public void AddRareAnimal(SimZooAnimal rareAnimal)
	{
	}

	public float GetAppeal()
	{
		return 0f;
	}

	public void RemoveAnimal(SimZooAnimal animal)
	{
	}

	public bool AnimalExists(SimZooAnimal animal)
	{
		return false;
	}

	public List<SimZooRequestedAnimal> GetRequestedAnimalList()
	{
		return null;
	}

	public List<SimZooAnimal> GetAnimalList()
	{
		return null;
	}

	public void AddAnimal(SimZooAnimal animal)
	{
	}

	public void RemoveRequestedAnimal(SimZooRequestedAnimal animal)
	{
	}

	public SimZooAnimal GetAnimalForBreed(string breed, int minQuality = 0, int maxQuality = 0)
	{
		return null;
	}

	public bool ShouldUpdateCompetitorAnimals()
	{
		return false;
	}

	public void ImpactAppealScore(int impact)
	{
	}
}
