using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SimSpecialJobDealer : SimSpecialJob
{
	private sealed class _003C_003Ec__DisplayClass104_0
	{
		public string name;

		internal bool _003CAddNewRivalCartel_003Eb__0(SimDealerCartel t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass108_0
	{
		public string drugType;

		internal bool _003CRemoveManufactureDrug_003Eb__0(SimDealerDrug t)
		{
			return false;
		}
	}

	private SimDealerCartel Cartel__BackingField;

	private List<SimDealerCartel> RivalCartels__BackingField;

	private DEALER_RANK Rank__BackingField;

	private string Nickname__BackingField;

	private SimDealerStreet AssignedStreet__BackingField;

	private int YearsInCartel__BackingField;

	private int YearsAtCurrentRank__BackingField;

	private int NumTimesThisYearInsultMakesStreetPersonLeave__BackingField;

	private int NumTimesThisYearAttackMakesStreetPersonLeave__BackingField;

	private int NumTimesThisYearMutinyAttempted__BackingField;

	private int NumSuccessfulSuppyRuns__BackingField;

	private int NumStreetsFullyControlled__BackingField;

	private List<string> DrugTypesAddedByHero__BackingField;

	private int AgeAtLastContribution__BackingField;

	private int AgeAtLastDealerPrisonRelease__BackingField;

	private List<SimDealerStreetPerson> DisgruntledStreetPersons__BackingField;

	private string LastTheftStreetPerson__BackingField;

	private float _infamy;

	private float _standing;

	private List<SimDealerDrug> _manufacturerAnnualDrugSupply;

	[NonSerialized]
	public List<Scenario> _DealerIncrementAgeScenarioQueueArray;

	public SimDealerCartel Cartel
	{
		get
		{
			return Cartel__BackingField;
		}
		set
		{
			Cartel__BackingField = value;
		}
	}

	public List<SimDealerCartel> RivalCartels
	{
		get
		{
			return RivalCartels__BackingField;
		}
		set
		{
			RivalCartels__BackingField = value;
		}
	}

	public DEALER_RANK Rank
	{
		get
		{
			return Rank__BackingField;
		}
		set
		{
			Rank__BackingField = value;
		}
	}

	public string Nickname
	{
		get
		{
			return Nickname__BackingField;
		}
		set
		{
			Nickname__BackingField = value;
		}
	}

	public SimDealerStreet AssignedStreet
	{
		get
		{
			return AssignedStreet__BackingField;
		}
		set
		{
			AssignedStreet__BackingField = value;
		}
	}

	public int YearsInCartel
	{
		get
		{
			return YearsInCartel__BackingField;
		}
		set
		{
			YearsInCartel__BackingField = value;
		}
	}

	public int YearsAtCurrentRank
	{
		get
		{
			return YearsAtCurrentRank__BackingField;
		}
		set
		{
			YearsAtCurrentRank__BackingField = value;
		}
	}

	public int NumTimesThisYearInsultMakesStreetPersonLeave
	{
		get
		{
			return NumTimesThisYearInsultMakesStreetPersonLeave__BackingField;
		}
		set
		{
			NumTimesThisYearInsultMakesStreetPersonLeave__BackingField = value;
		}
	}

	public int NumTimesThisYearAttackMakesStreetPersonLeave
	{
		get
		{
			return NumTimesThisYearAttackMakesStreetPersonLeave__BackingField;
		}
		set
		{
			NumTimesThisYearAttackMakesStreetPersonLeave__BackingField = value;
		}
	}

	public int NumTimesThisYearMutinyAttempted
	{
		get
		{
			return NumTimesThisYearMutinyAttempted__BackingField;
		}
		set
		{
			NumTimesThisYearMutinyAttempted__BackingField = value;
		}
	}

	public int NumSuccessfulSuppyRuns
	{
		get
		{
			return NumSuccessfulSuppyRuns__BackingField;
		}
		set
		{
			NumSuccessfulSuppyRuns__BackingField = value;
		}
	}

	public int NumStreetsFullyControlled
	{
		get
		{
			return NumStreetsFullyControlled__BackingField;
		}
		set
		{
			NumStreetsFullyControlled__BackingField = value;
		}
	}

	public List<string> DrugTypesAddedByHero
	{
		get
		{
			return DrugTypesAddedByHero__BackingField;
		}
		set
		{
			DrugTypesAddedByHero__BackingField = value;
		}
	}

	public int AgeAtLastContribution
	{
		get
		{
			return AgeAtLastContribution__BackingField;
		}
		set
		{
			AgeAtLastContribution__BackingField = value;
		}
	}

	public int AgeAtLastDealerPrisonRelease
	{
		get
		{
			return AgeAtLastDealerPrisonRelease__BackingField;
		}
		set
		{
			AgeAtLastDealerPrisonRelease__BackingField = value;
		}
	}

	public List<SimDealerStreetPerson> DisgruntledStreetPersons
	{
		get
		{
			return DisgruntledStreetPersons__BackingField;
		}
		set
		{
			DisgruntledStreetPersons__BackingField = value;
		}
	}

	public string LastTheftStreetPerson
	{
		get
		{
			return LastTheftStreetPerson__BackingField;
		}
		set
		{
			LastTheftStreetPerson__BackingField = value;
		}
	}

	public List<Scenario> DealerIncrementAgeScenarioQueueArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimSpecialJobDealer(SimDealerCartel cartel)
	{
	}

	public override void IncrementAge()
	{
	}

	public override string JobTitleWithArticle(bool article)
	{
		return null;
	}

	public static SimSpecialJobDealer GetCurrentSpecialJobDealer()
	{
		return null;
	}

	public static ArrayList GetAllHeroSpecialOccupationsAsDealer()
	{
		return null;
	}

	public void PromoteHeroToRank(DEALER_RANK rank)
	{
	}

	public bool QualifiesForPromotionToRank(DEALER_RANK newRank)
	{
		return false;
	}

	private void RefreshManufacturerDrugSupply()
	{
	}

	public List<SimDealerDrug> GetManufacturerAnnualDrugSupply()
	{
		return null;
	}

	public float GetInfamy()
	{
		return 0f;
	}

	public float GetStanding()
	{
		return 0f;
	}

	public void ImpactInfamy(int impactScore)
	{
	}

	public void ImpactStanding(int impactScore)
	{
	}

	public SimDealerCartelMember GetRandomRivalDealer()
	{
		return null;
	}

	public bool IsRivalCartelMember(SimDealerCartelMember member)
	{
		return false;
	}

	public bool IsMemberOfHeroCartel(SimDealerCartelMember member)
	{
		return false;
	}

	public SimDealerCartel GetRivalCartelForName(string name)
	{
		return null;
	}

	public List<SimDealerCartelMember> AbsorbRival(SimDealerCartel rivalCartel)
	{
		return null;
	}

	public static string GetRankStringForRank(DEALER_RANK rank, int gender, bool capitalize)
	{
		return null;
	}

	public static float GetMinAddictionLevelToBeConsideredAnAddict()
	{
		return 0f;
	}

	public static int AttemptsAllowedToDealNonDrugsPerPerson()
	{
		return 0;
	}

	public static int AttemptsAllowedToDealDrugsPerPerson()
	{
		return 0;
	}

	public static int MinHeroAgeToDealNonDrugItems()
	{
		return 0;
	}

	public static int MinAgeOfChildNeiceNephewsToDealNonDrugItems()
	{
		return 0;
	}

	public static int MinSiblingAgeToDealNonDrugItems()
	{
		return 0;
	}

	public static int RequiredNumberOfSuccessfulSupplyRunsToRankUp()
	{
		return 0;
	}

	public static void UpdateEmojiForAddictionLevelForPerson(SimPerson person, bool wasAddict, bool isAddict, bool isCop)
	{
	}

	public bool QualifiesForNewStreet()
	{
		return false;
	}

	public bool PureDrugSlotAvailable()
	{
		return false;
	}

	public void AddNewRivalCartel()
	{
	}

	public SimDealerStreetPerson GetRandomDisgruntledStreetPerson()
	{
		return null;
	}

	public static void CancelAllDealerScenarios()
	{
	}

	public void AddNewManufactureDrug(SimDealerDrug allocatedDrug)
	{
	}

	public void RemoveManufactureDrug(string drugType)
	{
	}
}
