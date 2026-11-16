using Game;

public class FightClubDefendChampionshipActivity : ActivityLazyCsvBase
{
	private readonly SimFightClub _fightClub;

	private readonly SimHero _hero;

	private readonly SimFightClubCoach _coach;

	private readonly SimFightClubLeagueFighter _opponent;

	private readonly DataFightIngArenaName _fightingArena;

	private readonly int _heroPreviousRank;

	private double _winnerPayout;

	private double _streetCredPayout;

	private int _experiencePayout;

	private FIGHT_CLUB_FIGHTING_MINI_GAME_OUTCOME _fightOutcome;

	private bool WasSponsored__BackingField;

	private int ExperiencePointsEarned__BackingField;

	private int StreetCredEarned__BackingField;

	private bool WonChampionship__BackingField;

	private int HeroPositionAfterFighting__BackingField;

	private bool FightWasFixed__BackingField;

	public bool WasSponsored
	{
		get
		{
			return WasSponsored__BackingField;
		}
		private set
		{
			WasSponsored__BackingField = value;
		}
	}

	public FIGHT_CLUB_FIGHTING_MINI_GAME_OUTCOME FightOutcome => FIGHT_CLUB_FIGHTING_MINI_GAME_OUTCOME.NONE;

	public int ExperiencePointsEarned
	{
		get
		{
			return ExperiencePointsEarned__BackingField;
		}
		set
		{
			ExperiencePointsEarned__BackingField = value;
		}
	}

	public int StreetCredEarned
	{
		get
		{
			return StreetCredEarned__BackingField;
		}
		set
		{
			StreetCredEarned__BackingField = value;
		}
	}

	public bool WonChampionship
	{
		get
		{
			return WonChampionship__BackingField;
		}
		set
		{
			WonChampionship__BackingField = value;
		}
	}

	public int HeroPositionAfterFighting
	{
		get
		{
			return HeroPositionAfterFighting__BackingField;
		}
		set
		{
			HeroPositionAfterFighting__BackingField = value;
		}
	}

	public bool FightWasFixed
	{
		get
		{
			return FightWasFixed__BackingField;
		}
		set
		{
			FightWasFixed__BackingField = value;
		}
	}

	public SimFightClubFighterStats Stats => null;

	public SimFightClubLeagueFighter Opponent => null;

	public SimHero Hero => null;

	public int HeroPreviousRank => 0;

	public FightClubDefendChampionshipActivity(Life life)
		: base(null)
	{
	}

	public string GetOpponentName()
	{
		return null;
	}

	public string GetArenaName()
	{
		return null;
	}

	public int GetHeroLeaguePosition()
	{
		return 0;
	}

	public int GetOpponentLeaguePosition()
	{
		return 0;
	}

	public double GetWinnerPayout()
	{
		return 0.0;
	}

	public double GetStreetCredPayoutAmount()
	{
		return 0.0;
	}

	public int GetExperiencePayoutAmount()
	{
		return 0;
	}

	public int GetLoserPayoutAmount()
	{
		return 0;
	}

	public void SetFightOutcome(FIGHT_CLUB_FIGHTING_MINI_GAME_OUTCOME outcome)
	{
	}

	public bool DidWin()
	{
		return false;
	}

	public void FillCommonBrackets(BracketParser parser)
	{
	}

	private void OnDefendChoiceMade(ChoiceSelection selection)
	{
	}

	private void StartMiniGame()
	{
	}

	private SimFightClubLeagueFighter GetContender()
	{
		return null;
	}

	private DataFightIngArenaName GetFightingArena()
	{
		return null;
	}

	private double GetWinnerPayoutAmount()
	{
		return 0.0;
	}
}
