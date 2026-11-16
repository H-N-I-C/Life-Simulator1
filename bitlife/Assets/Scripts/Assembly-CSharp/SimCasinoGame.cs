using System;

[Serializable]
public class SimCasinoGame
{
	private CASINO_GAME_TYPE Type__BackingField;

	private string Name__BackingField;

	private float HouseWinRate__BackingField;

	private CASINO_GAME_WIN_TYPE WinType__BackingField;

	public CASINO_GAME_TYPE Type
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

	public CASINO_GAME_WIN_TYPE WinType
	{
		get
		{
			return WinType__BackingField;
		}
		set
		{
			WinType__BackingField = value;
		}
	}

	public SimCasinoGame(CASINO_GAME_TYPE type)
	{
	}

	public static double GetStandardMinimumBetRequirementForGameType(CASINO_GAME_TYPE type)
	{
		return 0.0;
	}

	public static string GetNameStringForGameType(CASINO_GAME_TYPE type)
	{
		return null;
	}

	public static CASINO_GAME_TYPE GetGameTypeForNameString(string name)
	{
		return CASINO_GAME_TYPE.NONE;
	}

	public static SimCasinoGame CreateCasinoGameForGameType(CASINO_GAME_TYPE type)
	{
		return null;
	}

	public static Scenario CreateGameScenarioForSession(SimCasinoSession session, bool isHeroBetting = true)
	{
		return null;
	}

	public virtual float GetSkippedGameWinnings(float lastBetAmount)
	{
		return 0f;
	}

	public virtual float GetSkippedGameLosses(float lastBetAmount)
	{
		return 0f;
	}

	public virtual bool SkippedGameShouldWin()
	{
		return false;
	}

	public virtual void AssignSkippedGameSpecialWinType()
	{
	}
}
