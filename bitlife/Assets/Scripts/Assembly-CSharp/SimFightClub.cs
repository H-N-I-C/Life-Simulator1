using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Game;

[Serializable]
public class SimFightClub
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimFightClubStreetFighter, bool> _003C_003E9__130_0;

		public static Func<SimFightClubLeagueFighter, bool> _003C_003E9__136_0;

		public static Func<DataFightIngMove, SimFightClubMove> _003C_003E9__150_0;

		public static Func<DataFightIngMove, SimFightClubMove> _003C_003E9__150_2;

		public static Func<SimFightClubStreetFighter, bool> _003C_003E9__188_0;

		public static Func<SimFightClubStreetFighter, bool> _003C_003E9__188_1;

		public static Func<SimFightClubStreetFighter, bool> _003C_003E9__188_2;

		internal bool _003CUpdateStreetFighters_003Eb__130_0(SimFightClubStreetFighter x)
		{
			return false;
		}

		internal bool _003CUpdateLeagueFighters_003Eb__136_0(SimFightClubLeagueFighter x)
		{
			return false;
		}

		internal SimFightClubMove _003CNewMoveViaTrainingOrNull_003Eb__150_0(DataFightIngMove x)
		{
			return null;
		}

		internal SimFightClubMove _003CNewMoveViaTrainingOrNull_003Eb__150_2(DataFightIngMove x)
		{
			return null;
		}

		internal bool _003CGetLeastRepresentedStyle_003Eb__188_0(SimFightClubStreetFighter f)
		{
			return false;
		}

		internal bool _003CGetLeastRepresentedStyle_003Eb__188_1(SimFightClubStreetFighter f)
		{
			return false;
		}

		internal bool _003CGetLeastRepresentedStyle_003Eb__188_2(SimFightClubStreetFighter f)
		{
			return false;
		}
	}

	public static int MAX_INJURIES;

	private SimFightClubFighterStats Stats__BackingField;

	private SimFightClubCoach Coach__BackingField;

	private bool IsRetired__BackingField;

	private bool HasJoined__BackingField;

	private int LastYearsLeaguePosition__BackingField;

	private bool BannedFromLeagueMatchesForOneYear__BackingField;

	private bool CompletedTutorial__BackingField;

	private int StreetFightsThisYear__BackingField;

	private int LeagueFightsThisYear__BackingField;

	private int DeclinedLeagueFightsThisYear__BackingField;

	private int NumTimesLookedForCoachThisYear__BackingField;

	private int MovesLearnedViaTrainingThisYear__BackingField;

	private int YearSinceLastTitleDefense__BackingField;

	private int TitleDefenses__BackingField;

	private int TitleWins__BackingField;

	private int CurrentWinStreak__BackingField;

	private int BetsLostThisYear__BackingField;

	private int BetsWonThisYear__BackingField;

	private int WinCount__BackingField;

	private int LossCount__BackingField;

	private bool ParticipatedInLeagueFight__BackingField;

	private int LastAgeHadFightInteraction__BackingField;

	private SimFightClubAgeUp _fightClubAgeUpScenarios;

	private List<SimFightClubStreetFighter> _streetFighters;

	private List<SimFightClubLeagueFighter> _leagueFighters;

	private int _heroAgeAtLastStreetFighterListRefresh;

	private double _streetCred;

	private int _leaguePosition;

	[NonSerialized]
	private FightClubLeaguePositionOrganizer _positionOrganizer;

	[NonSerialized]
	private List<DataFightRechargeMethod> _rechargeMethods;

	public const int NumberOfStreetFightersToRefreshOnAgeUp = 2;

	public SimFightClubFighterStats Stats
	{
		get
		{
			return Stats__BackingField;
		}
		set
		{
			Stats__BackingField = value;
		}
	}

	public SimFightClubCoach Coach
	{
		get
		{
			return Coach__BackingField;
		}
		set
		{
			Coach__BackingField = value;
		}
	}

	public bool IsRetired
	{
		get
		{
			return IsRetired__BackingField;
		}
		set
		{
			IsRetired__BackingField = value;
		}
	}

	public bool HasJoined
	{
		get
		{
			return HasJoined__BackingField;
		}
		private set
		{
			HasJoined__BackingField = value;
		}
	}

	public int LastYearsLeaguePosition
	{
		get
		{
			return LastYearsLeaguePosition__BackingField;
		}
		set
		{
			LastYearsLeaguePosition__BackingField = value;
		}
	}

	public bool BannedFromLeagueMatchesForOneYear
	{
		get
		{
			return BannedFromLeagueMatchesForOneYear__BackingField;
		}
		set
		{
			BannedFromLeagueMatchesForOneYear__BackingField = value;
		}
	}

	public bool CompletedTutorial
	{
		get
		{
			return CompletedTutorial__BackingField;
		}
		set
		{
			CompletedTutorial__BackingField = value;
		}
	}

	public int StreetFightsThisYear
	{
		get
		{
			return StreetFightsThisYear__BackingField;
		}
		set
		{
			StreetFightsThisYear__BackingField = value;
		}
	}

	public int LeagueFightsThisYear
	{
		get
		{
			return LeagueFightsThisYear__BackingField;
		}
		set
		{
			LeagueFightsThisYear__BackingField = value;
		}
	}

	public int DeclinedLeagueFightsThisYear
	{
		get
		{
			return DeclinedLeagueFightsThisYear__BackingField;
		}
		set
		{
			DeclinedLeagueFightsThisYear__BackingField = value;
		}
	}

	public int NumTimesLookedForCoachThisYear
	{
		get
		{
			return NumTimesLookedForCoachThisYear__BackingField;
		}
		set
		{
			NumTimesLookedForCoachThisYear__BackingField = value;
		}
	}

	public int MovesLearnedViaTrainingThisYear
	{
		get
		{
			return MovesLearnedViaTrainingThisYear__BackingField;
		}
		set
		{
			MovesLearnedViaTrainingThisYear__BackingField = value;
		}
	}

	public int YearSinceLastTitleDefense
	{
		get
		{
			return YearSinceLastTitleDefense__BackingField;
		}
		set
		{
			YearSinceLastTitleDefense__BackingField = value;
		}
	}

	public int TitleDefenses
	{
		get
		{
			return TitleDefenses__BackingField;
		}
		set
		{
			TitleDefenses__BackingField = value;
		}
	}

	public int TitleWins
	{
		get
		{
			return TitleWins__BackingField;
		}
		set
		{
			TitleWins__BackingField = value;
		}
	}

	public int CurrentWinStreak
	{
		get
		{
			return CurrentWinStreak__BackingField;
		}
		set
		{
			CurrentWinStreak__BackingField = value;
		}
	}

	public int BetsLostThisYear
	{
		get
		{
			return BetsLostThisYear__BackingField;
		}
		set
		{
			BetsLostThisYear__BackingField = value;
		}
	}

	public int BetsWonThisYear
	{
		get
		{
			return BetsWonThisYear__BackingField;
		}
		set
		{
			BetsWonThisYear__BackingField = value;
		}
	}

	public int WinCount
	{
		get
		{
			return WinCount__BackingField;
		}
		set
		{
			WinCount__BackingField = value;
		}
	}

	public int LossCount
	{
		get
		{
			return LossCount__BackingField;
		}
		set
		{
			LossCount__BackingField = value;
		}
	}

	public bool ParticipatedInLeagueFight
	{
		get
		{
			return ParticipatedInLeagueFight__BackingField;
		}
		set
		{
			ParticipatedInLeagueFight__BackingField = value;
		}
	}

	public int LastAgeHadFightInteraction
	{
		get
		{
			return LastAgeHadFightInteraction__BackingField;
		}
		set
		{
			LastAgeHadFightInteraction__BackingField = value;
		}
	}

	public SimFightClubAgeUp FightClubAgeUpScenarios
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public FightClubLeaguePositionOrganizer PositionOrganizer => null;

	public bool IsActiveAdultFighter => false;

	public List<DataFightRechargeMethod> RechargeMethods
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private void OnDeserialized(StreamingContext context)
	{
	}

	private bool HeroIsVampire()
	{
		return false;
	}

	private bool ShouldUseVampireFightClubRoster()
	{
		return false;
	}

	public void EnsureVampireCoachIfNeeded(SimFightClubCoach coach)
	{
	}

	private void EnsureVampireStreetFighterIfNeeded(SimFightClubStreetFighter fighter)
	{
	}

	private void EnsureNonVampireFighterIfNeeded(SimFightClubFighter fighter)
	{
	}

	public void SetupWithFightingType(FIGHT_CLUB_FIGHTING_STYLE style)
	{
	}

	public void IncrementAge()
	{
	}

	public void AddCoach(SimFightClubCoach coach)
	{
	}

	public bool IsHeroDisqualifiedFromLeague()
	{
		return false;
	}

	public string GetHeroLeagueStatusString(bool capitalize)
	{
		return null;
	}

	public void ApplyVampireTransformationRulesForFighter()
	{
	}

	public Scenario GetVampireDisqualificationPushScenario()
	{
		return null;
	}

	public void AddAutoReplacementCoach()
	{
	}

	public void ReassignCoachForNewCountry()
	{
	}

	public void CaughtUsingSteroids()
	{
	}

	public void SetLeaguePosition(int newPosition)
	{
	}

	public int GetLeaguePosition()
	{
		return 0;
	}

	public void SetNewLeaguePositionForFighter(SimFightClubLeagueFighter theFighter, int newPosition)
	{
	}

	public int GetLeaguePositionForFighter(SimFightClubLeagueFighter theFighter)
	{
		return 0;
	}

	public void PromoteLeaguePosition(int promotionSpots)
	{
	}

	public void DemoteLeaguePosition(int demotionSpots)
	{
	}

	public List<SimFightClubStreetFighter> GetStreetFighters()
	{
		return null;
	}

	public List<SimFightClubLeagueFighter> GetLeagueFighters()
	{
		return null;
	}

	private static void FixFighterGenderAtBirth<T>(List<T> fighters) where T : SimFightClubFighter
	{
	}

	private void EnsureRosterMatchesHeroGender()
	{
	}

	private static bool RosterGenderMismatchesHero<T>(List<T> fighters, int heroGender) where T : SimFightClubFighter
	{
		return false;
	}

	public void RefreshRostersForHeroGenderChange()
	{
	}

	public void UpdateStreetFighters(bool forceRebuild = false)
	{
	}

	private void RotateStreetFightersOnAgeUp()
	{
	}

	private static int CompareStreetFightersByDescendingDifficulty(SimFightClubStreetFighter left, SimFightClubStreetFighter right)
	{
		return 0;
	}

	private void AddNewStreetFighter()
	{
	}

	private void AddNewStreetFighter(FIGHT_CLUB_FIGHTING_STYLE style)
	{
	}

	private void AddNewStreetFighter(FIGHT_CLUB_FIGHTING_STYLE style, int level)
	{
	}

	public void UpdateLeagueFighters(bool forceRebuild = false)
	{
	}

	private void AddNewLeagueFighter()
	{
	}

	public bool ContainsStreetFighter(SimFightClubStreetFighter fighter)
	{
		return false;
	}

	public void RemoveFighter(SimFightClubFighter fighter)
	{
	}

	public void Retire()
	{
	}

	public void ComeOutOfRetirement()
	{
	}

	public double GetStreetCred()
	{
		return 0.0;
	}

	public void AdjustStreetCred(double amount)
	{
	}

	public void SetStreetCred(double amount)
	{
	}

	public void LevelUp()
	{
	}

	public List<SimFightClubMove> GetUndiscoveredMoves()
	{
		return null;
	}

	public void LearnMove(SimFightClubMove move)
	{
	}

	public void LearnMoves(IEnumerable<SimFightClubMove> moves)
	{
	}

	public float GetStatGainForTraining()
	{
		return 0f;
	}

	public SimFightClubMove NewMoveViaTrainingOrNull()
	{
		return null;
	}

	public static bool HasActiveAdultFighter()
	{
		return false;
	}

	public static List<SimFightClubSteroid> GetAllSteroids()
	{
		return null;
	}

	public static int GetRequiredNumberOfNPCLeagueFighters()
	{
		return 0;
	}

	public static int GetRequiredNumberOfNPCStreetFighters()
	{
		return 0;
	}

	public static string GetLeagueRankStringForPosition(int position, bool capitalize)
	{
		return null;
	}

	public static int GetExperiencePointsForAmateurFightWin(int winnersLevel, int losersLevel)
	{
		return 0;
	}

	public static int GetStreetCredRewardForAmateurFightWin(int winnersLevel, int losersLevel)
	{
		return 0;
	}

	public static double GetPrizeMoneyForLeagueFightWin(int winnersPosition, int losersPosition)
	{
		return 0.0;
	}

	public static double GetStreetCredRewardForLeagueFightWin(int winnersPosition, int losersPosition)
	{
		return 0.0;
	}

	public static int GetExperiencePointsForLeagueFightWin(int winnersPosition, int losersPosition)
	{
		return 0;
	}

	public static double GetStreetCredCostOfSteroids()
	{
		return 0.0;
	}

	public static int GetMinFightingAge()
	{
		return 0;
	}

	public static int GetMaxFighterLevel()
	{
		return 0;
	}

	public static int GetMoveSetCapacity()
	{
		return 0;
	}

	public static int MaxLeagueFightsPerYear()
	{
		return 0;
	}

	public static double GetMatchFixingCashOffer(double prizeForWinning)
	{
		return 0.0;
	}

	public static FIGHT_CLUB_STYLE_COMPARISON CompareFightingStyles(FIGHT_CLUB_FIGHTING_STYLE fighterAStyle, FIGHT_CLUB_FIGHTING_STYLE fighterBStyle)
	{
		return FIGHT_CLUB_STYLE_COMPARISON.NONE;
	}

	public static string GetStyleStringForFightingStyle(FIGHT_CLUB_FIGHTING_STYLE fightingStyle)
	{
		return null;
	}

	public static string GetStrongAgainstStyleStringForFightingStyle(FIGHT_CLUB_FIGHTING_STYLE fightingStyle)
	{
		return null;
	}

	public static string GetStrongAgainstStyleStringForMoveType(string moveType)
	{
		return null;
	}

	public static string GetMoveTypeForFightingStyle(FIGHT_CLUB_FIGHTING_STYLE fightingStyle)
	{
		return null;
	}

	public static FIGHT_CLUB_FIGHTING_STYLE GetFightingStyleForMoveType(string moveType)
	{
		return FIGHT_CLUB_FIGHTING_STYLE.NONE;
	}

	public static string GetBoxingGloveEmojiForFightingStyle(FIGHT_CLUB_FIGHTING_STYLE FightingStyle)
	{
		return null;
	}

	public static int GetMinimumLeaguePositionToChallengeChampion()
	{
		return 0;
	}

	public static int GetLastPlaceLeaguePosition()
	{
		return 0;
	}

	public static double GetLeagueFightLossPrizeAmount()
	{
		return 0.0;
	}

	public static int GetNewLeaguePositionForWinner(int winnersCurrentPosition, int losersCurrentPosition)
	{
		return 0;
	}

	public static int GetNewLeaguePositionForLoser(int winnersCurrentPosition, int losersCurrentPosition)
	{
		return 0;
	}

	public static FIGHT_CLUB_FIGHTING_STYLE GetRandomFightingStyle()
	{
		return FIGHT_CLUB_FIGHTING_STYLE.NONE;
	}

	private static List<FIGHT_CLUB_FIGHTING_STYLE> GetInitialStyleDistribution(int count)
	{
		return null;
	}

	private FIGHT_CLUB_FIGHTING_STYLE GetLeastRepresentedStyle()
	{
		return FIGHT_CLUB_FIGHTING_STYLE.NONE;
	}

	public static int GetNumberOfTimesCanLookForCoachPerYear()
	{
		return 0;
	}

	public static string GetAccuracyString(float accuracy)
	{
		return null;
	}

	public static CoachFeedback GetCoachFeedback(SimFightClubCoach coach, MiniGameDataFighting dataFighting)
	{
		return null;
	}

	public static double GetVampireTaxRate()
	{
		return 0.0;
	}

	public static double GetVampireTaxAmount(double earnings)
	{
		return 0.0;
	}

	public static float GetVampireFightEssenceCost()
	{
		return 0f;
	}

	public static List<string> GetLeagueBlacklistedBodyParts()
	{
		return null;
	}

	public static string GetMiniGameExteriorTitleForType(FIGHT_CLUB_FIGHTING_MINI_GAME_TYPE type, bool isChampionship)
	{
		return null;
	}

	public static bool FullMiniGameAllowed()
	{
		return false;
	}

	public static void LaunchFightingGame(MiniGameDataFighting gameDataFighting, Scenario currentScenario)
	{
	}

	public void HireNewCoach(SimFightClubCoach fightClubCoach)
	{
	}

	public void RecordTitleDefenseWin()
	{
	}

	public void RecordTitleDefenseLoss()
	{
	}

	public void RecordTitleWin()
	{
	}

	public void RecordFightWin()
	{
	}

	public void RecordFightLoss()
	{
	}

	public List<DataFightRechargeMethod> GetRechargeMethods()
	{
		return null;
	}

	private bool _003CNewMoveViaTrainingOrNull_003Eb__150_1(SimFightClubMove x)
	{
		return false;
	}

	private bool _003CNewMoveViaTrainingOrNull_003Eb__150_3(SimFightClubMove x)
	{
		return false;
	}
}
