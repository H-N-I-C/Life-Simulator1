using System;
using System.Collections.Generic;
using BitLife.UI.Interfaces;
using DG.Tweening;
using Game;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniGameFighting : MonoBehaviour, IguiObject
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimFightClubMove, int> _003C_003E9__187_0;

		internal int _003CGetRandomPromotionalMovesForStyle_003Eb__187_0(SimFightClubMove x)
		{
			return 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass123_0
	{
		public MiniGameFightingButton button;

		internal void _003CShowMoveButtons_003Eb__1()
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass126_0
	{
		public MiniGameFightingButton moveButton;

		internal void _003CToggleMoveInfoDisplay_003Eb__0()
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass145_0
	{
		public MiniGameFightingFighter fighter;

		public MiniGameFighting _003C_003E4__this;

		internal void _003CGetFighterStartDanceSequence_003Eb__0()
		{
		}

		internal void _003CGetFighterStartDanceSequence_003Eb__1()
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass187_0
	{
		public DataFightIngMove moveData;

		internal bool _003CGetRandomPromotionalMovesForStyle_003Eb__1(SimFightClubMove y)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass95_0
	{
		public Scenario theScenario;

		public MiniGameFighting _003C_003E4__this;

		internal void _003CInitWithParameters_003Eb__0(bool result)
		{
		}
	}

	private MiniGameFightingStatBar HeroStaminaStatBar__BackingField;

	private GameObject FightContainerObject__BackingField;

	private MiniGameFightingFighterStats Stats1__BackingField;

	private MiniGameFightingFighterStats Stats2__BackingField;

	private MiniGameFightingBettingFighterStats BettingStats1__BackingField;

	private MiniGameFightingBettingFighterStats BettingStats2__BackingField;

	private MiniGameDataFighting MiniGameData__BackingField;

	private bool IsChampionshipMatch__BackingField;

	private FIGHTING_PREFERRED_WINNER PreferredWinner__BackingField;

	private SOUND LastPainSound__BackingField;

	private SOUND LastHitSound__BackingField;

	private SOUND LastDodgeSound__BackingField;

	private SOUND LastBlockSound__BackingField;

	private bool HeroGetsAdvantage__BackingField;

	private RectTransform _rect;

	private bool _gameOver;

	private Scenario _scenario;

	private float _width;

	private float _height;

	private bool _buttonJustPressed;

	private GameObject _moveButtonsContainerObject;

	private GameObject _fightBorderDisplayObject;

	private GameObject _moveInfoButtonObject;

	private MiniGameFightingFighter _fighter1;

	private MiniGameFightingFighter _fighter2;

	private MiniGameFightingFighter _sourceFighter;

	private MiniGameFightingFighter _destFighter;

	private SimFightClubMove _currentMove;

	private MiniGameFightingTextDisplay _textDisplay;

	private List<MiniGameFightingButton> _moveButtons;

	private List<GameObject> _infoButtonObjects;

	private MiniGameFightingButton _startButton;

	private bool _heroInPlay;

	private int _penaltyMoveCountdown;

	private BracketParser _parser;

	private int _lastPostMoveBlockTextIndex;

	private bool _gameError;

	private MiniGameFightingBetting _betting;

	private ScenarioMinigameTextRegistry _registry;

	private ScenarioMiniGameTextDictionary _randomOptions;

	private const string UnrecoverableNullFound = "Unrecoverable null found";

	private const float LargeSpacer = 60f;

	private const float SmallSpacer = 30f;

	private const int StaminaBarHeight = 58;

	private const int StaminaBarWidth = 942;

	private const int TextDisplayStandardWidth = 800;

	private const int PenaltyMovesAllowed = 2;

	private const int FightAnnounceChampionshipDivisor = 40;

	private const int FightAnnounceRegularDivisor = 33;

	private const int InfoButtonSpacer = 69;

	public MiniGameFightingStatBar HeroStaminaStatBar
	{
		get
		{
			return HeroStaminaStatBar__BackingField;
		}
		private set
		{
			HeroStaminaStatBar__BackingField = value;
		}
	}

	public GameObject FightContainerObject
	{
		get
		{
			return FightContainerObject__BackingField;
		}
		private set
		{
			FightContainerObject__BackingField = value;
		}
	}

	public MiniGameFightingFighterStats Stats1
	{
		get
		{
			return Stats1__BackingField;
		}
		private set
		{
			Stats1__BackingField = value;
		}
	}

	public MiniGameFightingFighterStats Stats2
	{
		get
		{
			return Stats2__BackingField;
		}
		private set
		{
			Stats2__BackingField = value;
		}
	}

	public MiniGameFightingBettingFighterStats BettingStats1
	{
		get
		{
			return BettingStats1__BackingField;
		}
		private set
		{
			BettingStats1__BackingField = value;
		}
	}

	public MiniGameFightingBettingFighterStats BettingStats2
	{
		get
		{
			return BettingStats2__BackingField;
		}
		private set
		{
			BettingStats2__BackingField = value;
		}
	}

	public MiniGameDataFighting MiniGameData
	{
		get
		{
			return MiniGameData__BackingField;
		}
		private set
		{
			MiniGameData__BackingField = value;
		}
	}

	public bool IsChampionshipMatch
	{
		get
		{
			return IsChampionshipMatch__BackingField;
		}
		private set
		{
			IsChampionshipMatch__BackingField = value;
		}
	}

	public FIGHTING_PREFERRED_WINNER PreferredWinner
	{
		get
		{
			return PreferredWinner__BackingField;
		}
		private set
		{
			PreferredWinner__BackingField = value;
		}
	}

	public SOUND LastPainSound
	{
		get
		{
			return LastPainSound__BackingField;
		}
		set
		{
			LastPainSound__BackingField = value;
		}
	}

	public SOUND LastHitSound
	{
		get
		{
			return LastHitSound__BackingField;
		}
		set
		{
			LastHitSound__BackingField = value;
		}
	}

	public SOUND LastDodgeSound
	{
		get
		{
			return LastDodgeSound__BackingField;
		}
		set
		{
			LastDodgeSound__BackingField = value;
		}
	}

	public SOUND LastBlockSound
	{
		get
		{
			return LastBlockSound__BackingField;
		}
		set
		{
			LastBlockSound__BackingField = value;
		}
	}

	public bool HeroGetsAdvantage
	{
		get
		{
			return HeroGetsAdvantage__BackingField;
		}
		set
		{
			HeroGetsAdvantage__BackingField = value;
		}
	}

	public ScenarioMinigameTextRegistry Registry => null;

	public void Constructor(IguiOptions boxedOptions)
	{
	}

	private void InitWithParameters(float desiredWidth, Scenario theScenario)
	{
	}

	private void OnDestroy()
	{
	}

	private void Update()
	{
	}

	private bool BuildComponents()
	{
		return false;
	}

	private bool SetSize()
	{
		return false;
	}

	private bool BuildFightContainer()
	{
		return false;
	}

	private bool BuildFightBorder()
	{
		return false;
	}

	private bool BuildStaminaBar()
	{
		return false;
	}

	private bool BuildStats()
	{
		return false;
	}

	private bool BuildBettingHpStatBars()
	{
		return false;
	}

	private bool BuildStartButton()
	{
		return false;
	}

	private bool BuildTextDisplay()
	{
		return false;
	}

	private bool BuildMoveButtons()
	{
		return false;
	}

	private bool BuildMoveInfoButton()
	{
		return false;
	}

	private bool BuildFighters()
	{
		return false;
	}

	private MiniGameFightingFighter BuildFighter(bool isFighter1)
	{
		return null;
	}

	private bool PositionComponents()
	{
		return false;
	}

	private void PresentFight()
	{
	}

	private void BeginFight()
	{
	}

	public bool IllegalMovesShouldBePunished()
	{
		return false;
	}

	private void GenerateMove()
	{
	}

	private void GenerateNpcMove()
	{
	}

	private void PerformCurrentMove()
	{
	}

	public void MoveCompletedCallback(int hpLoss, FIGHTING_MOVE_RESULT result)
	{
	}

	private void PostMoveActions(FIGHTING_MOVE_RESULT moveResult)
	{
	}

	private bool WillGetCaughtForIllegalMove(MiniGameFightingFighter fighter)
	{
		return false;
	}

	private void SetupNextMove()
	{
	}

	private void PunishForIllegalMove()
	{
	}

	private void ShowMoveButtons()
	{
	}

	private void HideMoveInfoButton()
	{
	}

	private void ShowMoveInfoButton()
	{
	}

	private void ToggleMoveInfoDisplay()
	{
	}

	private float HideMoveButtons()
	{
		return 0f;
	}

	private void ResetMoveButtonsInfoDisplays()
	{
	}

	private static void ShowButton(MiniGameFightingButton button)
	{
	}

	private static void HideButton(MiniGameFightingButton button)
	{
	}

	public void ButtonUp(PointerEventData data)
	{
	}

	public void ButtonDown(PointerEventData data)
	{
	}

	private bool IsInfoButton(GameObject button)
	{
		return false;
	}

	public void ResetButtonJustPressed()
	{
	}

	public void ShowStartButton()
	{
	}

	private void ActivateStartButton()
	{
	}

	public void GameOver()
	{
	}

	private bool TryGetBothSides(out MiniGameFightingFighter player, out MiniGameFightingFighter opponent)
	{
		player = null;
		opponent = null;
		return false;
	}

	private void BailOutOfGame()
	{
	}

	private void ExitGame()
	{
	}

	private void Fighter2DelayedIdleAnimations()
	{
	}

	private void AnimateFightersIntro()
	{
	}

	private void PlayZoomInSequence(float goToCornerDuration)
	{
	}

	private void PlayRevealSupportingStatsSequence(Vector3 finalFighter1Pos, Vector3 finalFighter2Pos)
	{
	}

	private Sequence GetFighterStartDanceSequence(MiniGameFightingFighter fighter, Vector3 finalFighterPos, float goToCornerDuration)
	{
		return null;
	}

	private Sequence ThrobObject(GameObject theObject)
	{
		return null;
	}

	private void KillThrob(Sequence theSequence, GameObject theObject)
	{
	}

	public SOUND GetPainSoundForGender(GENDER gender)
	{
		return SOUND.NONE;
	}

	public SOUND GetHitSound()
	{
		return SOUND.NONE;
	}

	public SOUND GetBlockSound()
	{
		return SOUND.NONE;
	}

	public SOUND GetDodgeSound()
	{
		return SOUND.NONE;
	}

	private void PlayEndGameSounds()
	{
	}

	public static void LightImpact()
	{
	}

	public static void MediumImpact()
	{
	}

	public static void HeavyImpact()
	{
	}

	private void AssignCoachingFeedback()
	{
	}

	private FIGHT_CLUB_FIGHTING_MINI_GAME_COACH_FEEDBACK DetermineActionableFeedbackFallback(MiniGameFightingFighter winningFighter, MiniGameFightingFighter losingFighter)
	{
		return FIGHT_CLUB_FIGHTING_MINI_GAME_COACH_FEEDBACK.NONE;
	}

	private void PostMatchTextDisplay()
	{
	}

	private void PostMoveTextDisplay(int hpLoss, FIGHTING_MOVE_RESULT result)
	{
	}

	private string ParseHitText(string movement, string critical, string took, int hpLoss)
	{
		return null;
	}

	private string ParseSpontaneousExercise(string subject, int number)
	{
		return null;
	}

	private string ParseInsult(string subject, string insult)
	{
		return null;
	}

	private string GetRandomGenericNpcWinString()
	{
		return null;
	}

	private string GetRandomGenericHeroWinString()
	{
		return null;
	}

	private string GetPostDodgeOrBlockText()
	{
		return null;
	}

	private void AnnounceFightTextDisplay()
	{
	}

	private float AnnounceFirstFighterInHeroMatchTextDisplay()
	{
		return 0f;
	}

	private float IllegalMovePunishmentTextDisplay()
	{
		return 0f;
	}

	private void DetermineHeroAdvantage()
	{
	}

	private void DetermineFighterToMoveFirst()
	{
	}

	private void DeterminePreferredWinner()
	{
	}

	public static FIGHTING_PREFERRED_WINNER DeterminePreferredWinnerByLevelComparisons(int heroLevel, int npcLevel)
	{
		return FIGHTING_PREFERRED_WINNER.NONE;
	}

	private void InfluenceFighterStats()
	{
	}

	private void InfluenceFighterMoves()
	{
	}

	public void UnrecoverableNull(string methodName = "")
	{
	}

	private void GameError(string message, string methodName = "")
	{
	}

	private void HideTextDisplay()
	{
	}

	public static void KillSequence(Sequence sequence)
	{
	}

	private static void DisableTouchInput()
	{
	}

	private static void EnableTouchInput()
	{
	}

	public Vector2 GetActualSize()
	{
		return default;
	}

	private void SetWidth(float width)
	{
	}

	public void SetSize(Vector2 size)
	{
	}

	public void SetHeightConstraint()
	{
	}

	public static SimFightClubFighter GeneratePromotionalFighter(FIGHT_CLUB_FIGHTING_STYLE blackListStyle = FIGHT_CLUB_FIGHTING_STYLE.NONE)
	{
		return null;
	}

	public static void AssignPromotionalMoves(SimFightClubFighterStats stats)
	{
	}

	private static List<SimFightClubMove> GetRandomPromotionalMovesForStyle(FIGHT_CLUB_FIGHTING_STYLE style)
	{
		return null;
	}

	public void ReplaceNames(ScenarioMinigameTextRegistry registry, List<string> replaceEntries, MiniGameFightingFighter fighter)
	{
	}

	private string ReplaceEntries(string translated, MiniGameFightingFighter fighter)
	{
		return null;
	}

	private void _003CBuildMoveInfoButton_003Eb__108_0(BaseEventData data)
	{
	}

	private void _003CBuildMoveInfoButton_003Eb__108_1(BaseEventData data)
	{
	}

	private void _003CShowMoveButtons_003Eb__123_0()
	{
	}

	private void _003CHideMoveInfoButton_003Eb__124_0()
	{
	}

	private void _003CPlayRevealSupportingStatsSequence_003Eb__144_0()
	{
	}
}
