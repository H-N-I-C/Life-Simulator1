using UnityEngine;

public class MiniGameMinesweeperGridSpot : MonoBehaviour
{
	private bool mInitialized;

	public bool mRevealed;

	public bool mFlagged;

	public bool mMine;

	public bool mMarked;

	public int mAdjacentMineCount;

	public int mGridIndex;

	public Vector3 mCenter;

	private int mThemeIndex;

	private GameObject mFlagObject;

	private GameObject mNumberLabel;

	private GameObject mCoveredObject;

	private GameObject mBackgroundObject;

	private GameObject mMineObject;

	private GameObject mPreviewMineObject;

	private bool mPreviewShown;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void InitWithParameters(int pGridIndex, int pThemeIndex)
	{
	}

	public void FlagToggle()
	{
	}

	public void AddMine()
	{
	}

	public void RevealMinePreview()
	{
	}

	public void RevealByUserTouch(bool pRevealedByTouch)
	{
	}

	public void AssignAdjacentMineCount(int pMineCount)
	{
	}
}
