using System;
using System.Collections.Generic;

[Serializable]
public class SimRaceCar
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimRaceCarMod, string> _003C_003E9__23_0;

		public static Func<string, string, string> _003C_003E9__23_1;

		internal string _003CGetModsListString_003Eb__23_0(SimRaceCarMod t)
		{
			return null;
		}

		internal string _003CGetModsListString_003Eb__23_1(string i, string j)
		{
			return null;
		}
	}

	private SimCar _simCar;

	private SimAuctionItem _blackMarketCar;

	public SimRaceCar()
	{
	}

	public SimRaceCar(SimCar car)
	{
	}

	public SimRaceCar(string carGrade, RACING_TRACK_CATEGORY category, CAR_TYPE carType = (CAR_TYPE)0)
	{
	}

	public SimRaceCar(SimAuctionItem car)
	{
	}

	public void IncrementAge()
	{
	}

	private void IncrementAgeBMCar(SimAuctionItem blackMarketCar)
	{
	}

	private void IncrementAgeSimCar(SimCar simCar)
	{
	}

	private void IncrementAgeNPCOwned()
	{
	}

	public double GetCarValue()
	{
		return 0.0;
	}

	public string DescriptionString()
	{
		return null;
	}

	public string GetEmojiString()
	{
		return null;
	}

	public string GetMake()
	{
		return null;
	}

	public string GetModel()
	{
		return null;
	}

	public string GetMakeAndModel()
	{
		return null;
	}

	public string GetTypeString()
	{
		return null;
	}

	private string GetTypeStringForBlackMarketCar(SimAuctionItem car)
	{
		return null;
	}

	public string GetRacingClass()
	{
		return null;
	}

	public string GetRacingCategoryList()
	{
		return null;
	}

	public int GetRacesWonCount()
	{
		return 0;
	}

	public int GetRacesLostCount()
	{
		return 0;
	}

	public List<SimRaceCarMod> GetModsList()
	{
		return null;
	}

	public string GetModsListString()
	{
		return null;
	}

	public int GetNumberOfOpenModSlots()
	{
		return 0;
	}

	public SimRaceCarDetails RaceCarDetails()
	{
		return null;
	}

	public float GetSpeed()
	{
		return 0f;
	}

	public float GetAcceleration()
	{
		return 0f;
	}

	public float GetHandling()
	{
		return 0f;
	}

	public float GetDurability()
	{
		return 0f;
	}

	public float GetSpecificStatForRaceCarAttribute(string attribute)
	{
		return 0f;
	}

	public SimCar GetSimCarOrNull()
	{
		return null;
	}

	public SimAuctionItem GetBlackMarketCarOrNull()
	{
		return null;
	}

	public float GetCondition()
	{
		return 0f;
	}

	public void ImpactCondition(int impact)
	{
	}

	public void AddMod(SimRaceCarMod mod)
	{
	}

	public void RemoveMod(SimRaceCarMod mod)
	{
	}

	public bool MatchesSimCar(SimCar car)
	{
		return false;
	}

	public bool MatchesBlackmarketCar(SimAuctionItem car)
	{
		return false;
	}

	public SimCarRepair GetNeededCarRepairOrNull(bool ignoreGoldenWrench = false)
	{
		return null;
	}

	public void PerformAnyNeededRepairs()
	{
	}

	public void PerformAnyNeededMaintenance()
	{
	}

	public int GetAge()
	{
		return 0;
	}

	public void RecordRaceResult(bool won)
	{
	}

	public void ResetRaceResults()
	{
	}

	public void SetCondition(float condition)
	{
	}

	public string GetLowestCarStat()
	{
		return null;
	}

	public string GetHighestCarStat()
	{
		return null;
	}

	public void SetGivenFromIAP()
	{
	}

	public bool IsCategoryCompatible(RACING_TRACK_CATEGORY category)
	{
		return false;
	}

	public void SetNeededCarRepair(CAR_REPAIR_TYPE repairType)
	{
	}

	public string GetColorString()
	{
		return null;
	}

	public float RepairCostForAccidentSeriousness(int score)
	{
		return 0f;
	}
}
