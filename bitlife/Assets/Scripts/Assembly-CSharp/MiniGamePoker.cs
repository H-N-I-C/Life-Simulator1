using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.Interfaces;
using GGS.Util;
using TMPro;
using UnityEngine;

public class MiniGamePoker : MonoBehaviour, IguiObject
{
	public enum POKER_GAME_PHASE
	{
		BEFORE_DISPLAYING_COMMUNITY_CARDS = 0,
		BEFORE_DISPLAYING_DEALER_CARDS = 1,
		SCORE_CALCULATION = 2,
		FOLD = 3
	}

	public enum CARD_GAME_RESULT
	{
		GAME_IN_PROGRESS = 0,
		WIN = 1,
		LOSE = 2,
		DRAW = 3
	}

	public enum CARD_RANKING
	{
		FIVE_OF_A_KIND = 0,
		ROYAL_STRAIGHT_FLUSH = 1,
		STRAIGHT_FLUSH = 2,
		FOUR_OF_A_KIND = 3,
		FULL_HOUSE = 4,
		FLUSH = 5,
		STRAIGHT = 6,
		THREE_OF_A_KIND = 7,
		TWO_PAIR = 8,
		PAIR = 9,
		HIGH_CARD = 10,
		HIGH_CARD_SPECIFIC = 11
	}

	protected enum CARD_ZONE
	{
		DECK = 0,
		PLAYER_HAND = 1,
		DEALER_HAND = 2,
		COMMUNITY_CARDS = 3
	}

	protected struct CardLocation
	{
		public CARD_ZONE Zone;

		public int Index;
	}

	protected struct ScoreInfo
	{
		public long Score;

		public CARD_RANKING Ranking;

		public CARD HighestCard;

		public List<CardLocation> Cards;
	}

	private sealed class _003C_003Ec__DisplayClass100_0
	{
		public CARD type;

		internal bool _003CCountCards_003Eb__0(MiniGameCard c)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass82_0
	{
		public MiniGameCard card;

		internal void _003CRevealCards_003Eb__0()
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass99_0
	{
		public CARD_SUIT suit;

		internal bool _003CCountCards_003Eb__0(MiniGameCard c)
		{
			return false;
		}
	}

	private sealed class _003CGameStart_003Ed__76 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float initialDelay;

		public MiniGamePoker _003C_003E4__this;

		private int _003Ci_003E5__2;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CGameStart_003Ed__76(int _003C_003E1__state)
		{
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
		}
	}

	private sealed class _003CGenerateCombinationsRecursive_003Ed__113<T> : IEnumerable<List<T>>, IEnumerable, IEnumerator<List<T>>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private List<T> _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		private int remainingCards;

		public int _003C_003E3__remainingCards;

		private List<T> currentCombination;

		public List<T> _003C_003E3__currentCombination;

		private List<T> allCards;

		public List<T> _003C_003E3__allCards;

		private int startIndex;

		public int _003C_003E3__startIndex;

		private int _003Ci_003E5__2;

		private IEnumerator<List<T>> _003C_003E7__wrap2;

		List<T> IEnumerator<List<T>>.Current => null;

		object IEnumerator.Current => null;

		public _003CGenerateCombinationsRecursive_003Ed__113(int _003C_003E1__state)
		{
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void _003C_003Em__Finally1()
		{
		}

		void IEnumerator.Reset()
		{
		}

		IEnumerator<List<T>> IEnumerable<List<T>>.GetEnumerator()
		{
			return null;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return null;
		}
	}

	public static bool ForceUseStraight;

	public static bool ForceUseRoyalFlush;

	private const int CALL_BUTTON_INDEX = 0;

	private const int FOLD_BUTTON_INDEX = 1;

	private const int CONTINUE_BUTTON_INDEX = 2;

	private int _numberOfRevealedCommunityCards;

	private int _numberOfHiddenCommunityCards;

	private int _numberOfJokers;

	private bool _betAfterRevealingCommunityCards;

	private MiniGameCard _cardPrefabObject;

	private RectTransform _rect;

	private TextMeshProUGUI _communityCardLabel;

	private TextMeshProUGUI _dealerLabel;

	private TextMeshProUGUI _playerLabel;

	private TextMeshProUGUI _dealerRanking;

	private TextMeshProUGUI _playerRanking;

	private TextMeshProUGUI _winLabel;

	private TextMeshProUGUI _bodyLabel;

	private GameObject _callButton;

	private GameObject _foldButton;

	private GameObject _continueButton;

	private TextMeshProUGUI _continueButtonLabel;

	private SimCasinoSession _session;

	private float _initialBet;

	private string _dealerName;

	private ScenarioGamePoker _scenario;

	private ScenarioMinigameTextRegistry mTextRegistry;

	private readonly List<MiniGameCard> _deck__BackingField;

	private readonly List<MiniGameCard> _communityCards__BackingField;

	private readonly List<MiniGameCard> _dealerHand__BackingField;

	private readonly List<MiniGameCard> _playerHand__BackingField;

	private bool _communityCardsRevealed;

	private bool _dealerCardsRevealed;

	private bool _initialized;

	private bool _gameOver;

	private int _playerHandSize;

	private POKER_GAME_PHASE _phase;

	private CARD_GAME_RESULT _result;

	private float BUTTON_GAP;

	private float CARD_HEIGHT_TO_WIDTH_RATIO;

	private float FIRST_DEAL_SPEED;

	private float DELAY_BETWEEN_CARDS;

	private float SINGLE_DEAL_SPEED;

	private float CARD_PROCESSING_DELAY;

	private float GAME_OVER_DELAY;

	private float DELAY_BEFORE_FIRST_DEAL;

	private float DELAY_AFTER_CHOICE;

	private float RELATIVE_OFFSETS_BETWEEN_CARDS;

	private bool _buttonsEnabled;

	private bool _buttonJustPressed;

	private Vector2 _communityCardsPos;

	private Vector2 _dealerHandPos;

	private Vector2 _playerHandPos;

	private float _width;

	private float _cardGap;

	private Vector2 _cardSize;

	private List<MiniGameCard> _deck => _deck__BackingField;

	private List<MiniGameCard> _communityCards => _communityCards__BackingField;

	private List<MiniGameCard> _dealerHand => _dealerHand__BackingField;

	private List<MiniGameCard> _playerHand => _playerHand__BackingField;

	public void Constructor(IguiOptions boxedOptions)
	{
	}

	public void InitWithParameters(float width, Scenario scenario)
	{
	}

	private void CreateDeck()
	{
	}

	private MiniGameCard CreateCard(Either<MiniGameCard.NormalCard, MiniGameCard.Joker> card)
	{
		return null;
	}

	private void ShuffleDeck()
	{
	}

	private void DealHands()
	{
	}

	private IEnumerator GameStart(float initialDelay = 0f)
	{
		return null;
	}

	private void CreateStraightDeck()
	{
	}

	private void CreateRoyalFlushDeck()
	{
	}

	private void DisposeOfHandsAnimated(bool animated)
	{
	}

	private void DestroyCardsGameObjects()
	{
	}

	private void DrawCard(List<MiniGameCard> source, List<MiniGameCard> destination, bool revealed, float movementTime)
	{
	}

	private float RevealCards(List<MiniGameCard> zone)
	{
		return 0f;
	}

	private void SetText(TextMeshProUGUI label, Color color, string text)
	{
	}

	private void ShowButtons(bool show)
	{
	}

	private void ShowContinueButton(bool show)
	{
	}

	private void SetPhase(POKER_GAME_PHASE phase)
	{
	}

	private void HighlightSelectedCards(List<CardLocation> playerCards, List<CardLocation> dealerCards)
	{
	}

	private void GameOver(CARD_GAME_RESULT result)
	{
	}

	private void ExitGame()
	{
	}

	private ScoreInfo? CalculateBestScore(CARD_ZONE[] zones)
	{
		return null;
	}

	private List<CardLocation> GetCardLocations(CARD_ZONE[] zones)
	{
		return null;
	}

	private List<MiniGameCard> GetCards(List<CardLocation> cardLocations)
	{
		return null;
	}

	private List<MiniGameCard> GetCards(CARD_ZONE zone)
	{
		return null;
	}

	private MiniGameCard GetCard(CARD_ZONE zone, int index)
	{
		return null;
	}

	private ScoreInfo? CalculateScore(List<CardLocation> cardLocations)
	{
		return null;
	}

	private List<CARD> CheckCardsOfAKind(List<MiniGameCard> hand, int desiredNumbersOfCards)
	{
		return null;
	}

	private CARD? CheckStraight(List<MiniGameCard> hand)
	{
		return null;
	}

	private CARD_SUIT? CheckFlush(List<MiniGameCard> hand)
	{
		return null;
	}

	private int CountCards(List<MiniGameCard> hand, CARD_SUIT suit)
	{
		return 0;
	}

	private int CountCards(List<MiniGameCard> hand, CARD type)
	{
		return 0;
	}

	private bool IsStraightFinishingAt(List<MiniGameCard> hand, CARD type)
	{
		return false;
	}

	private static int FindUsingOnlyJokers(List<MiniGameCard> hand, HashSet<int> usedCardsIndexes)
	{
		return 0;
	}

	private static int FindUsingOnlyNormalCards(List<MiniGameCard> hand, HashSet<int> usedCardsIndexes, CARD currentType)
	{
		return 0;
	}

	public virtual void OnButtonDown(int buttonIndex)
	{
	}

	public virtual void OnButtonUp(int buttonIndex)
	{
	}

	private void OnButtonUpLogic(int buttonIndex)
	{
	}

	protected void ResetButtonJustPressed()
	{
	}

	public Vector2 GetActualSize()
	{
		return default;
	}

	public void SetWidth(float width)
	{
	}

	public void SetSize(Vector2 size)
	{
	}

	public void SetHeightConstraint()
	{
	}

	private static IEnumerable<List<T>> GenerateCombinations<T>(List<T> allCards, int numberOfCards)
	{
		return null;
	}

	private static IEnumerable<List<T>> GenerateCombinationsRecursive<T>(List<T> allCards, int startIndex, List<T> currentCombination, int remainingCards)
	{
		return null;
	}

	protected string GetRankingString(CARD_RANKING ranking, CARD highestCard)
	{
		return null;
	}
}
