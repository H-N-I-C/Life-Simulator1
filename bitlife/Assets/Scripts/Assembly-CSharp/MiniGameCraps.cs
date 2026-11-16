using BitLife.UI.Interfaces;
using DG.Tweening;
using UnityEngine;

public class MiniGameCraps : MonoBehaviour, IguiObject
{
	private RectTransform rect;

	private bool _initialized;

	private bool _gameOver;

	private Scenario _scenario;

	private float _width;

	private float _height;

	private bool _buttonJustPressed;

	private SimCasinoSession _miniGameData;

	private GameObject _die1Object;

	private GameObject _die2Object;

	private GameObject _buttonObject;

	private GameObject _resultsLabelObject;

	private GameObject _tableObject;

	private GameObject _diceAreaObject;

	private GameObject _buttonLabel;

	private float _tableTop;

	private Vector2 _diceRoll;

	private Sequence _resultLabelSequence;

	private bool _rerollOccured;

	private int _numRerollsAllowed;

	private int _rerollCount;

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

	private void BuildTable(bool reset = false)
	{
	}

	private void RollDice()
	{
	}

	private void SetDiceFaces(bool preGameRoll = false)
	{
	}

	private void PostRollFlow()
	{
	}

	private Sequence ThrobObject(GameObject theObject)
	{
		return null;
	}

	private void KillThrob(Sequence theSequence, GameObject theObject)
	{
	}

	private void ShowButton()
	{
	}

	private void HideButton()
	{
	}

	private CRAPS_RESULT GetResult()
	{
		return CRAPS_RESULT.WIN;
	}

	private int DiceTotal()
	{
		return 0;
	}

	public void ButtonUp()
	{
	}

	public void ButtonDown()
	{
	}

	public void ResetButtonJustPressed()
	{
	}

	private bool ButtonUpVerification()
	{
		return false;
	}

	private void ExitGame()
	{
	}

	private void AnimationTest()
	{
	}

	private void TestReset()
	{
	}

	private void RollAnimation()
	{
	}

	private void PlayDiceShakeSound()
	{
	}

	private void PlayDiceRollSound()
	{
	}

	private void MidAnimationDiceChange()
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
