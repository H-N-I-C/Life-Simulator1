using System.Collections;
using BitLife.UI.Interfaces;
using DG.Tweening;
using UnityEngine;

public class MiniGameEyeExam : MonoBehaviour, IguiObject
{
	private RectTransform rect;

	private float mWidth;

	private bool mInitialized;

	private Scenario mScenario;

	private bool mButtonDown;

	private bool mButtonJustPressed;

	private bool mGameStarted;

	private bool mGameOver;

	private int mTimeRemaining;

	private int mAnswerSpot;

	private int mChosenSpot;

	private int mOriginalChosenSpot;

	private MiniGameDataEyeExam mData;

	private ArrayList mLetterButtons;

	private GameObject mTimerLabelObject;

	private GameObject mStartButtonObject;

	private GameObject mExitButtonObject;

	private Sequence mSequence;

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

	public void ButtonUpStartButton()
	{
	}

	public void ButtonUpExitButton()
	{
	}

	public void ButtonUpLetterButton(GameObject pObject)
	{
	}

	private void TimerSequence()
	{
	}

	private void UpdateCountdown()
	{
	}

	private bool CheckAnswerForSpotRecurse(int pSpot, bool pRecurse)
	{
		return false;
	}

	private int GetSpotInDirectionForSpot(int pDirection, int pSpot)
	{
		return 0;
	}

	private Vector2 GetLocationForSpot(int pSpot)
	{
		return default;
	}

	private int GetSpotForLocation(Vector2 pLocation)
	{
		return 0;
	}

	private void FillGrid()
	{
	}

	private void GameOverGridFeedback()
	{
	}

	private void GameOverWithType(int pType)
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
