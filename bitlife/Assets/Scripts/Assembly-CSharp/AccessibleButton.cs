using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AccessibleButton : UAP_BaseElement
{
	private AccessibleButton()
	{
	}

	protected override void OnInteract()
	{
	}

	public override bool IsElementActive()
	{
		return false;
	}

	private Button GetButton()
	{
		return null;
	}

	private EventTrigger GetEventTrigger()
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
