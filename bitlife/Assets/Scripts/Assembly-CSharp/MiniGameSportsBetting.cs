using BitLife.UI.Interfaces;
using UnityEngine;

public class MiniGameSportsBetting : MonoBehaviour, IguiObject
{
	private RectTransform rect;

	private Scenario _scenario;

	private SimCasinoSession _miniGameData;

	private SimCasinoGameSportsBetting _gameData;

	private bool _initialized;

	private bool _gameOver;

	private float _width;

	private MiniGameSportsBettingPlayArea _playAreaScript;

	private GameObject _resultLabelObject;

	private GameObject _continueButtonObject;

	private GameObject _startButtonObject;

	private bool _buttonJustPressed;

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

	public void InitWithParameters(float theWidth, Scenario theScenario)
	{
	}

	public void GameConcluded()
	{
	}

	private void PlayWinSound()
	{
	}

	private void PlayLoseSound()
	{
	}

	private void ExitGame()
	{
	}

	public void ButtonUpContinue()
	{
	}

	public void ButtonDownContinue()
	{
	}

	public void ButtonUpStart()
	{
	}

	public void ButtonDownStart()
	{
	}

	public void ResetButtonJustPressed()
	{
	}

	private void HideStartButton()
	{
	}

	private void ShowContinueButton()
	{
	}

	private void StartGame()
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
