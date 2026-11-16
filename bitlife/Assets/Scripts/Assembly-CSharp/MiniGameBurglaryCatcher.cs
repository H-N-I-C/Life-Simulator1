using UnityEngine;

public class MiniGameBurglaryCatcher : MonoBehaviour
{
	public int mType;

	public int mCurrentSpotIndex;

	public int mPreviousSpotIndex;

	public int mChaseType;

	public int mDirection;

	public int mCurrentMovementDirection;

	public bool mActive;

	private bool mSoundNeverPlayed;

	private float mLastSoundTime;

	private float mDuration;

	private float mCurrentTime;

	private Vector3 mStartPos;

	private Vector3 mEndPos;

	private int mMoveDirection;

	private MiniGameBurglary mBurglaryGameScript;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void InitWithParameters(int pType, int pChaseType, int pDireciton, int pSpotIndex)
	{
	}

	public void PlayAlertSoundWithGender(int pGender)
	{
	}

	private void UpdateMove()
	{
	}

	public void MoveTo(Vector2 pPosition, float pDuration, MiniGameBurglary pMiniGame)
	{
	}

	public void SetFacingDirection(int pDirection)
	{
	}
}
