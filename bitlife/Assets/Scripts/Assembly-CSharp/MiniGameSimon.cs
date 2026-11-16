using System.Collections;
using BitLife.UI.Interfaces;
using UnityEngine;

public class MiniGameSimon : MonoBehaviour, IguiObject
{
	private RectTransform rect;

	public static ButtonActivatedDelegate buttonActivatedDelegate;

	private float width;

	private bool initialized;

	private bool gameOver;

	private bool gameStarted;

	private bool playersTurn;

	private bool timerActive;

	private int sequenceTimerCount;

	private int maxSequenceCount;

	private int currentPlayerPressCount;

	private int numberOfButtonToPress;

	private int score;

	private float currentSpeed;

	private ArrayList colorButtons;

	private ArrayList buttonSequence;

	private Scenario scenario;

	private MiniGameDataSimon miniGameData;

	private GameObject boardObject;

	private GameObject eyeObject;

	private GameObject faceObject;

	private GameObject handObject;

	private GameObject button1Object;

	private GameObject button2Object;

	private GameObject button3Object;

	private GameObject button4Object;

	private GameObject startButtonObject;

	private GameObject modeLabelObject;

	private GameObject statusLabelObject;

	private GameObject cancelButtonObject;

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

	public void InitWithParameters(float pWidth, Scenario pScenario)
	{
	}

	public void Button1Up()
	{
	}

	public void Button2Up()
	{
	}

	public void Button3Up()
	{
	}

	public void Button4Up()
	{
	}

	public void StartButtonUp()
	{
	}

	public void CancelButtonUp()
	{
	}

	public void Button1Down()
	{
	}

	public void Button2Down()
	{
	}

	public void Button3Down()
	{
	}

	public void Button4Down()
	{
	}

	public void StartButtonDown()
	{
	}

	public void CancelButtonDown()
	{
	}

	private void PlaySoundForButton(int index)
	{
	}

	private void CreateSequence()
	{
	}

	private void PlayerPressedLightButton(int pIndex)
	{
	}

	private void ProcessCorrectAnswer()
	{
	}

	private void DisableButtons(bool disable)
	{
	}

	private void SimulatePressedLightButton(int index)
	{
	}

	private void ResetBoard()
	{
	}

	private void PlayButtonSequence()
	{
	}

	private void SetModeLabel()
	{
	}

	private void TimerSequence()
	{
	}

	private void SetupForNextPlayerMove()
	{
	}

	private void StartTimerFor(float seconds)
	{
	}

	private void StopTimer()
	{
	}

	private void TimerFired()
	{
	}

	private void StartGame()
	{
	}

	private void GameOverWithType(int type)
	{
	}

	private void HeavyImpact()
	{
	}

	private void ExitGame()
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
