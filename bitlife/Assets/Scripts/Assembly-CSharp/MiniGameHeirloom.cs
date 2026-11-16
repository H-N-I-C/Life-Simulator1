using System.Collections.Generic;
using BitLife.UI.Interfaces;
using UnityEngine;

public class MiniGameHeirloom : MonoBehaviour, IguiObject
{
	private RectTransform rect;

	private bool mInitialized;

	private bool mItemFound;

	private bool mTouchInPlay;

	private bool mDisableTouch;

	private bool mFlippedScene;

	private bool mFlashlightActive;

	private bool mGameOver;

	private float mSceneStartOffset;

	private Vector2 mTouchOffset;

	private Vector2 mDefaultFlashlightLocation;

	private SimHeirloom mHeirloom;

	private GameObject mSceneObject;

	private GameObject mItemObject;

	private GameObject mFrameObject;

	private GameObject mRingObject;

	private GameObject mRingImageObject;

	private GameObject mShadeObject;

	private GameObject mResultsContainerObject;

	private GameObject mResultsLabelObject;

	private GameObject mFlashlightObject;

	private GameObject mBadgeObject;

	private GameObject mNameLabelObject;

	private GameObject mInfoLabelObject;

	private GameObject mVideoButtonObject;

	private Scenario mScenario;

	private MiniGameDataHeirloom mData;

	private float mWidth;

	private SimHeirloom mVideoHeirloom;

	private List<Vector2> mLocations;

	private bool mButtonJustPressed;

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

	private void BuildLocationsForScene(int pIndex)
	{
	}

	private void GameOver()
	{
	}

	private void InstantWin()
	{
	}

	public void RewardedVideoExitGame()
	{
	}

	private void AddResultsAnimated(bool pAnimated)
	{
	}

	private void InstantWinItemPosition()
	{
	}

	private void AnimatedResults()
	{
	}

	private void AnimatedResults2()
	{
	}

	private void AnimatedResults3()
	{
	}

	private string TimeString()
	{
		return null;
	}

	private static string BuildNextHeirloomAvailableString(string template, int hours, int minutes)
	{
		return null;
	}

	private Rect RectTransformToWorldSpace(RectTransform transform)
	{
		return default;
	}

	private void HandleTouchDown(Vector3 pTouchLocation)
	{
	}

	private void HandleTouchMove(Vector3 pTouchLocation)
	{
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

	private void HandleTouchUp()
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

	private void _003CButtonUp_003Eb__51_0(int index)
	{
	}
}
