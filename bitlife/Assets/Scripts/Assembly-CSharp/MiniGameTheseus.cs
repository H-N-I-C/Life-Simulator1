using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.Interfaces;
using TMPro;
using UnityEngine;

public class MiniGameTheseus : MonoBehaviour, IguiObject
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Action<int> _003C_003E9__83_0;

		internal void _003CVideoButtonUp_003Eb__83_0(int index)
		{
		}
	}

	private sealed class _003CDamageEffectThenExit_003Ed__85 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MiniGameTheseus _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CDamageEffectThenExit_003Ed__85(int _003C_003E1__state)
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

	private int NUMBER_OF_EASY_MAZES;

	private int NUMBER_OF_MAZES;

	private float MOVE_SPEED;

	private bool mFirstMove;

	private bool mSoundToggle;

	private bool mButtonJustPressed;

	private int mExitDirection;

	private int mExitAttachmentIndex;

	private float mWidth;

	private float mGridSpotWidth;

	private float mMazeHeight;

	private float mVerticalGridOffset;

	private float mHorizontalGridOffset;

	private Vector3 mGridSize;

	private Scenario mScenario;

	private MiniGameNavigation mNavigationScript;

	private GameObject mCancelButton;

	private GameObject mVideoButton;

	private List<MiniGameGridSpot> mGridSpotScripts;

	private MiniGameTheseusPlayer mPlayerScript;

	private MiniGameTheseusGuard mGuardScript;

	private MiniGameTheseusMaze mMazeScript;

	private MiniGameDataTheseus mMiniGameDataScript;

	private bool mInitialized;

	private bool mGameOver;

	private bool mPlayerHeadedToExit;

	private GameObject mMovingGuard;

	private GameObject mSpotPrefabObject;

	private float mRunningHeight;

	private bool mDisguiseMode;

	private float mMorphCount;

	private TextMeshProUGUI _stealUniformLabel;

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

	private void ButtonActivated(int pButtonType)
	{
	}

	private void ActivateDisguiseMode()
	{
	}

	private void AddDisguise()
	{
	}

	private void Unblock()
	{
	}

	private void Morph1()
	{
	}

	private void Morph2()
	{
	}

	private Color GetColorForGridIndex(int pIndex)
	{
		return default;
	}

	private void HideNavigationButtons(bool pHide)
	{
	}

	private void MovePlayerInDirection(int pDirection)
	{
	}

	private void ComparePlayerAndGuard()
	{
	}

	private int NearestGridIndexForPosition(Vector3 pPosition)
	{
		return 0;
	}

	private Vector3 TestMoveGuard(GameObject pGuard, int pDirection)
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

	private void MoveGuard(GameObject pGuard)
	{
	}

	private void GuardMovePhase1()
	{
	}

	private void GuardMovePhase2()
	{
	}

	private void GuardMovePhase3()
	{
	}

	private void GuardMovePhase4()
	{
	}

	private bool MoveGuardOneSpot(GameObject pGuard)
	{
		return false;
	}

	private float DistanceBetween(Vector3 pPointA, Vector3 pPointB)
	{
		return 0f;
	}

	private void PlayFootstepSound()
	{
	}

	private void MoveObjectToGridIndex(GameObject pObject, int pIndex)
	{
	}

	private void MovePlayerToExit()
	{
	}

	public void MovePlayerCompletionhandler()
	{
	}

	public void MovePlayerToExitCompletionHandler()
	{
	}

	public void MoveGuardCompletionHandler()
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

	private void GetMazeForIndex(int pIndex)
	{
	}

	public int MazeIndexForDifficulty(int pDifficulty)
	{
		return 0;
	}

	private void CreateMaze()
	{
	}

	private void DrawMaze()
	{
	}

	public void ButtonUp()
	{
	}

	public void ResetButtonJustPressed()
	{
	}

	public void ButtonDown()
	{
	}

	private bool ButtonUpVerification()
	{
		return false;
	}

	public void VideoButtonUp()
	{
	}

	private void GameOver(int pType)
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

	private void _003CActivateDisguiseMode_003Eb__40_0(int index)
	{
	}
}
