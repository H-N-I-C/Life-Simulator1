using System;
using System.Collections.Generic;
using Game;

[Serializable]
public class SimDealerCartel
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Predicate<SimDealerCartelMember> _003C_003E9__42_0;

		public static Func<SimDealerCartelMember, bool> _003C_003E9__45_0;

		public static Func<SimDealerDrug, string> _003C_003E9__51_0;

		public static Func<SimDealerDrug, float> _003C_003E9__51_1;

		public static Func<SimDealerDrug, float> _003C_003E9__52_0;

		public static Func<SimDealerCartelMember, bool> _003C_003E9__55_0;

		public static Func<SimDealerCartelMember, bool> _003C_003E9__58_0;

		public static Func<SimDealerDrug, float> _003C_003E9__70_0;

		public static Predicate<SimDealerCartelMember> _003C_003E9__71_0;

		internal bool _003CCompleteSetup_003Eb__42_0(SimDealerCartelMember x)
		{
			return false;
		}

		internal bool _003CGetKingpin_003Eb__45_0(SimDealerCartelMember x)
		{
			return false;
		}

		internal string _003CGetSellableDrugsList_003Eb__51_0(SimDealerDrug x)
		{
			return null;
		}

		internal float _003CGetSellableDrugsList_003Eb__51_1(SimDealerDrug x)
		{
			return 0f;
		}

		internal float _003CGetDrugsListString_003Eb__52_0(SimDealerDrug t)
		{
			return 0f;
		}

		internal bool _003CGetMembersList_003Eb__55_0(SimDealerCartelMember t)
		{
			return false;
		}

		internal bool _003CGetMemberCount_003Eb__58_0(SimDealerCartelMember t)
		{
			return false;
		}

		internal float _003CGetMostPopularDrugEmojiString_003Eb__70_0(SimDealerDrug x)
		{
			return 0f;
		}

		internal bool _003CGetEmojiForKingpin_003Eb__71_0(SimDealerCartelMember x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass28_0
	{
		public DataDealerCartelDrug drugData;

		public Predicate<SimDealerDrug> _003C_003E9__0;

		internal bool _003C_002Ector_003Eb__0(SimDealerDrug x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass72_0
	{
		public SimDealerDrug pureDrug;

		internal bool _003CRemovePureDrug_003Eb__0(SimDealerSupplyRun t)
		{
			return false;
		}
	}

	private string Name__BackingField;

	private double LastYearsProfit__BackingField;

	private double ThisYearsProfit__BackingField;

	private int HeroAgeAtLastAttacked__BackingField;

	private int NumTimeTakeDownRivalCartel__BackingField;

	private List<SimDealerDrug> _drugs;

	private List<SimDealerCartelMember> _members;

	private float _infamy;

	private float _moneyScore;

	private int _numAbsorbedStreetsFromRivalCartels;

	private List<SimDealerSupplyRun> _supplyRuns;

	private int _startingPureDrugCount;

	private float _removedDrugsProfits;

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

	public double LastYearsProfit
	{
		get
		{
			return LastYearsProfit__BackingField;
		}
		set
		{
			LastYearsProfit__BackingField = value;
		}
	}

	public double ThisYearsProfit
	{
		get
		{
			return ThisYearsProfit__BackingField;
		}
		set
		{
			ThisYearsProfit__BackingField = value;
		}
	}

	public int HeroAgeAtLastAttacked
	{
		get
		{
			return HeroAgeAtLastAttacked__BackingField;
		}
		set
		{
			HeroAgeAtLastAttacked__BackingField = value;
		}
	}

	public int NumTimeTakeDownRivalCartel
	{
		get
		{
			return NumTimeTakeDownRivalCartel__BackingField;
		}
		set
		{
			NumTimeTakeDownRivalCartel__BackingField = value;
		}
	}

	public void IncrementAge()
	{
	}

	private int GetMinYearlyPureDrugDoses()
	{
		return 0;
	}

	private int GetMaxYearlyPureDrugDoses()
	{
		return 0;
	}

	private int GetMinYearlyCutDoses()
	{
		return 0;
	}

	private int GetMaxYearlyCutDoses()
	{
		return 0;
	}

	private void RefreshAvailableDrugs()
	{
	}

	public void RefreshSupplyRuns()
	{
	}

	public List<SimDealerSupplyRun> GetSupplyRuns()
	{
		return null;
	}

	public void RemoveSupplyRun(SimDealerSupplyRun run)
	{
	}

	public float GetNewSalaryForMemberWithRank(DEALER_RANK rank)
	{
		return 0f;
	}

	public int GetNumberOfStreets()
	{
		return 0;
	}

	public int NumberOfMembersNeeded()
	{
		return 0;
	}

	public int NumberOfMembersNeededOfRank(DEALER_RANK rank)
	{
		return 0;
	}

	public void CompleteSetup(bool isRival = false)
	{
	}

	public float GetInfamy()
	{
		return 0f;
	}

	public float GetMoneyScore()
	{
		return 0f;
	}

	public SimDealerCartelMember GetKingpin()
	{
		return null;
	}

	public void AddMember(SimDealerCartelMember member)
	{
	}

	public void RemoveMember(SimDealerCartelMember member)
	{
	}

	public float GetAverageMemberRelationshipStrengthWithHero()
	{
		return 0f;
	}

	public List<SimDealerDrug> GetPureDrugsList()
	{
		return null;
	}

	public List<SimDealerDrugCut> GetDrugCutsList(bool onlyIncludeCutsCreatedByHero = false)
	{
		return null;
	}

	public List<SimDealerDrug> GetSellableDrugsList()
	{
		return null;
	}

	public string GetDrugsListString()
	{
		return null;
	}

	public void AdjustAvailableDosesOfPureDrug(string drugType, int doses)
	{
	}

	public List<SimDealerCartelMember> GetCartelMembersWithinRanks(List<DEALER_RANK> ranks)
	{
		return null;
	}

	public List<SimDealerCartelMember> GetMembersList(bool includeJailed)
	{
		return null;
	}

	public SimDealerCartelMember GetRandomMembersNotKingpin()
	{
		return null;
	}

	public List<SimDealerCartelMember> GetJailedMembersList()
	{
		return null;
	}

	public int GetMemberCount()
	{
		return 0;
	}

	public int GetPureDrugsCount()
	{
		return 0;
	}

	public SimDealerDrug GetRandomPureDrug(int dosesNeeded = 0)
	{
		return null;
	}

	public void ImpactInfamy(int impactScore)
	{
	}

	public void ImpactMoneyScore(int impactScore)
	{
	}

	public SimDealerStreet GetRandomAssignedStreet()
	{
		return null;
	}

	public void PromoteMember(SimDealerCartelMember member, DEALER_RANK newRank)
	{
	}

	public SimDealerCartelMember GetTopLieutenant()
	{
		return null;
	}

	public void AddAbsorbedStreets(int newStreets)
	{
	}

	public SimDealerCartelMember FillPositionViaPromotion(DEALER_RANK emptyRankToBeFilled)
	{
		return null;
	}

	public SimDealerCartelMember GetTopMemberOfRank(DEALER_RANK rank, bool avoidRecursiveCall = false)
	{
		return null;
	}

	public void RemoveDrugCut(SimDealerDrugCut cutDrug)
	{
	}

	public string GetMostPopularDrugEmojiString()
	{
		return null;
	}

	public string GetEmojiForKingpin()
	{
		return null;
	}

	public void RemovePureDrug(SimDealerDrug pureDrug)
	{
	}

	public void AddPureDrug(SimDealerDrug drug)
	{
	}

	public SimDealerCartelMember GetRandomMemberOfRank(DEALER_RANK rank, bool canBeInJail)
	{
		return null;
	}

	public SimDealerCartelMember GetRandomMember(bool canBeInJail, SimDealerCartelMember excludePerson = null)
	{
		return null;
	}

	public SimDealerCartelMember GetRandomJailedMember(SimDealerCartelMember excludeMember)
	{
		return null;
	}

	public int GetStartingPureDrugsCount()
	{
		return 0;
	}

	public void CollectProfit(double value)
	{
	}

	public int GetManufacturerRankUpAmountNeeded()
	{
		return 0;
	}
}
