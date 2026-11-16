public class AccessibleAlertViewButton : UAP_BaseElement
{
	private AccessibleAlertViewButton()
	{
	}

	protected override void OnInteract()
	{
	}

	public override bool IsElementActive()
	{
		return false;
	}

	private UIAlertViewEntry GetButton()
	{
		return null;
	}

	public override bool IsInteractable()
	{
		return false;
	}

	public override bool AutoFillTextLabel()
	{
		return false;
	}

	protected override void AutoInitialize()
	{
	}

	protected override void OnHoverHighlight(bool enable)
	{
	}
}
