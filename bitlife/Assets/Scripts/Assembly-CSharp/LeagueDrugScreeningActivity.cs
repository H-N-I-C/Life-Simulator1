using System;

public class LeagueDrugScreeningActivity : ActivityLazyCsvBase
{
	public enum ScenarioID
	{
		DRUG_SCREEN_REQUESTED = 0,
		DRUG_SCREEN_CAUGHT_CHEATING = 1,
		DRUG_SCREEN_PASSED = 2,
		DRUG_SCREEN_WITHDRAW = 3,
		DRUG_SCREEN_CAUGHT_CHEATING_LAST_PLACE = 4
	}

	public enum Outcome
	{
		Passed = 0,
		Failed = 1,
		Withdrawn = 2
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Action<SimFightClubFighter> _003C_003E9__20_3;

		public static Action<int> _003C_003E9__20_7;

		internal void _003C_002Ector_003Eb__20_3(SimFightClubFighter _)
		{
		}

		internal void _003C_002Ector_003Eb__20_7(int x)
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass20_0
	{
		public LeagueDrugScreeningActivity _003C_003E4__this;

		public SimFightClubFighter opponent;

		internal SimFightClubCoach _003C_002Ector_003Eb__0()
		{
			return null;
		}

		internal void _003C_002Ector_003Eb__1(SimFightClubCoach x)
		{
		}

		internal SimFightClubFighter _003C_002Ector_003Eb__2()
		{
			return null;
		}

		internal int _003C_002Ector_003Eb__4()
		{
			return 0;
		}

		internal void _003C_002Ector_003Eb__5(int x)
		{
		}

		internal int _003C_002Ector_003Eb__6()
		{
			return 0;
		}

		internal SimFightClubSteroid _003C_002Ector_003Eb__8()
		{
			return null;
		}

		internal void _003C_002Ector_003Eb__9(SimFightClubSteroid x)
		{
		}
	}

	private Action<Outcome, Scenario> m_OnOutcomeReached;

	private Scenario _eventOrigin;

	public const string DRUG_WEAR_OFF_TEMPLATE = "<br><br>The effects of your <fight-drug> have worn off.";

	private const int CAUGHT_DEMOTION_COUNT = 2;

	private readonly SimHero _hero;

	private readonly SimFightClub _fightClub;

	private readonly BracketVariable<SimFightClubCoach> _coach;

	private readonly BracketVariable<SimFightClubFighter> _fighterName;

	private readonly BracketVariable<int> _currentLeagueRank;

	private readonly BracketVariable<int> _caughtLeagueRank;

	private readonly BracketVariable<SimFightClubSteroid> _steroidUsed;

	private readonly ChoiceRewardedVideo _passUrineTest;

	public ChoiceRewardedVideo UrineTestVideo => null;

	public bool TakingSteroid => false;

	public event Action<Outcome, Scenario> OnOutcomeReached
	{
		add
		{
		}
		remove
		{
		}
	}

	public LeagueDrugScreeningActivity(Life life, SimFightClubFighter opponent)
		: base(null)
	{
	}

	private int GetDemotedLeaguePosition(int currentLeaguePosition)
	{
		return 0;
	}

	private string RefreshLeagueRankBracket(int leagueRank)
	{
		return null;
	}

	private string RefreshOpponentNameBracket(SimFightClubFighter fighter)
	{
		return null;
	}

	private string RefreshCoachNameBracket(SimFightClubCoach coach)
	{
		return null;
	}

	private string RefreshSteroidDrug(SimFightClubSteroid steroid)
	{
		return null;
	}

	private void DrugScreenChoiceSelected(ChoiceSelection choiceSelection)
	{
	}

	private void PassedFeedbackDismissed()
	{
	}

	private void FailedFeedbackDismissed()
	{
	}

	private void WithdrawFeedbackDismissed()
	{
	}
}
