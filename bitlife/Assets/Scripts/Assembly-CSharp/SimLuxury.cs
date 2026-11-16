using System;
using System.Collections.Generic;

[Serializable]
public class SimLuxury
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimLuxuryIsland, double> _003C_003E9__69_0;

		public static Func<SimLuxuryIsland, double> _003C_003E9__70_0;

		internal double _003CGetMonthlyCosts_003Eb__69_0(SimLuxuryIsland t)
		{
			return 0.0;
		}

		internal double _003CGetIslandsTotalValue_003Eb__70_0(SimLuxuryIsland t)
		{
			return 0.0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass51_0
	{
		public string description;

		internal bool _003CRebuildJewelryForSale_003Eb__0(SimJewel t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass54_0
	{
		public SimCar car;

		internal bool _003CRebuildCarsForSale_003Eb__0(SimCar t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass57_0
	{
		public SimAircraft aircraft;

		internal bool _003CRebuildAircraftForSale_003Eb__0(SimAircraft t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass60_0
	{
		public SimWatercraft watercraft;

		internal bool _003CRebuildWatercraftForSale_003Eb__0(SimWatercraft t)
		{
			return false;
		}
	}

	private SimLuxurySecretSociety SecretSociety__BackingField;

	private SimCompany CarDealership__BackingField;

	private SimCompany AircraftDealership__BackingField;

	private SimCompany WatercraftDealership__BackingField;

	private int NumTimesAttendedVIPEvent__BackingField;

	private int LastAgeHeroOfferIsland__BackingField;

	private int LastAgeHeroWatchedCuriosityShopRV__BackingField;

	private List<SimLuxuryIsland> _islandsForSale;

	private List<SimLuxuryIsland> _ownedIslands;

	private List<SimLuxuryCharity> _charities;

	private List<SimLuxurySecretSociety> _availableSecretSocieties;

	private List<SimJewel> _jewelryForSale;

	private List<SimCar> _carsForSale;

	private List<SimAircraft> _aircraftForSale;

	private List<SimWatercraft> _watercraftForSale;

	private SimHeirloom _antiqueShopHeirloom;

	private int _heroAgeAtLastAntiqueHeirloomCreated;

	private List<string> _listRulesVoted;

	public SimLuxurySecretSociety SecretSociety
	{
		get
		{
			return SecretSociety__BackingField;
		}
		set
		{
			SecretSociety__BackingField = value;
		}
	}

	public SimCompany CarDealership
	{
		get
		{
			return CarDealership__BackingField;
		}
		set
		{
			CarDealership__BackingField = value;
		}
	}

	public SimCompany AircraftDealership
	{
		get
		{
			return AircraftDealership__BackingField;
		}
		set
		{
			AircraftDealership__BackingField = value;
		}
	}

	public SimCompany WatercraftDealership
	{
		get
		{
			return WatercraftDealership__BackingField;
		}
		set
		{
			WatercraftDealership__BackingField = value;
		}
	}

	public int NumTimesAttendedVIPEvent
	{
		get
		{
			return NumTimesAttendedVIPEvent__BackingField;
		}
		set
		{
			NumTimesAttendedVIPEvent__BackingField = value;
		}
	}

	public int LastAgeHeroOfferIsland
	{
		get
		{
			return LastAgeHeroOfferIsland__BackingField;
		}
		set
		{
			LastAgeHeroOfferIsland__BackingField = value;
		}
	}

	public int LastAgeHeroWatchedCuriosityShopRV
	{
		get
		{
			return LastAgeHeroWatchedCuriosityShopRV__BackingField;
		}
		set
		{
			LastAgeHeroWatchedCuriosityShopRV__BackingField = value;
		}
	}

	public void IncrementAge()
	{
	}

	public void JoinSociety(SimLuxurySecretSociety society)
	{
	}

	public void LeaveSociety(SimLuxurySecretSociety society)
	{
	}

	public List<SimLuxurySecretSociety> GetAvailableSecretSocieties()
	{
		return null;
	}

	public List<SimLuxuryCharity> GetCharities(bool forceRefresh = false)
	{
		return null;
	}

	public List<SimLuxuryIsland> GetIslandsForSale(bool forceRefresh = false)
	{
		return null;
	}

	public List<SimLuxuryIsland> GetOwnedIslands()
	{
		return null;
	}

	public void AddIsland(SimLuxuryIsland island)
	{
	}

	public void RemoveIsland(SimLuxuryIsland island)
	{
	}

	public List<SimJewel> GetJewelryForSale()
	{
		return null;
	}

	private void RebuildIslandForSale()
	{
	}

	private void RebuildJewelryForSale()
	{
	}

	public void RemoveJewelFromJewelryStore(SimJewel jewel)
	{
	}

	public List<SimCar> GetCarsForSale()
	{
		return null;
	}

	private void RebuildCarsForSale()
	{
	}

	public void RemoveCarFromDealership(SimCar car)
	{
	}

	public List<SimAircraft> GetAircraftForSale()
	{
		return null;
	}

	private void RebuildAircraftForSale()
	{
	}

	public void RemoveAircraftFromDealership(SimAircraft aircraft)
	{
	}

	public List<SimWatercraft> GetWatercraftForSale()
	{
		return null;
	}

	private void RebuildWatercraftForSale()
	{
	}

	public void RemoveWatercraftFromDealership(SimWatercraft watercraft)
	{
	}

	public SimHeirloom GetAntiqueShopHeirloomOrNull()
	{
		return null;
	}

	public void AssignAntiqueShopHeirloom(SimHeirloom anHeirloom)
	{
	}

	public void BuyAntiqueShopHeirloom()
	{
	}

	public static string GetVipEventNameForType(LUXURY_VIP_EVENT_TYPE eventType)
	{
		return null;
	}

	public static LUXURY_VIP_EVENT_TYPE GetVipEventNameForName(string eventType)
	{
		return LUXURY_VIP_EVENT_TYPE.NONE;
	}

	public static bool EligibleToPurchaseIAP()
	{
		return false;
	}

	public static float GetLuxuryRequirementProgress()
	{
		return 0f;
	}

	public double GetMonthlyCosts()
	{
		return 0.0;
	}

	public double GetIslandsTotalValue()
	{
		return 0.0;
	}

	public void AddRuleVoted(string directiveString)
	{
	}

	public bool HasAvailableRulesToVote()
	{
		return false;
	}

	public static double GetRequirementValue(bool higherLimit = false)
	{
		return 0.0;
	}
}
