using System;
using System.Collections.Generic;

[Serializable]
public class SimMuseum
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimDisplayItem, bool> _003C_003E9__60_0;

		public static Func<SimDisplayItem, bool> _003C_003E9__67_0;

		public static Func<SimAuctionItem, bool> _003C_003E9__77_0;

		public static Func<SimDiscovery, bool> _003C_003E9__77_1;

		public static Func<SimDisplayItem, bool> _003C_003E9__79_0;

		public static Func<SimDisplayItem, double> _003C_003E9__82_0;

		public static Func<SimDisplayItem, int> _003C_003E9__83_0;

		internal bool _003CIncrementAge_003Eb__60_0(SimDisplayItem t)
		{
			return false;
		}

		internal bool _003CGetNumberOfExhibits_003Eb__67_0(SimDisplayItem t)
		{
			return false;
		}

		internal bool _003CHeroHasItemsForMuseum_003Eb__77_0(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CHeroHasItemsForMuseum_003Eb__77_1(SimDiscovery t)
		{
			return false;
		}

		internal bool _003CGetAllDisplayItems_003Eb__79_0(SimDisplayItem t)
		{
			return false;
		}

		internal double _003CGetMostValuableExhibit_003Eb__82_0(SimDisplayItem x)
		{
			return 0.0;
		}

		internal int _003CGetMuseumAverageAppealScore_003Eb__83_0(SimDisplayItem x)
		{
			return 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass87_0
	{
		public int iapType;

		internal bool _003CIsIAPItemAvailableToDisplay_003Eb__0(SimDisplayItem t)
		{
			return false;
		}
	}

	public float Att_Traffic;

	private string Name__BackingField;

	private int Age__BackingField;

	private int NumExhibitSlots__BackingField;

	private float TicketPrice__BackingField;

	private int TotalTicketsSold__BackingField;

	private int TotalTicketsSoldThisYear__BackingField;

	private int HeroAgeAtLastThief__BackingField;

	private bool HasReceivedBankruptcyWarning__BackingField;

	private bool Bankrupt__BackingField;

	private List<SimMuseumFloor> _Floors__BackingField;

	private float OpeningValue__BackingField;

	private SimLoan Mortgage__BackingField;

	private string StreetAddress__BackingField;

	private List<SimLoan> _WingLoans;

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

	public int NumExhibitSlots
	{
		get
		{
			return NumExhibitSlots__BackingField;
		}
		set
		{
			NumExhibitSlots__BackingField = value;
		}
	}

	public float TicketPrice
	{
		get
		{
			return TicketPrice__BackingField;
		}
		set
		{
			TicketPrice__BackingField = value;
		}
	}

	public int TotalTicketsSold
	{
		get
		{
			return TotalTicketsSold__BackingField;
		}
		set
		{
			TotalTicketsSold__BackingField = value;
		}
	}

	public int TotalTicketsSoldThisYear
	{
		get
		{
			return TotalTicketsSoldThisYear__BackingField;
		}
		set
		{
			TotalTicketsSoldThisYear__BackingField = value;
		}
	}

	public int HeroAgeAtLastThief
	{
		get
		{
			return HeroAgeAtLastThief__BackingField;
		}
		set
		{
			HeroAgeAtLastThief__BackingField = value;
		}
	}

	public bool HasReceivedBankruptcyWarning
	{
		get
		{
			return HasReceivedBankruptcyWarning__BackingField;
		}
		set
		{
			HasReceivedBankruptcyWarning__BackingField = value;
		}
	}

	public bool Bankrupt
	{
		get
		{
			return Bankrupt__BackingField;
		}
		set
		{
			Bankrupt__BackingField = value;
		}
	}

	private List<SimMuseumFloor> _Floors
	{
		get
		{
			return _Floors__BackingField;
		}
		set
		{
			_Floors__BackingField = value;
		}
	}

	public List<SimMuseumFloor> Floors
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public float OpeningValue
	{
		get
		{
			return OpeningValue__BackingField;
		}
		set
		{
			OpeningValue__BackingField = value;
		}
	}

	public SimLoan Mortgage
	{
		get
		{
			return Mortgage__BackingField;
		}
		set
		{
			Mortgage__BackingField = value;
		}
	}

	public string StreetAddress
	{
		get
		{
			return StreetAddress__BackingField;
		}
		set
		{
			StreetAddress__BackingField = value;
		}
	}

	public List<SimLoan> WingLoans
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public void IncrementAge()
	{
	}

	private bool ProcessLoan(SimLoan loan)
	{
		return false;
	}

	public void CloseMuseumAndReturnAllItems()
	{
	}

	public void ReturnAllDisplayItems()
	{
	}

	public float GetTrafficValue()
	{
		return 0f;
	}

	public double GetMonthlyExpense()
	{
		return 0.0;
	}

	public double GetMonthlyMortgageExpense()
	{
		return 0.0;
	}

	public int GetNumberOfExhibits()
	{
		return 0;
	}

	public static SimMuseum GenerateMuseum()
	{
		return null;
	}

	public int MortgageRejectionReason()
	{
		return 0;
	}

	public void OriginateMortgage()
	{
	}

	public double GetTotalValue(bool includeOpenValue = true)
	{
		return 0.0;
	}

	public SimMuseumWing GetMuseumWing(int floor, int wingIndex)
	{
		return null;
	}

	public bool MuseumWingIsOpen(int floor, int wingIndex)
	{
		return false;
	}

	public int GetTotalMuseumWings()
	{
		return 0;
	}

	public float GetWingExpansionCost()
	{
		return 0f;
	}

	public float GetAnnualRevenue()
	{
		return 0f;
	}

	public static bool HeroHasItemsForMuseum()
	{
		return false;
	}

	public SimDisplayItem GetRandomDisplayItem()
	{
		return null;
	}

	public List<SimDisplayItem> GetAllDisplayItems()
	{
		return null;
	}

	public void RemoveDisplayItem(SimDisplayItem item)
	{
	}

	public bool HasFirstFloorWingsCompleted()
	{
		return false;
	}

	public SimDisplayItem GetMostValuableExhibit()
	{
		return null;
	}

	public int GetMuseumAverageAppealScore()
	{
		return 0;
	}

	public int LoanRejectionReason(float purchaseValue)
	{
		return 0;
	}

	public void OriginateLoanForSalePrice(float cost, int typeLoan)
	{
	}

	public void OpenSecondFloor()
	{
	}

	public bool IsIAPItemAvailableToDisplay(int iapType)
	{
		return false;
	}

	public void ImpactTraffic(int impactScore)
	{
	}

	public static bool HasActiveMuseumLoanInAnotherCountry()
	{
		return false;
	}

	public static bool DoesIAPItemExistInAnotherMuseum(int itemType, int iapType = -1)
	{
		return false;
	}

	public static void RemoveItemFromDisplay(int floor, int wing, int displayIdx)
	{
	}

	public static SimPlace FindMuseumPlaceForItem(SimAuctionItem item)
	{
		return null;
	}

	public static void CancelAllMuseumScenarios()
	{
	}

	public static bool IsMuseumFull()
	{
		return false;
	}
}
