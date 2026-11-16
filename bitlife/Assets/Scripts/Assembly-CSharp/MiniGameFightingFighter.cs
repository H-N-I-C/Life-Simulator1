using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MiniGameFightingFighter : MonoBehaviour
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimFightClubMove, float> _003C_003E9__82_0;

		public static TweenCallback _003C_003E9__137_0;

		public static TweenCallback _003C_003E9__144_0;

		public static TweenCallback _003C_003E9__144_1;

		internal float _003CInitWithParameters_003Eb__82_0(SimFightClubMove x)
		{
			return 0f;
		}

		internal void _003CPlayAnimationWinnerChampionshipBeltSequence_003Eb__137_0()
		{
		}

		internal void _003CPlayAnimationSquishFighterSequence_003Eb__144_0()
		{
		}

		internal void _003CPlayAnimationSquishFighterSequence_003Eb__144_1()
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass111_0
	{
		public string moveName;

		internal bool _003CGetLastYearsMoveSetReuseCount_003Eb__0(SimFightClubMove x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass132_0
	{
		public MiniGameFightingFighter _003C_003E4__this;

		public float distance;

		public float duration;

		internal void _003CPlayAnimationIdleDanceSequence_003Eb__0()
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass134_0
	{
		public Vector2 newStartPos;

		public int distance;

		public float multiplier;

		public Sequence gloveSequence;

		public GameObject gloveObject;

		public float duration;

		public bool leftGlove;

		public MiniGameFightingFighter _003C_003E4__this;

		internal void _003CPlayAnimationGloveRotateSequence_003Eb__0()
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass141_0
	{
		public MiniGameFightingFighter _003C_003E4__this;

		public int hpLoss;

		public FIGHTING_MOVE_RESULT moveResult;

		internal void _003CPlayAnimationReceiveKnockoutSequence_003Eb__0()
		{
		}

		internal void _003CPlayAnimationReceiveKnockoutSequence_003Eb__1()
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass142_0
	{
		public MiniGameFightingFighter _003C_003E4__this;

		public Vector2 finalGlovePosition;

		internal void _003CPlayAnimationKnockoutGlovesFlySequence_003Eb__0()
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass145_0
	{
		public MiniGameFightingFighter _003C_003E4__this;

		public FIGHTING_ANIMATION_POWER_LEVEL powerLevel;

		public int hpLoss;

		public FIGHTING_MOVE_RESULT moveResult;

		internal void _003CPlayAnimationReceiveHitSequence_003Eb__0()
		{
		}

		internal void _003CPlayAnimationReceiveHitSequence_003Eb__1()
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass164_0
	{
		public SimFightClubMove move;

		internal bool _003CGetNumberOfTimesMoveUsed_003Eb__0(SimFightClubMove x)
		{
			return false;
		}
	}

	private Vector2 FinalPosition__BackingField;

	private VoltronFaceContainerView _voltronFace;

	private GameObject _subContainerObject;

	private GameObject _faceObject;

	private GameObject _leftGloveObject;

	private GameObject _rightGloveObject;

	private Vector2 _size;

	private SimPerson _person;

	private int _currentHp;

	private int _startingHp;

	private int _staminaRemaining;

	private SimFightClubFighterStats _stats;

	private string _shortName;

	private MiniGameFighting _miniGame;

	private bool _isHero;

	private bool _isFirstFighter;

	private MiniGameFightingFighterStats _statsDisplay;

	private MiniGameFightingBettingFighterStats _bettingStatsDisplay;

	private Sequence _idleDanceSequence;

	private Sequence _leftGloveRotateSequence;

	private Sequence _rightGloveRotateSequence;

	private Sequence _winnerJumpSequence;

	private Sequence _knockoutGlovesFlySequence;

	private Vector2 _leftGloveStartPos;

	private Vector2 _rightGloveStartPos;

	private List<SimFightClubMove> _recordedMoves;

	private int _criticalHitCount;

	private bool _lastMoveWasCritizalHit;

	private int _numMovesSinceLastHit;

	private bool _queueFatigue;

	private VOLTRON_MOOD _mood;

	private int _moveAttemptDodgedCount;

	private int _moveAttemptBlockedCount;

	private int _moveAttemptSuccessCount;

	private int _moveAttemptDisadvantagedCount;

	private int _illegalMoveCount;

	private bool _isTutorial;

	private bool _isBettingFight;

	private float _strengthInfluence;

	private float _techniqueInfluence;

	private Dictionary<SimFightClubMove, float> _moveInfluences;

	private Vector2 _finalWinningPosition;

	private bool _lastChanceSaveTriggered;

	private List<SimFightClubMove> _moveSet;

	private const int GloveRotateDistanceSlow = 5;

	private const int GloveRotateDistanceFast = 8;

	private const int GloveRotateDistanceWinner = 10;

	private const float GloveRotateDurationSlow = 0.95f;

	private const float GloveRotateDurationFast = 0.75f;

	private const float GloveRotateDurationWinner = 0.2f;

	private const float GloveRotateDurationFirstFighterAdjustment = 0.03f;

	private const float GloveRotateDurationNoStaminaMulitplier = 2f;

	private const int GloveRotateDistanceNoStamina = 3;

	private const float DanceDistanceSlow = 4f;

	private const float DanceDistanceFast = 25f;

	private const float DanceDistanceNoStamina = 2.66f;

	private const float DanceDurationNoStaminaMultiplier = 1.5f;

	private const int DanceDurationLowerLimitSlow = 59;

	private const int DanceDurationUpperLimitSlow = 62;

	private const int DanceDurationLowerLimitFast = 50;

	private const int DanceDurationUpperLimitFast = 54;

	private const int StatBaseline = 36;

	private const float BaseMovePower = 10f;

	private const float NoStaminaPowerMultiplier = 0.25f;

	private const int MinimumPower = 3;

	private const float CriticalHpMultiplier = 2f;

	private const float IllegalHpHitMultiplier = 1.5f;

	private const int ChampionshipCenterVertOffset = -50;

	private const int FighterSize = 400;

	private const float VoltronFaceReduction = 0.9f;

	private const float GlovePositionMultiplier = 0.4f;

	private const int GloveToBeltOffset = 100;

	private const int BeltWeightDropDistance = 50;

	private const int BeltLiftDistance = 215;

	private const int TutorialHeroHpBottomCap = 5;

	private const float TutorialHeroHpHitLimiter = 0.25f;

	public Vector2 FinalPosition
	{
		get
		{
			return FinalPosition__BackingField;
		}
		set
		{
			FinalPosition__BackingField = value;
		}
	}

	public SimPerson Person => null;

	private void OnDestroy()
	{
	}

	public bool InitWithParameters(SimPerson fighter, SimFightClubFighterStats stats, MiniGameFighting minigame, bool isFighterOne)
	{
		return false;
	}

	public void BuildHp()
	{
	}

	private bool BuildComponents()
	{
		return false;
	}

	private bool SetupFace()
	{
		return false;
	}

	private bool SetupGloves()
	{
		return false;
	}

	private GameObject SetupGlove(bool isLeftGlove)
	{
		return null;
	}

	private bool SetupStats()
	{
		return false;
	}

	public void FixSortOrder()
	{
	}

	private bool IsPreferredOutcomeActive()
	{
		return false;
	}

	private bool IsPreferredWinner()
	{
		return false;
	}

	public bool IsHero()
	{
		return false;
	}

	private int GetStaminaRemaining()
	{
		return 0;
	}

	public float GetOriginalStrength()
	{
		return 0f;
	}

	private float GetStrength()
	{
		return 0f;
	}

	private float GetToughness(bool ramped = true)
	{
		return 0f;
	}

	public Vector2 GetFaceSize()
	{
		return default;
	}

	public FIGHT_CLUB_FIGHTING_STYLE GetFightingStyle()
	{
		return FIGHT_CLUB_FIGHTING_STYLE.NONE;
	}

	public float GetAgility()
	{
		return 0f;
	}

	public float GetOriginalTechnique()
	{
		return 0f;
	}

	private float GetTechnique()
	{
		return 0f;
	}

	public string GetShortName()
	{
		return null;
	}

	public string GetFullName()
	{
		return null;
	}

	public int GetGender()
	{
		return 0;
	}

	public int GetEffectiveGender()
	{
		return 0;
	}

	public string GetPronoun(PRONOUN_TYPE type)
	{
		return null;
	}

	public int GetIllegalMoveCount()
	{
		return 0;
	}

	public int GetLevel()
	{
		return 0;
	}

	public int GetStatScore()
	{
		return 0;
	}

	public int GetNumberOfMovesSinceLastHit()
	{
		return 0;
	}

	public int GetLastYearsMoveSetReuseCount()
	{
		return 0;
	}

	private float GetLastYearsMoveReuseEffectivenessPunishmentPercentage()
	{
		return 0f;
	}

	public bool IsKnockedOut()
	{
		return false;
	}

	public int GetCurrentHp()
	{
		return 0;
	}

	private int GetStartingHp()
	{
		return 0;
	}

	public List<SimFightClubMove> GetMoveSet()
	{
		return null;
	}

	private int GetPercentageHpRemaining()
	{
		return 0;
	}

	public SimFightClubMove GetMoveForIndex(int index)
	{
		return null;
	}

	public bool UsedSameMovesTooMuch()
	{
		return false;
	}

	public bool UsedDisadvantagedMovesTooMuch()
	{
		return false;
	}

	public int GetDisadvantagedMoveCount()
	{
		return 0;
	}

	private float GetInfluenceForMove(SimFightClubMove move)
	{
		return 0f;
	}

	public void InfluenceStrength(float influence)
	{
	}

	public void InfluenceTechnique(float influence)
	{
	}

	public void InfluenceMove(SimFightClubMove move, float influence)
	{
	}

	public void SyncStamina()
	{
	}

	public void FatigueCheck()
	{
	}

	private void AdjustStamina(int amount, float duration)
	{
	}

	public void AdjustHp(int amount, float duration)
	{
	}

	private static float RampStat(float stat)
	{
		return 0f;
	}

	private void PlayInitialSlowDanceSequence()
	{
	}

	public void PlayAnimationIdleDanceSequence(bool slow)
	{
	}

	public void PlayAnimationBothGlovesRotateSequence(bool slow)
	{
	}

	private void PlayAnimationGloveRotateSequence(bool leftGlove, int distance, float duration)
	{
	}

	public Sequence GetAnimationStatBarRevealSequence(Vector2 finalFighterPos)
	{
		return null;
	}

	public void PlayAnimationWinnerSequence()
	{
	}

	private void PlayAnimationWinnerChampionshipBeltSequence()
	{
	}

	private void PlayAnimationWinnerFistPumpSequence()
	{
	}

	private void PlayAnimationWinnerJumpSequence()
	{
	}

	private void PlayAnimationAttackSequence(FIGHTING_MOVE_RESULT result, float shakeStrength, float shakeDuration)
	{
	}

	private void PlayAnimationReceiveKnockoutSequence(FIGHTING_MOVE_RESULT moveResult, float delay, int hpLoss)
	{
	}

	private void PlayAnimationKnockoutGlovesFlySequence(float delay)
	{
	}

	private void PlayAnimationGlovesDetachAndFallSequence(Vector2 finalGlovePosition)
	{
	}

	private void PlayAnimationSquishFighterSequence(float delay, float duration)
	{
	}

	private void PlayAnimationReceiveHitSequence(FIGHTING_MOVE_RESULT moveResult, FIGHTING_ANIMATION_POWER_LEVEL powerLevel, float delay, int hpLoss)
	{
	}

	private void PlayAnimationBlockedHitSequence(float delay)
	{
	}

	private void PlayAnimationBlockedHitReactionSequence(float delay)
	{
	}

	private void PlayAnimationDodgedHitSequence(float delay)
	{
	}

	public float PlayAnimationGlovesBlockSequence(float delay)
	{
		return 0f;
	}

	private static FIGHTING_ANIMATION_POWER_LEVEL GetAnimationPowerLevelForPower(float power)
	{
		return FIGHTING_ANIMATION_POWER_LEVEL.NONE;
	}

	private static float GetAnimationPowerLevelShakeStrength(FIGHTING_ANIMATION_POWER_LEVEL powerLevel)
	{
		return 0f;
	}

	private static float GetAnimationPowerLevelReactDistance(FIGHTING_ANIMATION_POWER_LEVEL powerLevel)
	{
		return 0f;
	}

	private static float GetAnimationPowerLevelReactAngle(FIGHTING_ANIMATION_POWER_LEVEL powerLevel)
	{
		return 0f;
	}

	private static float GetAnimationPowerLevelReactScale(FIGHTING_ANIMATION_POWER_LEVEL powerLevel)
	{
		return 0f;
	}

	private static float GetAnimationPowerLevelShakeDuration(FIGHTING_ANIMATION_POWER_LEVEL powerLevel)
	{
		return 0f;
	}

	private static void ImpactForPowerLevel(FIGHTING_ANIMATION_POWER_LEVEL powerLevel)
	{
	}

	public SimFightClubMove GenerateMoveAgainstFighter(MiniGameFightingFighter destFighter)
	{
		return null;
	}

	private bool ShouldMakeSmartMove()
	{
		return false;
	}

	private bool CouldKnockoutFighter(MiniGameFightingFighter fighter)
	{
		return false;
	}

	private int GetHpLossForMoveAgainstFighter(MiniGameFightingFighter fighter, SimFightClubMove move)
	{
		return 0;
	}

	private List<SimFightClubMove> GetMovesOrderedByHpLoss(MiniGameFightingFighter destFighter)
	{
		return null;
	}

	public void AttackWithMove(MiniGameFightingFighter destFighter, SimFightClubMove move)
	{
	}

	private static int GetMoveStaminaCost(SimFightClubMove move)
	{
		return 0;
	}

	public int GetNumberOfTimesMoveUsed(SimFightClubMove move)
	{
		return 0;
	}

	private void RecordMove(SimFightClubMove move)
	{
	}

	private SimFightClubMove GetLastMove()
	{
		return null;
	}

	public int GetRecordedMoveCount()
	{
		return 0;
	}

	private SimFightClubMove GetRandomMove(bool excludePreviousMove, bool staminaRequired)
	{
		return null;
	}

	private int DetermineAccuracyOfMove(MiniGameFightingFighter destFighter, SimFightClubMove move)
	{
		return 0;
	}

	private FIGHTING_MOVE_RESULT DetermineMoveResult(MiniGameFightingFighter destFighter, SimFightClubMove move)
	{
		return FIGHTING_MOVE_RESULT.NONE;
	}

	private bool ShouldBeCriticalHit(MiniGameFightingFighter destFighter, SimFightClubMove move)
	{
		return false;
	}

	private bool NeedsToCatchUp(MiniGameFightingFighter destFighter, int moveCount)
	{
		return false;
	}

	private bool HasExceededRecommendedMovesForLevel(int moveCount)
	{
		return false;
	}

	private float DeterminePowerOfMove(MiniGameFightingFighter destFighter, SimFightClubMove move)
	{
		return 0f;
	}

	private int DetermineHpLossForMovePower(float movePower, FIGHTING_MOVE_RESULT result)
	{
		return 0;
	}

	private float GetLevelHpMultiplier()
	{
		return 0f;
	}

	private void ReactToMove(FIGHTING_MOVE_RESULT moveResult, float movePower, float delay, FIGHTING_ANIMATION_POWER_LEVEL animationPowerLevel)
	{
	}

	private void ShowHappyFace()
	{
	}

	private void ShowMood(VOLTRON_MOOD mood)
	{
	}

	public void Show()
	{
	}

	public void Hide()
	{
	}

	public Vector2 GetSize()
	{
		return default;
	}

	private void _003CPlayAnimationWinnerSequence_003Eb__136_0()
	{
	}

	private void _003CPlayAnimationWinnerChampionshipBeltSequence_003Eb__137_1()
	{
	}

	private void _003CPlayAnimationWinnerChampionshipBeltSequence_003Eb__137_2()
	{
	}

	private void _003CPlayAnimationWinnerFistPumpSequence_003Eb__138_0()
	{
	}

	private void _003CPlayAnimationWinnerFistPumpSequence_003Eb__138_1()
	{
	}

	private void _003CPlayAnimationBlockedHitReactionSequence_003Eb__147_0()
	{
	}

	private void _003CPlayAnimationBlockedHitReactionSequence_003Eb__147_1()
	{
	}

	private void _003CPlayAnimationDodgedHitSequence_003Eb__148_0()
	{
	}

	private void _003CPlayAnimationDodgedHitSequence_003Eb__148_1()
	{
	}

	private void _003CPlayAnimationGlovesBlockSequence_003Eb__149_0()
	{
	}
}
