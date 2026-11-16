using DG.Tweening;
using UnityEngine;

public class MiniGameButtonAnim : MonoBehaviour
{
	private float mDuration;

	private float mCurrentTime;

	private Vector3 mStartScale;

	private Vector3 mEndScale;

	private Vector3 mOriginalScale;

	private bool mBounceGrowActive;

	private bool mScaleActive;

	private bool mReverse;

	private bool mAlphaActive;

	private float mStartAlpha;

	private float mEndAlpha;

	private RectTransform _cachedGameObjectRect;

	private CanvasGroup _cachedGameObjectCanvasGroup;

	private Sequence _theSequence;

	private RectTransform CachedGameObjectRect => null;

	private CanvasGroup CachedGameObjectCanvasGroup => null;

	private void Update()
	{
	}

	private void UpdateScale()
	{
	}

	private void UpdateBounceGrow()
	{
	}

	private void UpdateAlpha()
	{
	}

	public void ScaleTo(Vector3 pScale, float pDuration, bool pReverse)
	{
	}

	public void BounceGrow()
	{
	}

	public void AlphaTo(float pAlpha, float pDuration)
	{
	}

	public void ThrobObject()
	{
	}

	public void KillThrob()
	{
	}
}
