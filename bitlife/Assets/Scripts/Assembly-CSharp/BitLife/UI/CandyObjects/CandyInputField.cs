using BitLife.UI.Interfaces;
using BitLife.UI.Managers;
using BitLife.UI.Utils;
using TMPro;
using UnityEngine;

namespace BitLife.UI.CandyObjects
{
	public class CandyInputField : MonoBehaviour, IguiObject
	{
		private RectTransform rect;

		private ConstraintContentSizeFitter constraintContentSizeFitter;

		private CandyText title;

		private CandyText randomButtonText;

		private TMP_InputField inputField;

		private CandyImage background;

		private UIFontManager fontManager;

		public string[] randomContent;

		public Delegates.InputFieldEndEditDelegate delegateMethod;

		public Delegates.InputFieldEndEditDelegateIndexed delegateMethodIndexed;

		public int listIndex;

		private string prevString;

		public Delegates.InputFieldSumbitEditDelegate delegateMethodSubmit;

		private bool forceOverride;

		public ScenarioInputField scenarioInputField;

		public bool AllowRawInput => false;

		public void Constructor(IguiOptions boxedOptions)
		{
		}

		public void SetFieldText(string text)
		{
		}

		public void SetTitleText(string text)
		{
		}

		public void OnEndEdit(string input)
		{
		}

		public void OnValueChange(string input)
		{
		}

		public Vector2 GetActualSize()
		{
			return default;
		}

		public void SetHeightConstraint()
		{
		}

		public void SetSize(Vector2 size)
		{
		}

		public void SetupInputFieldTMProText(CandyTextOptions options)
		{
		}

		public void SetupInputFieldPlaceholderTMProText(CandyTextOptions options)
		{
		}

		public TMP_FontAsset GetFont(FontsEnum fontName)
		{
			return null;
		}

		public void PickRandom()
		{
		}

		private void SetText(string text)
		{
		}

		public string GetText()
		{
			return null;
		}
	}
}
