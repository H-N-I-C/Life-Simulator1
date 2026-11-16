using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.Interfaces;
using UnityEngine;

public class MiniGameSnake : MonoBehaviour, IguiObject
{
	private sealed class _003CDamageEffectThenExit_003Ed__42 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MiniGameSnake _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CDamageEffectThenExit_003Ed__42(int _003C_003E1__state)
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

	private sealed class _003CDelayedRiotMoves_003Ed__78 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float pDelay;

		public GameObject pCopObject;

		public GameObject pInmateObject;

		public Vector2 pPoint;

		public MiniGameSnake _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CDelayedRiotMoves_003Ed__78(int _003C_003E1__state)
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

	private float mWidth;

	private bool mInitialized;

	private bool mGameOver;

	private Scenario mScenario;

	private MiniGameSwipeNavigation mNavigationScript;

	private bool mCopsEnabled;

	private bool mRiotStarted;

	private int mInmateAttackCount;

	private int mMoveDirection;

	private int mLastVisualMoveDirection;

	private int mFoodGridSpot;

	private int mDesiredNumberOfCops;

	private int mDesiredNumberOfInmates;

	private int mInmatesRecruited;

	private int mNumberOfCopsAdded;

	private int mLastEmojiIndex;

	private float mGridSpotWidth;

	private Vector2 mGridSize;

	private Sprite mHeroEmoji;

	private GameObject mFoodObject;

	private List<Vector2> mGridSpots;

	private List<GameObject> mSnakeSegments;

	private List<int> mSnakeSegmentLocations;

	private List<Sprite> mEmojis;

	private List<int> mCopLocations;

	private GameObject mHighlightSquareObject;

	private bool mFoodFound;

	private MiniGameDataPrisonRiot mMiniGameDataScript;

	private GameObject mBacker;

	private GameObject mLabel;

	private GameObject mSnakeEmojiPrefabObject;

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

	private void ButtonActivated(int pButtonType)
	{
	}

	private void HideNavigationButtons(bool pHide)
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

	private void UpdateInmatesRecruitedStatus()
	{
	}

	private void RequestMove(int pDirection)
	{
	}

	private bool ValidGridSpot(int pSpot)
	{
		return false;
	}

	private int SpotForCoordinate(Vector2Int pCoordinate)
	{
		return 0;
	}

	private Vector2Int CoordinateForSpot(int pIndex)
	{
		return default;
	}

	private void AddFood()
	{
	}

	private void AddCop()
	{
	}

	private void AddRiotCop()
	{
	}

	private bool EdgeTooCloseTo(int pSpot)
	{
		return false;
	}

	private void BuildEmojiList()
	{
	}

	private Sprite GetNewEmoji()
	{
		return null;
	}

	private Sprite GetRandomEmoji()
	{
		return null;
	}

	private bool EmojiAlreadyUsed(Sprite sprite)
	{
		return false;
	}

	private bool SnakeHeadTooCloseTo(int pSpot)
	{
		return false;
	}

	private bool FoodTooCloseTo(int pSpot)
	{
		return false;
	}

	private bool CopTooCloseTo(int pSpot)
	{
		return false;
	}

	private float DistanceBetween(Vector2 pPointA, Vector2 pPointB)
	{
		return 0f;
	}

	private void AddSnakeAndFood()
	{
	}

	private bool SnakeSegmentExistsAt(int pGridSpot)
	{
		return false;
	}

	private bool ObjectExistsAt(int pGridSpot)
	{
		return false;
	}

	private bool CopExistsAt(int pGridSpot)
	{
		return false;
	}

	private bool FoodExistsAt(int pGridSpot)
	{
		return false;
	}

	private void BuildGrid()
	{
	}

	private bool MoveSnakeInDirection(int pDirection)
	{
		return false;
	}

	private bool SegmentExistsAt(int pSpot)
	{
		return false;
	}

	private bool CopInGridSpot(int pSpot)
	{
		return false;
	}

	private void AddSegment(Sprite pEmoji, float pHeight, int pGridSpot, int pType)
	{
	}

	private void AddSegment(int pEmojiID, float pHeight, int pGridSpot, int pType)
	{
	}

	private void ResetGame()
	{
	}

	private void MoveSnakeHeadTo(int pSpot)
	{
	}

	public void MoveEmojiCompletionHandler(GameObject pObject)
	{
	}

	public void MoveRioterEmojiCompletionHandler(GameObject pObject, GameObject pPartnerObject)
	{
	}

	private void Riot()
	{
	}

	private void RiotPhase2()
	{
	}

	private IEnumerator DelayedRiotMoves(GameObject pCopObject, GameObject pInmateObject, Vector2 pPoint, float pDelay)
	{
		return null;
	}

	private Vector2 GetMidPoint(Vector2 pPointA, Vector2 pPointB)
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
}
