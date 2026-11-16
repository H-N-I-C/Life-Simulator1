using BitLife.UI.Interfaces;
using DG.Tweening;
using UnityEngine;

public class MiniGameThreeCardShuffle : MonoBehaviour, IguiObject
{
	private RectTransform rect;

	private float CARD_HEIGHT_TO_WIDTH_RATIO;

	private bool _initialized;

	private bool _gameOver;

	private Scenario _scenario;

	private float _width;

	private float _height;

	private float _scale;

	private bool _buttonJustPressed;

	private Vector3 _pos0;

	private Vector3 _pos1;

	private Vector3 _pos2;

	private SimCasinoSession _miniGameData;

	private SimCasinoGameThreeCardShuffle _gameData;

	private int _difficultyLevel;

	private GameObject _card1Object;

	private GameObject _card2Object;

	private GameObject _card3Object;

	private GameObject _activeCardObject;

	private GameObject _winningCardObject;

	private Sequence _throbCardSequence;

	private GameObject _startButtonObject;

	private float _cardVerticalBaseline;

	private float _distanceBetweenCards;

	private GameObject _resultsLabelObject;

	private Vector3 _cardLocalScale;

	private Sequence _resultsLabelSequence;

	private bool _cardTouchesEnabled;

	private ScenarioMinigameTextRegistry mTextRegistry;

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

	public void InitWithParameters(float desiredWidth, Scenario theScenario)
	{
	}

	public void ButtonUpStart()
	{
	}

	public void ButtonDownStart()
	{
	}

	private void HideStartButton()
	{
	}

	private void ShuffleCards()
	{
	}

	private void PlaySwapSound()
	{
	}

	private void StartPlay()
	{
	}

	private void GameStartHideCards()
	{
	}

	private void NonGamePlayHideCards()
	{
	}

	private void NonGamePlayFlipCard1()
	{
	}

	private void NonGamePlayFlipCard2()
	{
	}

	private void NonGamePlayFlipCard3()
	{
	}

	private void RevealWinningCard()
	{
	}

	private void PlayerRevealCard()
	{
	}

	private void RevealCard(GameObject cardObject, bool hideAfter)
	{
	}

	private void PlayWinSound()
	{
	}

	private void PlayLoseSound()
	{
	}

	private Sequence ThrobObject(GameObject theObject)
	{
		return null;
	}

	private void KillThrob(Sequence theSequence, GameObject theObject)
	{
	}

	private void DelayWin()
	{
	}

	private void HideActiveCard()
	{
	}

	private void HideCard(GameObject cardObject)
	{
	}

	public void ResetButtonJustPressed()
	{
	}

	private bool ButtonUpVerification()
	{
		return false;
	}

	public void Card1ButtonUp()
	{
	}

	public void Card2ButtonUp()
	{
	}

	public void Card3ButtonUp()
	{
	}

	private void GameOver(int result)
	{
	}

	private void WinGame()
	{
	}

	private void LoseGame()
	{
	}

	private void ExitGame(int result)
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
