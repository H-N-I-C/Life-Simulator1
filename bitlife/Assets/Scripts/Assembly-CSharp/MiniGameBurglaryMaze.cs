using System.Collections.Generic;
using UnityEngine;

public class MiniGameBurglaryMaze : MonoBehaviour
{
	public bool mShouldBeRotated;

	public int mWidth;

	public int mHeight;

	public int mExitAttachmentSpot;

	public int mExitDirection;

	public int mPlayerSpot;

	public GameObject mPlayer;

	public List<GameObject> mCatchers;

	public List<int> mLeftWalls;

	public List<int> mRightWalls;

	public List<int> mTopWalls;

	public List<int> mBottomWalls;

	public List<GameObject> mItems;

	public Sprite HomeOwnerEmoji;

	private List<int> mPossiblePlayerStartSpots;

	private List<int> mPossibleChaserStartSpots;

	private List<int> mPossibleItemSpots;

	private GameObject mCatcherPrefabObject;

	private GameObject mItemToStealPrefabObject;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void InitWithParameters(int pWidth, int pHeight, int pExitAttachmentSpot, int pExitDirection)
	{
	}

	public void AddWallToSide(int pSide, int pSpot)
	{
	}

	public void AddFullWallAtCoordinate(Vector2Int pPoint)
	{
	}

	public void AddFullWallToSpot(int pSpot)
	{
	}

	public void AddCatcherToSpot(int pSpot, int pType, int pChaseType, int pDirection, Sprite pHomeOwnerEmoji)
	{
	}

	public void MirrorMazeHorizontal(bool pHorizontal)
	{
	}

	public void AddItem(SimBurgleItem pSimBurgleItem, int pSpot)
	{
	}

	public void AddToBestAvailableSpotForItem(SimBurgleItem pSimBurgleItem)
	{
	}

	public void AddPlayerToBestAvailableSpot()
	{
	}

	public void AddCatcherToBestAvailableSpotWithType(int pType)
	{
	}

	public void AddPossiblePlayerStartCoordinate(Vector2Int pCoordinate)
	{
	}

	public void AddPossibleChaserStartCoordinate(Vector2Int pCoordinate)
	{
	}

	public void AddPossibleItemCoordinate(Vector2Int pCoordinate)
	{
	}

	public void RotateMaze()
	{
	}

	private int IndexForCoordinate(Vector2Int pCoordinate)
	{
		return 0;
	}

	private Vector2Int CoordinateForIndex(int pIndex)
	{
		return default;
	}

	private int GetMirrorSpot(int pSpot, bool pHorizontal)
	{
		return 0;
	}

	private int GetRotatedSpot(int pSpot)
	{
		return 0;
	}
}
