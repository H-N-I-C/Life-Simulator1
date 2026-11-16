using DG.Tweening;
using UnityEngine;

public class MiniGameSnakeEmoji : MonoBehaviour
{
	public int mSpot;

	public bool mActive;

	private float mDuration;

	private float mCurrentTime;

	private Vector3 mStartPos;

	private Vector3 mEndPos;

	private int mCurrentDirection;

	private int mNewDirection;

	private MiniGameSnake mSnakeMiniGameScript;

	private bool mRiotMode;

	private GameObject mPartnerObject;

	private Sequence mSequence;

	private void Start()
	{
	}

	private void OnDestroy()
	{
	}

	private void Update()
	{
	}

	private void UpdateMove()
	{
	}

	public void MoveTo(Vector2 pPosition, float pDuration, MiniGameSnake pMiniGame, bool pRiotMode, GameObject pPartnerObject)
	{
	}

	public void RiotFight(float pScale)
	{
	}

	public void KillRiots()
	{
	}
}
