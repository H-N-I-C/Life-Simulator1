using System.Collections;
using Game;

public class FightClubInterviewActivity : ActivityLazyCsvBase
{
	private enum ScenarioId
	{
		FC_POST_LEAGUE_REPORTER_WIN = 0,
		FC_POST_LEAGUE_REPORTER_LOSE = 1,
		FC_POST_LEAGUE_REPORTER_LOST_FAIL = 2,
		FC_POST_LEAGUE_REPORTER_LOST_SUCCESS = 3,
		FC_POST_LEAGUE_REPORTER_WIN_SUCCESS = 4,
		FC_POST_LEAGUE_REPORTER_WIN_FAIL = 5,
		FC_POST_LEAGUE_REPORTER_LOST_SUCCESS_REWARDED = 6
	}

	private ScenarioId _currentScenarioId;

	private DataFightIngInterviewFanReaction FanReaction__BackingField;

	private FightClubPostLeagueData LeagueData__BackingField;

	private SimFightClub FightClub__BackingField;

	private DataFightIngLossReason LossReason__BackingField;

	private DataFightIngWinReason WinReason__BackingField;

	private SimLover Reporter__BackingField;

	public const int CredEarn = 10;

	private SimHero _hero;

	private readonly ChoiceRewardedVideo _rewardedVideo;

	public string CurrentScenarioId => null;

	public DataFightIngInterviewFanReaction FanReaction
	{
		get
		{
			return FanReaction__BackingField;
		}
		set
		{
			FanReaction__BackingField = value;
		}
	}

	public FightClubPostLeagueData LeagueData
	{
		get
		{
			return LeagueData__BackingField;
		}
		set
		{
			LeagueData__BackingField = value;
		}
	}

	public SimFightClub FightClub
	{
		get
		{
			return FightClub__BackingField;
		}
		set
		{
			FightClub__BackingField = value;
		}
	}

	public DataFightIngLossReason LossReason
	{
		get
		{
			return LossReason__BackingField;
		}
		set
		{
			LossReason__BackingField = value;
		}
	}

	public DataFightIngWinReason WinReason
	{
		get
		{
			return WinReason__BackingField;
		}
		set
		{
			WinReason__BackingField = value;
		}
	}

	public SimLover Reporter
	{
		get
		{
			return Reporter__BackingField;
		}
		set
		{
			Reporter__BackingField = value;
		}
	}

	public bool ShouldShowReasonList => false;

	public FightClubInterviewActivity(Life life, FightClubPostLeagueData data)
		: base(null)
	{
	}

	private SimLover GenerateReporter()
	{
		return null;
	}

	private void OnAnswerSelected(ChoiceSelection selection)
	{
	}

	private void PushLoserOutcome()
	{
	}

	private void ApplyLoserInterviewSuccessImpact()
	{
	}

	private void ApplyLoserInterviewRVSuccessImpact()
	{
	}

	private void PushWinnerOutcome()
	{
	}

	public void WatchedRewardedVideo()
	{
	}

	public void AddRewardedVideoChoice(ArrayList scenarioChoices)
	{
	}
}
