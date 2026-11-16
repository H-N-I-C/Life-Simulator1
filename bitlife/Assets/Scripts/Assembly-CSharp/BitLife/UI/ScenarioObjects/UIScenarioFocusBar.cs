using BitLife.UI.CandyObjects;
using BitLife.UI.Interfaces;
using UnityEngine;

namespace BitLife.UI.ScenarioObjects
{
	public class UIScenarioFocusBar : MonoBehaviour, IguiObject
	{
		private const float TOTAL_WIDTH_FOR_TITLE_AND_NAME = 760f;

		private const float TITLE_AND_NAME_GAP_SIZE = 20f;

		private RectTransform rect;

		private CandyImage background;

		private CandyImage emojiImage;

		private CandyText nameText;

		private CandyText titleText;

		public void Constructor(IguiOptions boxedOptions)
		{
		}

		private void SetupTitleText(UIScenarioFocusBarOptions options)
		{
		}

		private void SetupNameText(UIScenarioFocusBarOptions options)
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
	}
}
