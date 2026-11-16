public class MiniGameDataFighting
{
	private FIGHT_CLUB_FIGHTING_MINI_GAME_TYPE Type__BackingField;

	private FIGHT_CLUB_FIGHTING_MINI_GAME_OUTCOME Outcome__BackingField;

	private SimFightClubFighter NpcFighter__BackingField;

	private FIGHT_CLUB_FIGHTING_MINI_GAME_COACH_FEEDBACK CoachFeedback__BackingField;

	private SimFightClubBettingMatch BettingMatch__BackingField;

	private SimFightClubFighterStats FigtherOneStats__BackingField;

	private SimFightClubFighterStats FigtherTwoStats__BackingField;

	private BracketParser Parser__BackingField;

	public FIGHT_CLUB_FIGHTING_MINI_GAME_TYPE Type
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

	public FIGHT_CLUB_FIGHTING_MINI_GAME_OUTCOME Outcome
	{
		get
		{
			return Outcome__BackingField;
		}
		set
		{
			Outcome__BackingField = value;
		}
	}

	public SimFightClubFighter NpcFighter
	{
		get
		{
			return NpcFighter__BackingField;
		}
		set
		{
			NpcFighter__BackingField = value;
		}
	}

	public FIGHT_CLUB_FIGHTING_MINI_GAME_COACH_FEEDBACK CoachFeedback
	{
		get
		{
			return CoachFeedback__BackingField;
		}
		set
		{
			CoachFeedback__BackingField = value;
		}
	}

	public SimFightClubBettingMatch BettingMatch
	{
		get
		{
			return BettingMatch__BackingField;
		}
		set
		{
			BettingMatch__BackingField = value;
		}
	}

	public SimFightClubFighterStats FigtherOneStats
	{
		get
		{
			return FigtherOneStats__BackingField;
		}
		set
		{
			FigtherOneStats__BackingField = value;
		}
	}

	public SimFightClubFighterStats FigtherTwoStats
	{
		get
		{
			return FigtherTwoStats__BackingField;
		}
		set
		{
			FigtherTwoStats__BackingField = value;
		}
	}

	public BracketParser Parser
	{
		get
		{
			return Parser__BackingField;
		}
		set
		{
			Parser__BackingField = value;
		}
	}
}
