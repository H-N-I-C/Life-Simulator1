using BitLife.UI.CandyObjects;

public class AccessibleCandyButton : UAP_BaseElement
{
	private AccessibleCandyButton()
	{
	}

	protected override void OnInteract()
	{
	}

	public override bool IsElementActive()
	{
		return false;
	}

	private CandyButton GetButton()
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
