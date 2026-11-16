using BitLife.UI.Interfaces;
using DG.Tweening;
using UnityEngine;

public class MiniGameHighLow : MonoBehaviour, IguiObject
{
	private RectTransform rect;

	private float CARD_HEIGHT_TO_WIDTH_RATIO;

	private bool _initialized;

	private bool _gameOver;

	private Scenario _scenario;

	private float _width;

	private float _height;

	private bool _buttonJustPressed;

	private SimCasinoSession _miniGameData;

	private GameObject _card1Object;

	private GameObject _card2Object;

	private GameObject _continueButtonObject;

	private GameObject _higherButtonObject;

	private GameObject _lowerButtonObject;

	private GameObject _stopButtonObject;

	private GameObject _instructionsLabelObject;

	private GameObject _secondaryLabelObject;

	private GameObject _resultsLabelObject;

	private GameObject _tableObject;

	private GameObject _cardAreaObject;

	private GameObject _cardPrefabObject;

	private GameObject _newCardObject;

	private int _currentMultiplier;

	private double _runningEarnings;

	private float _tableTop;

	private int _winLimit;

	private int _handsCompleted;

	private Vector3 _cardLocalScale;

	private Vector3 _naturalHigherButtonPos;

	private Vector3 _naturalLowerButtonPos;

	private Sequence _resultsLabelSequence;

	private Sequence _cardSequence;

	private int _lastCardValue;

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

	private void RaiseMiddleGroundChances(GameObject card)
	{
	}

	private int GetRandomSuit(int excludeSuit = -1)
	{
		return 0;
	}

	private int GetRandomType(int excludeType = -1)
	{
		return 0;
	}

	private void BuildTable()
	{
	}

	public void ButtonUpHigher()
	{
	}

	private int ConvertValueToCardType(int value)
	{
		return 0;
	}

	public void ButtonDownHigher()
	{
	}

	public void ButtonUpLower()
	{
	}

	public void ButtonDownLower()
	{
	}

	public void ButtonUpContinue()
	{
	}

	public void ButtonDownContinue()
	{
	}

	public void ButtonUpStop()
	{
	}

	public void ButtonDownStop()
	{
	}

	public void ReassignCardObjects()
	{
	}

	public void ShowNextGameButtons()
	{
	}

	public void ResetButtonJustPressed()
	{
	}

	private bool ButtonUpVerification()
	{
		return false;
	}

	private void HandComplete(int result)
	{
	}

	private void PostWinSequence()
	{
	}

	private void PostLoseSequence()
	{
	}

	private void ShowButton(GameObject buttonObject)
	{
	}

	private void HideButton(GameObject buttonObject)
	{
	}

	private void WinGame()
	{
	}

	private void QuitGame()
	{
	}

	private void LoseGame()
	{
	}

	private void ExitGame()
	{
	}

	private Sequence ThrobObject(GameObject theObject)
	{
		return null;
	}

	private void KillThrob(Sequence theSequence, GameObject theObject)
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
