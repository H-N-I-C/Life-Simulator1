using UnityEngine;

public class MiniGamePlayingCard : MonoBehaviour
{
	public bool mRevealed;

	public int mSuit;

	public int mType;

	public int mValue;

	public bool mActive;

	private GameObject mBack;

	private GameObject mFront;

	private float mDuration;

	private float mCurrentTime;

	private Vector3 mStartPos;

	private Vector3 mEndPos;

	private Vector3 mStartScale;

	private Vector3 mEndScale;

	private Vector3 mOriginalScale;

	private bool mScaleActive;

	private bool mReverse;

	private bool mAlphaActive;

	private float mStartAlpha;

	private float mEndAlpha;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void InitWithParameters(int pSuit, int pType)
	{
	}

	private void UpdateMove()
	{
	}

	private void UpdateScale()
	{
	}

	private void UpdateAlpha()
	{
	}

	public void MoveTo(Vector2 pPosition, float pDuration)
	{
	}

	public void ScaleTo(Vector3 pScale, float pDuration, bool pReverse)
	{
	}

	public void AlphaTo(float pAlpha, float pDuration)
	{
	}

	public void MakeVisible()
	{
	}

	public void Reveal(bool pAnimated)
	{
	}

	public void Hide(bool pAnimated)
	{
	}

	private void DelayedFaceActive()
	{
	}

	private void DelayedBackActive()
	{
	}

	public void DefineCard(int suit, int type)
	{
	}
}
