using System.Collections.Generic;
using UnityEngine;

public class MiniGameGridSpot : MonoBehaviour
{
	public int mIndex;

	public float mWidth;

	public Vector3 mCenter;

	private List<bool> mWalls;

	private LineRenderer mBorderLines;

	private int mMode;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void InitWithParameters(float pWidth, int pIndex, int pMode)
	{
	}

	public void AddWallToSide(int pSide)
	{
	}

	private void AddFullWall()
	{
	}

	public bool WallExistsOnSide(int pSide)
	{
		return false;
	}

	public bool FullWallExists()
	{
		return false;
	}

	public void AddFrame(Color pColor)
	{
	}

	public void AddBorder(int pBorderType)
	{
	}

	public void AddWallImage(int pWallType)
	{
	}
}
