using System;
using BitLife.UI.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniGameAccount : MonoBehaviour, IguiObject
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, string> _003C_003E9__27_0;

		internal string _003CSetupConsentAccessibility_003Eb__27_0(string linkID)
		{
			return null;
		}
	}

	private const string PRIVACY_POLICY_TAG = "Privacy Policy";

	private const string TERMS_OF_SERVICE_TAG = "Terms of Service";

	private Image checkBox;

	private Image checkBoxMarketing;

	private TextMeshProUGUI label;

	private TextMeshProUGUI labelMarketing;

	private string privacyPolicyUrl;

	private string termsOfServiceUrl;

	private IScenarioLogin _scenario;

	private MiniGameAccountData _miniGameData;

	private Sprite _checkedSprite;

	private Sprite _uncheckedSprite;

	private bool _stateSignup;

	private bool _stateMarketing;

	public Vector2 GetActualSize()
	{
		return default;
	}

	public void Constructor(IguiOptions boxedOptions)
	{
	}

	public void SetHeightConstraint()
	{
	}

	public void SetSize(Vector2 size)
	{
	}

	public void InitWithParameters(float width, Scenario scenario)
	{
	}

	private void SetSignupChecked(bool isChecked)
	{
	}

	private void SetMarketingChecked(bool isChecked)
	{
	}

	private void SetupImages()
	{
	}

	public void OnCheckClick()
	{
	}

	public void OnCheckMarketingClick()
	{
	}

	private void AnnounceCheckboxState(bool isChecked, string label)
	{
	}

	public void OnLabelClick(BaseEventData eventData)
	{
	}

	private void OpenConsentLink(string linkID)
	{
	}

	private void SetupConsentAccessibility()
	{
	}
}
