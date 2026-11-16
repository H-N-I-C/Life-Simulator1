using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.Interfaces;
using UnityEngine;

public class MiniGameMinesweeper : MonoBehaviour, IguiObject
{
	private sealed class _003CDamageEffectThenExit_003Ed__53 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MiniGameMinesweeper _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CDamageEffectThenExit_003Ed__53(int _003C_003E1__state)
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

	private RectTransform rect;

	private bool mInitialized;

	private bool mTouchInPlay;

	private bool mLongPressCheckEnabled;

	private bool mSoundToggle;

	private bool mFirstTouch;

	private bool mLongPress;

	private bool mButtonJustPressed;

	private bool mGameOver;

	private int mMinesRemaining;

	private int mNumberOfMines;

	private float mWidth;

	private float mGridSpotWidth;

	private float mGridStartVerticalPos;

	private Vector2 mGridSize;

	private Vector3 mTouchLocation;

	private Scenario mScenario;

	private MiniGameDataMinesweeper mData;

	private List<MiniGameMinesweeperGridSpot> mGridSpots;

	private GameObject mMinesFlaggedLabel;

	private GameObject mMinesFlaggedDescriptionLabel;

	private GameObject mFlagIcon;

	private GameObject mBacker;

	private GameObject mExitButton;

	private GameObject mFlagNotice;

	private GameObject mVideoButton;

	private float mReductionMult;

	private bool mDisableVideoButton;

	private bool mAlertShowing;

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

	private void LayoutFlagLabels()
	{
	}

	private void PlayFootstepSound()
	{
	}

	private float BuildGrid()
	{
		return 0f;
	}

	private int NumberOfMinesToCreate()
	{
		return 0;
	}

	private bool ValidGridIndex(int pIndex)
	{
		return false;
	}

	private int IndexForCoordinate(Vector2 pCoordinate)
	{
		return 0;
	}

	private Vector2 CoordinateForIndex(int pIndex)
	{
		return default;
	}

	private void BuildMineFieldWithStartPosition(Vector2 pStartPos)
	{
	}

	private void BuildMineFieldWithRandomStartPosition()
	{
	}

	private void ExitGame()
	{
	}

	public void ButtonUpExit()
	{
	}

	public void ButtonDownExit()
	{
	}

	public void VideoButtonUp()
	{
	}

	private void AlertRemoved()
	{
	}

	public void VideoButtonDown()
	{
	}

	public void ResetButtonJustPressed()
	{
	}

	private void VideoButtonActivated()
	{
	}

	private void GameOverWithType(int pType)
	{
	}

	private IEnumerator DamageEffectThenExit()
	{
		return null;
	}

	private void CheckForWin()
	{
	}

	private int NumberOfAdjacentSpotsForSpot(MiniGameMinesweeperGridSpot pSpot)
	{
		return 0;
	}

	private void ProcessSpot(MiniGameMinesweeperGridSpot pSpot, bool pUserTouched)
	{
	}

	private void RevealRelevantMines()
	{
	}

	public void RevealPreviewMines()
	{
	}

	private void AssignAdjacentMineCountToSpot(MiniGameMinesweeperGridSpot pSpot)
	{
	}

	private void ClearMarks()
	{
	}

	private void UpdateMinesRemaining()
	{
	}

	private bool SpotAdjacentToTargetSpot(MiniGameMinesweeperGridSpot pSpot, MiniGameMinesweeperGridSpot pTargetSpot)
	{
		return false;
	}

	private void CheckForLongPress()
	{
	}

	private void ResetFlagNotice()
	{
	}

	private void HandleTouchDown(Vector3 pTouchLocation)
	{
	}

	private void HandleTouchUp(Vector3 pTouchLocation)
	{
	}

	private Rect RectTransformToWorldSpace(RectTransform transform)
	{
		return default;
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

	private void _003CVideoButtonUp_003Eb__47_0(int index)
	{
	}
}
