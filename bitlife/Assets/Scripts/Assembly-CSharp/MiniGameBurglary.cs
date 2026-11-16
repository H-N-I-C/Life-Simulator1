using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.Interfaces;
using UnityEngine;

public class MiniGameBurglary : MonoBehaviour, IguiObject
{
	private sealed class _003CDamageEffectThenExit_003Ed__83 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MiniGameBurglary _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CDamageEffectThenExit_003Ed__83(int _003C_003E1__state)
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

	public static ButtonActivatedDelegate buttonActivatedDelegate;

	private float mHeight;

	private int NUMBER_OF_BURGLARY_MAZES;

	private float BURGLARY_SINGLE_MOVE_DURATION;

	private bool mGameOver;

	private bool mPlayerHeadedToExit;

	private bool mForceAReverse;

	private bool mChaseMode;

	private bool mFirstMove;

	private bool mSoundToggle;

	private int mPrimaryDesiredButtonType;

	private int mSecondaryDesiredButtonType;

	private int mCurrentMoveDirection;

	private float mWidth;

	private float mGridSpotWidth;

	private float mMazeHeight;

	private float mVerticalGridOffset;

	private float mHorizontalGridOffset;

	private float mCollectedListGraphicRunningHorizontal;

	private Vector3 mExitCenter;

	private List<MiniGameGridSpot> mGridSpotScripts;

	private List<int> mScatterTargets;

	private MiniGameBurglaryMaze mMazeScript;

	private float mUnitDivider;

	private MiniGameDataBurglary mMiniGameDataScript;

	private Color mWallColor;

	private Color mSpotColor;

	private Vector3 mRunningColletecItemPosition;

	private bool mInitialized;

	private Vector3 mPlayerSize;

	private Vector3 mCatcherSize;

	private Scenario mScenario;

	private GameObject mCollectedItemsTextObject;

	private GameObject mSpotPrefabObject;

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

	public void MovePlayerCompletionhandler()
	{
	}

	public void MovePlayerToExitCompletionHandler()
	{
	}

	public void MoveCatcherCompletionHandlerForCatcher(GameObject pCatcher)
	{
	}

	public void PlayerCaughtViaCatcher(int pCatcherIndex)
	{
	}

	private void ButtonActivated(int pButtonType)
	{
	}

	private bool MovePlayerForButton(int pButtonType)
	{
		return false;
	}

	private void SwatTeamGoGoGo()
	{
	}

	private void SendInTheCops()
	{
	}

	private void SendInACop()
	{
	}

	private void ActivateCop(int pIndex, Vector2 pPosition, float pDuration)
	{
	}

	private void ToggleCatcherMode()
	{
	}

	private void CatcherModeTimerA()
	{
	}

	private void CatcherModeTimerB()
	{
	}

	private bool MovePlayerInDirection(int pDirection)
	{
		return false;
	}

	private int NearestGridIndexForPosition(Vector3 pPosition)
	{
		return 0;
	}

	private Vector3 TestMoveCatcher(GameObject pCatcher, int pDirection)
	{
		return default;
	}

	private bool ExitingFromGridIndex(int pIndex, int pDirection)
	{
		return false;
	}

	private bool WallExistsTravelingFrom(int pCurrentIndex, int pDesiredIndex, int pDirection)
	{
		return false;
	}

	private void ReleaseTheHounds()
	{
	}

	private bool MoveCatcher(GameObject pCatcher)
	{
		return false;
	}

	private int DirectionFrom(int pIndexA, int pIndexB)
	{
		return 0;
	}

	private float DistanceBetween(Vector3 pPointA, Vector3 pPointB)
	{
		return 0f;
	}

	private void PlayFootstepSound()
	{
	}

	private bool VisibilityBetween(int pSpotA, int pSpotB, int pDirection)
	{
		return false;
	}

	private void MovePlayerToGridIndex(int pIndex)
	{
	}

	private bool DeadEndAtGridIndex(int pIndex, int pMovementDirection)
	{
		return false;
	}

	private void AnimatePositionChangeForCatcher(GameObject pCatcher, int pIndex)
	{
	}

	private void StealItemAtIndex(int pIndex)
	{
	}

	private void MovePlayerToExit()
	{
	}

	private bool PositionEquality(Vector3 pPositionOne, Vector3 pPositionTwo)
	{
		return false;
	}

	private int GridIndexForPosition(Vector3 pPosition)
	{
		return 0;
	}

	private bool LeftEdgeSpot(int pIndex)
	{
		return false;
	}

	private bool RightEdgeSpot(int pIndex)
	{
		return false;
	}

	private bool TopEdgeSpot(int pIndex)
	{
		return false;
	}

	private bool BottomEdgeSpot(int pIndex)
	{
		return false;
	}

	private int CornerEdgeSpot(int pIndex)
	{
		return 0;
	}

	private bool ValidGridIndex(int pIndex)
	{
		return false;
	}

	private Vector3 PositionForGridIndex(int pIndex)
	{
		return default;
	}

	private GameObject GetMazeForIndex(int pIndex)
	{
		return null;
	}

	private void CreateMaze()
	{
	}

	private void DrawMaze()
	{
	}

	private void GameOver(int pType, int pCatchType)
	{
	}

	private IEnumerator DamageEffectThenExit()
	{
		return null;
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
