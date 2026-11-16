using UnityEngine;

public class MiniGameNavigation : MonoBehaviour
{
	public int mActiveButtonType;

	public float mHeight;

	private bool mButtonsAlwaysEnabled;

	private bool mActionWhileDownMode;

	private bool mButtonsDisabled;

	private bool mButtonJustPressed;

	private GameObject mLeftButton;

	private GameObject mRightButton;

	private GameObject mUpButton;

	private GameObject mDownButton;

	private ButtonActivatedDelegate mButtonActivatedDelegate;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void InitWithParameters(float pWidth, ButtonActivatedDelegate pButtonActivatedDelegate)
	{
	}

	public void ButtonUp(int pDirection)
	{
	}

	public void ButtonDown(int pDirection)
	{
	}

	private void EnableButtons()
	{
	}

	public void DisableAllButtons(bool pDisable)
	{
	}

	public void ActionWhileDown(bool pActionWhileDown)
	{
	}

	public void ButtonsAlwaysEnabled(bool pButtonsAlwaysEnabled)
	{
	}
}
