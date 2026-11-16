using UnityEngine;

public class MiniGameKenoBall : MonoBehaviour
{
	private bool _initialized;

	private int Number__BackingField;

	private float mDuration;

	private float mCurrentTime;

	private Vector3 mStartScale;

	private Vector3 mEndScale;

	private Vector3 mOriginalScale;

	private bool mScaleActive;

	private bool mAlphaActive;

	private float mStartAlpha;

	private float mEndAlpha;

	private bool mReverse;

	public int Number
	{
		get
		{
			return Number__BackingField;
		}
		set
		{
			Number__BackingField = value;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void InitWithParameters(int number)
	{
	}

	private void UpdateScale()
	{
	}

	private void UpdateAlpha()
	{
	}

	public void ScaleTo(Vector3 pScale, float pDuration, bool pReverse)
	{
	}

	public void AlphaTo(float pAlpha, float pDuration)
	{
	}
}
