using System.Collections.Generic;
using UnityEngine;

public class MiniGameTheseusMaze : MonoBehaviour
{
	public int mWidth;

	public int mHeight;

	public int mExitAttachmentSpot;

	public int mExitDirection;

	public int mPlayerSpot;

	public int mGuardSpot;

	public GameObject mPlayer;

	public GameObject mGuard;

	public List<int> mLeftWalls;

	public List<int> mRightWalls;

	public List<int> mTopWalls;

	public List<int> mBottomWalls;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void InitWithParameters(int pWidth, int pHeight, int pExitAttachmentSpot, int pExitDirection, int pPlayerSpot, int pGuardSpot)
	{
	}

	public void AddWallToSide(int pSide, int pSpot)
	{
	}

	public void AddWallToSide(int pSide, Vector2 pPoint)
	{
	}

	public int IndexForCoordinate(Vector2 pCoordinate)
	{
		return 0;
	}

	public Vector2 CoordinateForIndex(int pIndex)
	{
		return default;
	}

	public int GetSpot(int pSpot, bool pMirrorForHorizontal)
	{
		return 0;
	}

	public void MirrorMazeHorizontal(bool pHorizontal)
	{
	}
}
