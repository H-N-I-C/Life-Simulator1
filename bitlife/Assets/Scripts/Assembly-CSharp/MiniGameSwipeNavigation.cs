using UnityEngine;

public class MiniGameSwipeNavigation : MonoBehaviour
{
	public int mActiveButtonType;

	private bool mActivateOnRelease;

	private ButtonActivatedDelegate mButtonActivatedDelegate;

	private bool mInitialized;

	public bool mTouchInPlay;

	private Vector2 mStartTouchPosition;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void InitWithParameters(Vector2 pSize, ButtonActivatedDelegate pButtonActivatedDelegate, bool pActivateOnRelease)
	{
	}

	private void HandleTouchDown(Vector3 pTouchLocation)
	{
	}

	private void HandleTouchMoved(Vector3 pTouchLocation)
	{
	}

	private void HandleTouchUp(Vector3 pTouchLocation)
	{
	}
}
