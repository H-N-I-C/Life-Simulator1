using Game;

public class GameFightingOutcomeActivity : ActivityLazyCsvBase
{
	public static class GameFightingOutcomeBalancing
	{
		public static int DopingChallengeChance;
	}

	private SimFightClubFighterStats HeroStats__BackingField;

	private SimFightClub Club__BackingField;

	private SimFightClubFighter Fighter__BackingField;

	private MiniGameDataFighting GameData__BackingField;

	private SimFightClubCoach Coach__BackingField;

	private readonly SimHero _hero;

	private readonly GameVars _gameVars;

	private readonly SimFightClubLeagueFighter _leagueFighter;

	private int ExperiencePointsAwarded__BackingField;

	private int StreetCredAwarded__BackingField;

	private double PrizeMoney__BackingField;

	private bool _heroWasOnSteroids;

	private bool _wasChampionBeforeFight;

	private int _heroPositionBeforeFight;

	private CoachFeedback _feedback;

	public SimFightClubFighterStats HeroStats
	{
		get
		{
			return HeroStats__BackingField;
		}
		private set
		{
			HeroStats__BackingField = value;
		}
	}

	public SimFightClub Club
	{
		get
		{
			return Club__BackingField;
		}
		private set
		{
			Club__BackingField = value;
		}
	}

	public SimFightClubFighter Fighter
	{
		get
		{
			return Fighter__BackingField;
		}
		private set
		{
			Fighter__BackingField = value;
		}
	}

	public MiniGameDataFighting GameData
	{
		get
		{
			return GameData__BackingField;
		}
		private set
		{
			GameData__BackingField = value;
		}
	}

	public SimFightClubCoach Coach
	{
		get
		{
			return Coach__BackingField;
		}
		private set
		{
			Coach__BackingField = value;
		}
	}

	public int ExperiencePointsAwarded
	{
		get
		{
			return ExperiencePointsAwarded__BackingField;
		}
		private set
		{
			ExperiencePointsAwarded__BackingField = value;
		}
	}

	public int StreetCredAwarded
	{
		get
		{
			return StreetCredAwarded__BackingField;
		}
		private set
		{
			StreetCredAwarded__BackingField = value;
		}
	}

	public double PrizeMoney
	{
		get
		{
			return PrizeMoney__BackingField;
		}
		private set
		{
			PrizeMoney__BackingField = value;
		}
	}

	public bool IsTutorial => false;

	public GameFightingOutcomeActivity(GameManager gameManager, MiniGameDataFighting gameData)
		: base(null)
	{
	}

	public CoachFeedback GetCoachFeedback()
	{
		return null;
	}

	private bool IsWin()
	{
		return false;
	}

	private void ApplyOutcome()
	{
	}

	private void ProcessWin()
	{
	}

	private void ProcessLose()
	{
	}

	private void HeroPostFightUpdate()
	{
	}

	public bool LeveledUpOrMoveUpgraded()
	{
		return false;
	}

	public int GetExperiencePointsAwarded()
	{
		return 0;
	}

	public int GetStreetCredAdjustment()
	{
		return 0;
	}

	public void Recap()
	{
	}

	private void OnRecapChoice(ChoiceSelection selection)
	{
	}

	private void TryPushPostLeagueScenario(FightClubPostLeagueData data)
	{
	}
}
