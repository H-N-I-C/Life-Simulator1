using System;
using System.Collections.Generic;
using Game;

public class FightingLeagueActivity : ActivityLazyCsvBase
{
	public static class CommonBrackets
	{
		public const string FIGHTER_NAME = "<fighter-name>";

		public const string FIGHTER_NAME_ALT = "<fighter>";

		public const string FIGHTER_ARENA = "<fighting-arena>";

		public const string HERO_CURRENT_RANK = "<hero-current-rank>";

		public const string FIGHTER_CURRENT_RANK = "<fighter-current-rank>";

		public const string WINNER_PAYOUT_AMOUNT = "<winner-payout-amount>";

		public const string LOSER_PAYOUT_AMOUNT = "<loser-payout-amount>";

		public const string STREET_CRED_PAYOUT_AMOUNT = "<street-cred-payout-amount>";

		public const string EXPERIENCE_PAYOUT_AMOUNT = "<experience-payout-amount>";

		public const string CAREER_WINS = "<hero-career-wins>";

		public const string CAREER_LOSS = "<hero-career-losses>";

		public const string EXPERIENCE_EARNED = "<hero-experience-earned>";

		public const string EXPERIENCE = "<hero-experience>";

		public const string PREVIOUS_RANK = "<hero-previous-rank>";

		public const string FIGHT_RESULT = "<fight-result>";

		public const string FIGHTER_POSSESSIVE = "<fighter-possessive>";

		public const string TITLE_WINS = "<hero-title-wins>";

		public const string TITLE_DEFENSES = "<hero-title-defenses>";

		public const string WIN_STREAK = "<hero-win-streak>";

		public const string HERO_PENALTY_RANK = "<hero-penalty-rank>";
	}

	public static class PressConferenceBrackets
	{
		public const string PRESS_CONFERENCE_QUESTION = "<fighting-press-conference-question>";

		public const string PRESS_CONFERENCE_OUTLET = "<fighting-media-outlets>";

		public const string PRESS_CONFERENCE_CHOICE1 = "<press-choice-1>";

		public const string PRESS_CONFERENCE_CHOICE2 = "<press-choice-2>";

		public const string PRESS_CONFERENCE_CHOICE3 = "<press-choice-3>";

		public const string PRESS_CONFERENCE_CHOICE4 = "<press-choice-4>";

		public const string RANDOM_BUFF = "<random-buff>";

		public const string STAT_AFFECTED = "<random-stat>";

		public const string STAT_EFFECT = "<stat-effect>";
	}

	public static class FightFixingBrackets
	{
		public const string FIGHT_FIXING_NAME = "<fighting-bribe-amount>";

		public const string FIGHT_FIXING_DIVE = "<fighting-dive-scenario>";

		public const string FIGHT_FIXING_NARRATIVE = "<fighting-dive-scenario-log-version>";

		public const string FIGHT_FEE = "<fixed-fight-fee>";
	}

	public static class FightSponsorshipBrackets
	{
		public const string FIGHT_SPONSORSHIP_COMPANY = "<fighting-sponsorship-company>";

		public const string FIGHT_SPONSORSHIP_AMOUNT = "<sponsorship-amount>";

		public const string FIGHT_SPONSORSHIP_STAT1 = "<fighting-stat1>";

		public const string FIGHT_SPONSORSHIP_STAT2 = "<fighting-stat2>";

		public const string FIGHT_SPONSORSHIP_BONUS_AMOUNT = "<sponsorship-bonus-amount>";

		public const string WINNING_FEEDBACK = "<sponsor-won-feedback>";
	}

	public static class RecapBrackets
	{
		public const string COACH = "<coach-fighting>";

		public const string COACH_FEEDBACK = "<coach-fighting-feedback>";

		public const string COACH_FEEDBACK_LOG = "<coach-feedback-log-version>";
	}

	public static class FightingLeagueBalancing
	{
		public const int LOSER_PAYOUT_VALUE = 2000;

		public const int DRUG_SCREENING_PERCENT_CHANCE = 30;

		public const int LEAGUE_SPONSORSHIP_MIN_PERCENT = 10;

		public const int LEAGUE_SPONSORSHIP_MAX_PERCENT = 40;

		public const int MATCH_FIXING_PERCENT_CHANCE = 30;

		public const int PRESS_CONFERENCE_PERCENT_CHANCE = 70;

		public const int FIXED_FIGHT_FEE = -30000;

		public const int FIGHTER_RANK_DIFFERENCE = 3;

		public static bool FORCE_DRUG_SCREEN;

		public static bool FORCE_SPONSORSHIP;

		public static bool FORCE_FIX_MATCH;

		public static bool FORCE_PRESS_CONFERENCE;
	}

	public enum ScenarioId
	{
		FIGHTING_LEAGUE_PRO_FIGHTER_PROFILE = 0,
		FIGHTING_LEAGUE_PRESS_CONFERENCE = 1,
		FIGHTING_LEAGUE_MATCH_FIXING = 2,
		FIGHTING_LEAGUE_FIGHT_NIGHT = 3,
		FIGHTING_LEAGUE_SPONSORSHIP = 4,
		FIGHTING_LEAGUE_FIGHT_RECAP = 5,
		FIGHTING_LEAGUE_MATCH_FIXING_SUCCESS = 6,
		FIGHTING_LEAGUE_MATCH_FIXING_FAILURE = 7,
		FIGHTING_LEAGUE_MATCH_FIXING_FAILURE_NO_DEMOTION = 8,
		FIGHTING_LEAGUE_MATCH_MAX_REACHED = 9,
		FIGHTING_LEAGUE_MATCH_FIXING_COLOR = 10,
		FIGHTING_LEAGUE_MATCH_FIXING_OUTCOME = 11,
		FIGHTING_LEAGUE_MATCH_FIXING_OUTRAGE = 12,
		FIGHTING_LEAGUE_CHAMPION_CHALLENGE = 13,
		FIGHTING_LEAGUE_CHAMPION_CHALLENGE_FAILED_FEEDBACK = 14,
		FIGHTING_LEAGUE_CHAMPION_ALREADY_CHALLENGED_FEEDBACK = 15,
		FIGHTING_LEAGUE_PRESS_HERO_BUFFED_FEEDBACK = 16,
		FIGHTING_LEAGUE_PRESS_FIGHTER_BUFFED_FEEDBACK = 17,
		FIGHTING_LEAGUE_SPONSORSHIP_OUTCOME_FEEDBACK = 18,
		FIGHTING_LEAGUE_SPONSORSHIP_OUTCOME_FEEDBACK_WON = 19,
		FIGHTING_LEAGUE_RANKING_OUTCOME_FEEDBACK = 20,
		FIGHTING_LEAGUE_MATCH_FIXING_OUTRAGE_NO_DEMOTION = 21,
		FIGHTING_LEAGUE_NO_FIGHTERS_FEEDBACK = 22
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<FIGHT_CLUB_BUFF_TYPE, bool> _003C_003E9__88_0;

		public static Func<FIGHT_CLUB_BUFF_TYPE, bool> _003C_003E9__88_1;

		public static Func<SimFightClubLeagueFighter, bool> _003C_003E9__113_1;

		internal bool _003C_002Ector_003Eb__88_0(FIGHT_CLUB_BUFF_TYPE x)
		{
			return false;
		}

		internal bool _003C_002Ector_003Eb__88_1(FIGHT_CLUB_BUFF_TYPE x)
		{
			return false;
		}

		internal bool _003CFightNightChoiceMade_003Eb__113_1(SimFightClubLeagueFighter f)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass120_0
	{
		public List<SimFightClubLeagueFighter> excludeFighters;

		internal bool _003CGetLeagueFighter_003Eb__1(SimFightClubLeagueFighter f)
		{
			return false;
		}
	}

	private readonly SimFightClub _fightClub;

	private readonly SimHero _hero;

	private readonly SimFightClubCoach _coach;

	private SimFightClubLeagueFighter _opponent;

	private readonly SimPlace _heroPlaceOfResidence;

	private DataFightIngArenaName _fightingArena;

	private readonly DataFightPressConferenceMediaOutlet _mediaOutlet;

	private readonly DataFightPressConferenceQuestion _question;

	private readonly DataFightPressConferenceQuestionCombination[] _questionCombinations;

	private readonly DataFightIngDiveScenario _diveScenario;

	private readonly DataFightIngSponsorshipCompany _sponsorshipCompany;

	private readonly List<SimFightClubLeagueFighter> _previousFighters;

	private readonly double _sponsorCompanyOffer;

	private readonly FIGHT_CLUB_BUFF_TYPE _heroBuff;

	private readonly FIGHT_CLUB_BUFF_TYPE _opponentBuff;

	private readonly FIGHT_CLUB_BUFF_TYPE _heroDebuff;

	private readonly FIGHT_CLUB_BUFF_TYPE _opponentDebuff;

	private readonly FIGHT_CLUB_ATTRIBUTE _attribute1;

	private readonly int _increaseAmount;

	private readonly FIGHT_CLUB_ATTRIBUTE _attribute2;

	private readonly int _decreaseAmount;

	private readonly int _heroPreviousRank;

	private MiniGameDataFighting _fightResults;

	private CoachFeedback _coachGenericFeedback;

	private string _resolvedArenaName;

	private string _resolvedSponsorshipCompanyName;

	private double _winnerPayout;

	private double _streetCredPayout;

	private int _experiencePayout;

	private bool WasSponsored__BackingField;

	private int ExperiencePointsEarned__BackingField;

	private int StreetCredEarned__BackingField;

	private bool WonChampionship__BackingField;

	private int HeroPositionAfterFighting__BackingField;

	private bool FightWasFixed__BackingField;

	private bool HadPreFightScenario__BackingField;

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

	public FIGHT_CLUB_BUFF_TYPE HeroBuff => FIGHT_CLUB_BUFF_TYPE.NONE;

	public FIGHT_CLUB_BUFF_TYPE OpponentBuff => FIGHT_CLUB_BUFF_TYPE.NONE;

	public FIGHT_CLUB_BUFF_TYPE HeroDebuff => FIGHT_CLUB_BUFF_TYPE.NONE;

	public FIGHT_CLUB_BUFF_TYPE OpponentDebuff => FIGHT_CLUB_BUFF_TYPE.NONE;

	public FIGHT_CLUB_FIGHTING_MINI_GAME_OUTCOME FightOutcome => FIGHT_CLUB_FIGHTING_MINI_GAME_OUTCOME.NONE;

	public FIGHT_CLUB_ATTRIBUTE Attribute1 => FIGHT_CLUB_ATTRIBUTE.NONE;

	public FIGHT_CLUB_ATTRIBUTE Attribute2 => FIGHT_CLUB_ATTRIBUTE.NONE;

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

	public bool HadPreFightScenario
	{
		get
		{
			return HadPreFightScenario__BackingField;
		}
		set
		{
			HadPreFightScenario__BackingField = value;
		}
	}

	public SimFightClubFighterStats Stats => null;

	public SimFightClub FightClub => null;

	public SimFightClubLeagueFighter Opponent => null;

	public SimHero Hero => null;

	public int HeroPreviousRank => 0;

	private FightingLeagueActivity(Life life)
		: base(null)
	{
	}

	public FightingLeagueActivity(Life life, SimFightClubLeagueFighter opponent = null)
		: base(null)
	{
	}

	private void DetermineEntryScenario()
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

	public string GetSponsorshipCompanyName()
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

	public double GetFixedFightPayoutAmount()
	{
		return 0.0;
	}

	public int GetLoserPayoutAmount()
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

	public double PayoutAmount()
	{
		return 0.0;
	}

	public bool DidWin()
	{
		return false;
	}

	private double GetWinnerPayoutAmount()
	{
		return 0.0;
	}

	private CoachFeedback GetCoachFeedbackString()
	{
		return null;
	}

	public void SetFightOutcome(MiniGameDataFighting outcome)
	{
	}

	public void FillCommonBrackets(BracketParser parser)
	{
	}

	private DataFightIngArenaName GetFightingArena()
	{
		return null;
	}

	private DataFightPressConferenceMediaOutlet GetMediaOutlet()
	{
		return null;
	}

	private DataFightPressConferenceQuestion GetQuestion()
	{
		return null;
	}

	private DataFightIngDiveScenario GetFightDiveScenario()
	{
		return null;
	}

	private DataFightIngSponsorshipCompany GetSponsorshipCompany()
	{
		return null;
	}

	private double GetWinningSponsorBonus()
	{
		return 0.0;
	}

	private void FightNightChoiceMade(ChoiceSelection selection)
	{
	}

	private void SponsorshipChoiceMade(ChoiceSelection selection)
	{
	}

	private void ChangeStat(FIGHT_CLUB_ATTRIBUTE attribute, float amount)
	{
	}

	private void FixedMatchChoiceMade(ChoiceSelection selection)
	{
	}

	private void FixedFightOutrageChoiceMade(ChoiceSelection selection)
	{
	}

	private void PressConferenceChoiceMade(ChoiceSelection selection)
	{
	}

	private void ChallengeChoiceMade(ChoiceSelection choiceArguments)
	{
	}

	private SimFightClubLeagueFighter GetLeagueFighter(List<SimFightClubLeagueFighter> excludeFighters = null)
	{
		return null;
	}

	public void SponsorshipOutcomeDismissed()
	{
	}

	private void StartMiniGame()
	{
	}

	private void _003CFightNightChoiceMade_003Eb__113_0(LeagueDrugScreeningActivity.Outcome outcome, Scenario origin)
	{
	}

	internal static bool _003CGetLeagueFighter_003Eg__Between_007C120_0(int value, int min, int max)
	{
		return false;
	}
}
