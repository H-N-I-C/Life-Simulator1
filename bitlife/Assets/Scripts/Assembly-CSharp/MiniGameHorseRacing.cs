using System.Collections.Generic;
using BitLife.UI.Interfaces;
using UnityEngine;

public class MiniGameHorseRacing : MonoBehaviour, IguiObject
{
	private RectTransform rect;

	private Scenario mScenario;

	private SimHorseRacingSession mMiniGameData;

	private bool mInitialized;

	private bool mGameOver;

	private float mWidth;

	private int mFallingHorseIndex;

	private MiniGameHorseRacingTrack mTrackScript;

	private MiniGameHorseRacingList mHorseListScript;

	private GameObject mBetLabel;

	private GameObject mResultLabel;

	private GameObject mResultEmoji;

	private GameObject mContinueButton;

	private GameObject mGoButton;

	private bool mButtonJustPressed;

	private List<List<float>> mSpeedArrays;

	private List<int> mRankOrder;

	private int mCompletedCount;

	private int mRandomHorseDropIndex;

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

	private void RaceEnded()
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

	public void ButtonUpGo()
	{
	}

	public void ButtonDownGo()
	{
	}

	public void ResetButtonJustPressed()
	{
	}

	private void HideGoButton()
	{
	}

	private void ShowContinueButton()
	{
	}

	private float GetDurationForRank(int pRank)
	{
		return 0f;
	}

	private void StartRace()
	{
	}

	public void GrowCompletionHandlerForHorseProgressMeter(MiniGameHorseRacingProgressMeter pHorseMeter, int pGrowCounter)
	{
	}

	private void DropHorse(MiniGameHorseRacingProgressMeter pHorseMeter)
	{
	}

	private List<float> GetHorseSpeedArrayForIndex(int pIndex)
	{
		return null;
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
