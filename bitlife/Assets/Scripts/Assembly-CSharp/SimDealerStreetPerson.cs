using System;
using System.Collections.Generic;

[Serializable]
public class SimDealerStreetPerson
{
	private bool IsUndercoverCop__BackingField;

	private DEALER_STREET_PERSON_TYPE Type__BackingField;

	private int HeroAgeAtLastPurchase__BackingField;

	private SimDealerCartelMember _rivalCartelMember;

	private SimStreetPerson _streetPerson;

	private Dictionary<string, float> _streetPersonAddictions;

	private List<SimDealerDisgruntledStreetPersonDrugCutSale> _disgruntledDrugCutSales;

	public bool IsUndercoverCop
	{
		get
		{
			return IsUndercoverCop__BackingField;
		}
		set
		{
			IsUndercoverCop__BackingField = value;
		}
	}

	public DEALER_STREET_PERSON_TYPE Type
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

	public int HeroAgeAtLastPurchase
	{
		get
		{
			return HeroAgeAtLastPurchase__BackingField;
		}
		set
		{
			HeroAgeAtLastPurchase__BackingField = value;
		}
	}

	public SimDealerStreetPerson(DEALER_STREET_PERSON_TYPE type)
	{
	}

	public SimDealerStreetPerson(SimDealerCartelMember rivalCartelMember)
	{
	}

	public void IncrementAge()
	{
	}

	public bool IsAlive()
	{
		return false;
	}

	public void AddDisgruntledDrugCutSale(SimDealerDrugCut drugCut, double purchasePrice)
	{
	}

	public void RemoveDisgruntledDrugCutSale(SimDealerDisgruntledStreetPersonDrugCutSale badSale)
	{
	}

	public List<SimDealerDisgruntledStreetPersonDrugCutSale> GetDisgruntledDrugCutSalesList()
	{
		return null;
	}

	public bool IsDisgruntled()
	{
		return false;
	}

	public void AddVice(string drugType, float addictionLevel)
	{
	}

	public string GetVicesListString()
	{
		return null;
	}

	public Dictionary<string, float> GetVices()
	{
		return null;
	}

	public KeyValuePair<string, float> GetGreatestVice()
	{
		return default;
	}

	public void ImpactAddictionToDrug(string drugType, int impactScore)
	{
	}

	public float GetAddictionLevelForDrug(string drugType)
	{
		return 0f;
	}

	public float GetAverageAddictionLevel()
	{
		return 0f;
	}

	public string GetName(bool includeLastName = false)
	{
		return null;
	}

	public float GetWillpower()
	{
		return 0f;
	}

	public float GetCraziness()
	{
		return 0f;
	}

	public float GetHeroRelationshipStrength()
	{
		return 0f;
	}

	public SimPerson GetPerson()
	{
		return null;
	}

	public string GetEmojiString()
	{
		return null;
	}

	public void AdjustBankBalance(double money)
	{
	}

	public double GetBankBalance()
	{
		return 0.0;
	}

	public string DescriptionString()
	{
		return null;
	}

	public string PronounStringForType(PRONOUN_TYPE type)
	{
		return null;
	}

	public bool IsAddict()
	{
		return false;
	}

	public bool IsAddictedToDrug(string drugType)
	{
		return false;
	}

	public string GetGenderString(bool capitalize)
	{
		return null;
	}

	public void ImpactRelationshipStrength(int impact)
	{
	}

	public virtual void ImpactWillpower(int impactScore)
	{
	}

	public bool IsRivalDealer()
	{
		return false;
	}

	public bool IsUniformedPoliceOfficer()
	{
		return false;
	}
}
