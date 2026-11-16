using System;
using System.Collections.Generic;

public class DataPetType : IComparable
{
	private int Animal__BackingField;

	private string EmojiFileName__BackingField;

	private string Breed__BackingField;

	public List<string> ColorStringArray;

	private bool PureBred__BackingField;

	private bool Wild__BackingField;

	private bool BlackMarket__BackingField;

	private bool Exotic__BackingField;

	private int Lifespan__BackingField;

	private int ObedienceScore__BackingField;

	private int IntelligenceScore__BackingField;

	private int DangerScore__BackingField;

	private int CostScore__BackingField;

	private int CommonnessScore__BackingField;

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

	public bool PureBred
	{
		get
		{
			return PureBred__BackingField;
		}
		set
		{
			PureBred__BackingField = value;
		}
	}

	public bool Wild
	{
		get
		{
			return Wild__BackingField;
		}
		set
		{
			Wild__BackingField = value;
		}
	}

	public bool BlackMarket
	{
		get
		{
			return BlackMarket__BackingField;
		}
		set
		{
			BlackMarket__BackingField = value;
		}
	}

	public bool Exotic
	{
		get
		{
			return Exotic__BackingField;
		}
		set
		{
			Exotic__BackingField = value;
		}
	}

	public int Lifespan
	{
		get
		{
			return Lifespan__BackingField;
		}
		set
		{
			Lifespan__BackingField = value;
		}
	}

	public int ObedienceScore
	{
		get
		{
			return ObedienceScore__BackingField;
		}
		set
		{
			ObedienceScore__BackingField = value;
		}
	}

	public int IntelligenceScore
	{
		get
		{
			return IntelligenceScore__BackingField;
		}
		set
		{
			IntelligenceScore__BackingField = value;
		}
	}

	public int DangerScore
	{
		get
		{
			return DangerScore__BackingField;
		}
		set
		{
			DangerScore__BackingField = value;
		}
	}

	public int CostScore
	{
		get
		{
			return CostScore__BackingField;
		}
		set
		{
			CostScore__BackingField = value;
		}
	}

	public int CommonnessScore
	{
		get
		{
			return CommonnessScore__BackingField;
		}
		set
		{
			CommonnessScore__BackingField = value;
		}
	}

	public string AnimalString()
	{
		return null;
	}

	public string AnimalStringIfBaby()
	{
		return null;
	}

	public string AnimalEndearmentString()
	{
		return null;
	}

	public string MainAttackString()
	{
		return null;
	}

	public string GetRandomAttackVerb()
	{
		return null;
	}

	public string GetAttackVerbOnOtherAnimal(int otherAnimal)
	{
		return null;
	}

	public bool HasCrazinessAttribute()
	{
		return false;
	}

	public bool ReferToByBreed()
	{
		return false;
	}

	public bool ReferToByBreedOnAgeString()
	{
		return false;
	}

	public bool CanBeDisciplined()
	{
		return false;
	}

	public bool CanBeWalked()
	{
		return false;
	}

	public bool CanBeRidden()
	{
		return false;
	}

	public int AnimalEscapePropensityScore()
	{
		return 0;
	}

	public int AnimalFoundAfterEscapeProbability()
	{
		return 0;
	}

	public int GetBaseValueIfYoungAndHealthy()
	{
		return 0;
	}

	public int GetBaseMonthlyExpense()
	{
		return 0;
	}

	public int GetBaseEuthanasiaCost()
	{
		return 0;
	}

	public string SoundFileNameForDiscipline(bool discipline)
	{
		return null;
	}

	public int CompareTo(object obj)
	{
		return 0;
	}
}
