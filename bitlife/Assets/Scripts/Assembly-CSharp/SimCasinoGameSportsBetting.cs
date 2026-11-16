using System;

[Serializable]
public class SimCasinoGameSportsBetting : SimCasinoGame
{
	private SPORT_TYPE SportType__BackingField;

	private string SportName__BackingField;

	private SimSportsTeam TeamA__BackingField;

	private SimSportsTeam TeamB__BackingField;

	private SimSportsTeam BetTeam__BackingField;

	private SimSportsTeam WinningTeam__BackingField;

	private int OddsTeamA__BackingField;

	private int OddsTeamB__BackingField;

	public SPORT_TYPE SportType
	{
		get
		{
			return SportType__BackingField;
		}
		set
		{
			SportType__BackingField = value;
		}
	}

	public string SportName
	{
		get
		{
			return SportName__BackingField;
		}
		set
		{
			SportName__BackingField = value;
		}
	}

	public SimSportsTeam TeamA
	{
		get
		{
			return TeamA__BackingField;
		}
		set
		{
			TeamA__BackingField = value;
		}
	}

	public SimSportsTeam TeamB
	{
		get
		{
			return TeamB__BackingField;
		}
		set
		{
			TeamB__BackingField = value;
		}
	}

	public SimSportsTeam BetTeam
	{
		get
		{
			return BetTeam__BackingField;
		}
		set
		{
			BetTeam__BackingField = value;
		}
	}

	public SimSportsTeam WinningTeam
	{
		get
		{
			return WinningTeam__BackingField;
		}
		set
		{
			WinningTeam__BackingField = value;
		}
	}

	public int OddsTeamA
	{
		get
		{
			return OddsTeamA__BackingField;
		}
		set
		{
			OddsTeamA__BackingField = value;
		}
	}

	public int OddsTeamB
	{
		get
		{
			return OddsTeamB__BackingField;
		}
		set
		{
			OddsTeamB__BackingField = value;
		}
	}

	public SimCasinoGameSportsBetting()
		: base(CASINO_GAME_TYPE.NONE)
	{
	}

	public override float GetSkippedGameWinnings(float lastBetAmount)
	{
		return 0f;
	}

	public override float GetSkippedGameLosses(float lastBetAmount)
	{
		return 0f;
	}

	public override bool SkippedGameShouldWin()
	{
		return false;
	}

	private int GetOddsForTeam(SimSportsTeam team)
	{
		return 0;
	}
}
