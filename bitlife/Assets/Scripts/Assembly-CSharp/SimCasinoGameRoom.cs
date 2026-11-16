using System;

[Serializable]
public class SimCasinoGameRoom
{
	private CASINO_GAME_TYPE GameType__BackingField;

	private string Name__BackingField;

	private int InstalledUnits__BackingField;

	private float MinBetAmount__BackingField;

	private float HouseWinRate__BackingField;

	private float CurrentYearsWagers__BackingField;

	private float LastYearsWagers__BackingField;

	private float CurrentYearsHouseWins__BackingField;

	private float LastYearsHouseWins__BackingField;

	private int HeroAgeAtLastHouseRulesComplaint__BackingField;

	private int Age__BackingField;

	private int NumTimesCelebrityMeetAndGreetHeld__BackingField;

	private float _popularity;

	public CASINO_GAME_TYPE GameType
	{
		get
		{
			return GameType__BackingField;
		}
		set
		{
			GameType__BackingField = value;
		}
	}

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

	public int InstalledUnits
	{
		get
		{
			return InstalledUnits__BackingField;
		}
		set
		{
			InstalledUnits__BackingField = value;
		}
	}

	public float MinBetAmount
	{
		get
		{
			return MinBetAmount__BackingField;
		}
		set
		{
			MinBetAmount__BackingField = value;
		}
	}

	public float HouseWinRate
	{
		get
		{
			return HouseWinRate__BackingField;
		}
		set
		{
			HouseWinRate__BackingField = value;
		}
	}

	public float CurrentYearsWagers
	{
		get
		{
			return CurrentYearsWagers__BackingField;
		}
		set
		{
			CurrentYearsWagers__BackingField = value;
		}
	}

	public float LastYearsWagers
	{
		get
		{
			return LastYearsWagers__BackingField;
		}
		set
		{
			LastYearsWagers__BackingField = value;
		}
	}

	public float CurrentYearsHouseWins
	{
		get
		{
			return CurrentYearsHouseWins__BackingField;
		}
		set
		{
			CurrentYearsHouseWins__BackingField = value;
		}
	}

	public float LastYearsHouseWins
	{
		get
		{
			return LastYearsHouseWins__BackingField;
		}
		set
		{
			LastYearsHouseWins__BackingField = value;
		}
	}

	public int HeroAgeAtLastHouseRulesComplaint
	{
		get
		{
			return HeroAgeAtLastHouseRulesComplaint__BackingField;
		}
		set
		{
			HeroAgeAtLastHouseRulesComplaint__BackingField = value;
		}
	}

	public string NameRoom => null;

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

	public int NumTimesCelebrityMeetAndGreetHeld
	{
		get
		{
			return NumTimesCelebrityMeetAndGreetHeld__BackingField;
		}
		set
		{
			NumTimesCelebrityMeetAndGreetHeld__BackingField = value;
		}
	}

	public SimCasinoGameRoom(CASINO_GAME_TYPE gameType, int unitsToInstall)
	{
	}

	public void IncrementAge(SimCasino casino)
	{
	}

	private void SimulateAgeUpGames(SimCasino casino)
	{
	}

	public float GetPopularity()
	{
		return 0f;
	}

	public string GetGameName(bool capitalized = false)
	{
		return null;
	}

	public void AddUnits(int numberOfUnits, CASINO_TYPE casinoType)
	{
	}

	public static double GetBaseRoomPrice(CASINO_GAME_TYPE type)
	{
		return 0.0;
	}

	public static double GetCostPerUnit(CASINO_GAME_TYPE type)
	{
		return 0.0;
	}

	public static int GetUnitCapacity(CASINO_GAME_TYPE gameType, CASINO_TYPE casinoType)
	{
		return 0;
	}

	public static string GetUnitDescription(CASINO_GAME_TYPE type)
	{
		return null;
	}

	public void ImpactPopularity(int impactScore)
	{
	}

	public float GetAnnualCosts()
	{
		return 0f;
	}

	public float GetAnnualRevenue()
	{
		return 0f;
	}

	public float GetAnnualHouseWin()
	{
		return 0f;
	}
}
