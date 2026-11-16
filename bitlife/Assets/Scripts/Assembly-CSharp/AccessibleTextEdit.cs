using UnityEngine;
using UnityEngine.UI;

public class AccessibleTextEdit : UAP_BaseElement
{
	private string prevText;

	private string deltaText;

	private AccessibleTextEdit()
	{
	}

	public override bool IsElementActive()
	{
		return false;
	}

	private InputField GetInputField()
	{
		return null;
	}

	private Component GetTMPInputField()
	{
		return null;
	}

	public override string GetCurrentValueAsText()
	{
		return null;
	}

	private string GetValueFromEditBox()
	{
		return null;
	}

	private bool IsPassword()
	{
		return false;
	}

	public override bool IsInteractable()
	{
		return false;
	}

	protected override void OnInteract()
	{
	}

	private void OnInputFinished(string editedText, bool wasConfirmed)
	{
	}

	public void ValueChangeCheck()
	{
	}

	protected override void OnInteractAbort()
	{
	}

	protected override void OnInteractEnd()
	{
	}

	protected override void OnHoverHighlight(bool enable)
	{
	}

	public override bool AutoFillTextLabel()
	{
		return false;
	}

	private void _003COnInteract_003Eb__10_0(string _003Cp0_003E)
	{
	}

	private void _003COnInteract_003Eb__10_1(string _003Cp0_003E)
	{
	}

	private void _003COnInteractAbort_003Eb__13_0(string _003Cp0_003E)
	{
	}

	private void _003COnInteractAbort_003Eb__13_1(string _003Cp0_003E)
	{
	}

	private void _003COnInteractEnd_003Eb__14_0(string _003Cp0_003E)
	{
	}

	private void _003COnInteractEnd_003Eb__14_1(string _003Cp0_003E)
	{
	}
}
