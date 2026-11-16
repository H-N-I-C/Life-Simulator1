using System;
using System.Collections.Generic;
using Game;

[Serializable]
public class SimCasinoCelebrity : SimLover
{
	private int HeroAgeAtLastInteractedWithHero__BackingField;

	private bool AcquiredWithHollywoodStar__BackingField;

	private int NumGiftsHeroTriedToGiveThisYear__BackingField;

	private string Genre__BackingField;

	private float Salary__BackingField;

	private int ContractLength__BackingField;

	private int TimesHeroMessedWith__BackingField;

	private int YearsInCasino__BackingField;

	private float LastYearRevenue__BackingField;

	private int Skin__BackingField;

	private float _popularity;

	private List<string> _requiredPerks;

	private List<int> _requiredIdPerks;

	private double LastYearProfits__BackingField;

	private double CurrentYearProfits__BackingField;

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

	public bool AcquiredWithHollywoodStar
	{
		get
		{
			return AcquiredWithHollywoodStar__BackingField;
		}
		set
		{
			AcquiredWithHollywoodStar__BackingField = value;
		}
	}

	public int NumGiftsHeroTriedToGiveThisYear
	{
		get
		{
			return NumGiftsHeroTriedToGiveThisYear__BackingField;
		}
		set
		{
			NumGiftsHeroTriedToGiveThisYear__BackingField = value;
		}
	}

	public string Genre
	{
		get
		{
			return Genre__BackingField;
		}
		set
		{
			Genre__BackingField = value;
		}
	}

	public float Salary
	{
		get
		{
			return Salary__BackingField;
		}
		set
		{
			Salary__BackingField = value;
		}
	}

	public int ContractLength
	{
		get
		{
			return ContractLength__BackingField;
		}
		set
		{
			ContractLength__BackingField = value;
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

	public int YearsInCasino
	{
		get
		{
			return YearsInCasino__BackingField;
		}
		set
		{
			YearsInCasino__BackingField = value;
		}
	}

	public float LastYearRevenue
	{
		get
		{
			return LastYearRevenue__BackingField;
		}
		set
		{
			LastYearRevenue__BackingField = value;
		}
	}

	public int Skin
	{
		get
		{
			return Skin__BackingField;
		}
		set
		{
			Skin__BackingField = value;
		}
	}

	public DataCasinoCasinoSingerGenre GenreData => null;

	public double LastYearProfits
	{
		get
		{
			return LastYearProfits__BackingField;
		}
		set
		{
			LastYearProfits__BackingField = value;
		}
	}

	public double CurrentYearProfits
	{
		get
		{
			return CurrentYearProfits__BackingField;
		}
		set
		{
			CurrentYearProfits__BackingField = value;
		}
	}

	public SimCasinoCelebrity(bool initHere)
		: base(initHere: false)
	{
	}

	public override void IncrementAge()
	{
	}

	public void AddPerk(int id, string perk)
	{
	}

	public List<string> GetRequiredPerks()
	{
		return null;
	}

	public List<int> GetRequiredIdPerks()
	{
		return null;
	}

	public float GetYearlyExpenseForPerk(int perk)
	{
		return 0f;
	}

	public new string RelationshipStatusString()
	{
		return null;
	}

	public float GetPopularity()
	{
		return 0f;
	}

	public void SetPopularity(float popularity)
	{
	}

	public override string OccupationString()
	{
		return null;
	}

	public void ImpactPopularity(int impactScore)
	{
	}

	public float GetAnnualExtraExpenses()
	{
		return 0f;
	}

	public float GetAnnualRevenue(SimCasino casino)
	{
		return 0f;
	}
}
