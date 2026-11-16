using System.Collections.Generic;
using BitLife.UI.Interfaces;
using DG.Tweening;
using UnityEngine;

public class MiniGameBlackjack : MonoBehaviour, IguiObject
{
	private RectTransform rect;

	private float BUTTON_GAP;

	private float CARD_HEIGHT_TO_WIDTH_RATIO;

	private float FIRST_DEAL_SPEED;

	private float DELAY_BETWEEN_CARDS;

	private float SINGLE_DEAL_SPEED;

	private float CARD_PROCESSING_DELAY;

	private float GAME_OVER_DELAY;

	private float DELAY_BEFORE_FIRST_DEAL;

	private float DELAY_AFTER_CHOICE;

	private bool mButtonsEnabled;

	private bool mDealerCardsRevealed;

	private bool mButtonJustPressed;

	private Vector2 mDealerHandPos;

	private Vector2 mPlayerHandPos;

	private int mMode;

	private float mWidth;

	private float mCardGap;

	private Vector2 mCardSize;

	private List<MiniGamePlayingCard> mDeck;

	private List<MiniGamePlayingCard> mDealerHand;

	private List<MiniGamePlayingCard> mPlayerHand;

	private Scenario mScenario;

	private SimCasinoSession mMiniGameData;

	private ScenarioMinigameTextRegistry mMinigameRegistry;

	private bool mInitialized;

	private bool mGameOver;

	private GameObject mDealerLabel;

	private GameObject mPlayerLabel;

	private GameObject mDealerValueLabel;

	private GameObject mPlayerValueLabel;

	private GameObject mResultLabel;

	private string mDealerName;

	private GameObject mResultEmoji;

	private GameObject mContinueButton;

	private GameObject mHitButton;

	private GameObject mStandButton;

	private GameObject mCardPrefabObject;

	private Sequence mResultLabelSequence;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Constructor(IguiOptions boxedOptions)
	{
	}

	public void InitWithParameters(float pWidth, Scenario pScenario)
	{
	}

	private void DelayedFirstDeal()
	{
	}

	private void CreateDeck()
	{
	}

	private void ShuffleDeck()
	{
	}

	private void DealHands()
	{
	}

	private void DealPhase1()
	{
	}

	private void DealPhase2()
	{
	}

	private void DealPhase3()
	{
	}

	private void DealPhase4()
	{
	}

	private void DelayedEnablingButtons()
	{
	}

	private void DisposeOfHandsAnimated(bool pAnimated)
	{
	}

	private void RemoveHands()
	{
	}

	private void DealOneCardToHand(List<MiniGamePlayingCard> pHand, float pSpeed)
	{
	}

	private void RevealDealerCard()
	{
	}

	private void DelayedUpdateDealerLabel()
	{
	}

	private void DelayedUpdatePlayerLabel()
	{
	}

	private void SetText(GameObject pLabel, Color pColor, string pText)
	{
	}

	private void UpdateLabelCenters()
	{
	}

	private void ShowButtons(bool pShow)
	{
	}

	private void showContinueButton()
	{
	}

	private void ProcessCards()
	{
	}

	private void DelayedGameOverLose()
	{
	}

	private void DelayedGameOverWin()
	{
	}

	private void DelayedGameOverDraw()
	{
	}

	private void DelayedProcessCards()
	{
	}

	private void DelayedDealHands()
	{
	}

	private Sequence ThrobObject(GameObject theObject)
	{
		return null;
	}

	private void KillThrob(Sequence theSequence, GameObject theObject)
	{
	}

	private void GameOver(int pType)
	{
	}

	private void ExitGame()
	{
	}

	private int GetCurrentTotalForHand(List<MiniGamePlayingCard> pHand)
	{
		return 0;
	}

	public void ButtonUpHit()
	{
	}

	public void ButtonDownHit()
	{
	}

	public void ButtonUpStand()
	{
	}

	public void ButtonDownStand()
	{
	}

	public void ButtonUpContinue()
	{
	}

	public void ButtonDownContinue()
	{
	}

	public void ResetButtonJustPressed()
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
}
