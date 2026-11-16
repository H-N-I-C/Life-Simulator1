using BitLife.UI.CandyObjects;
using BitLife.UI.Interfaces;
using UnityEngine;

namespace BitLife.UI.ScenarioObjects
{
	public class UIScenarioFocusBarOptions : IguiOptions
	{
		public CandyImageOptions backgroundOptions;

		public CandyImageOptions emojiCandyImageOptions;

		public CandyTextOptions nameCandyTextOptions;

		public CandyTextOptions titleCandyTextOptions;

		public UIScenarioFocusBarOptions(CandyImageOptions backgroundOptions, CandyImageOptions emojiCandyImageOptions, CandyTextOptions nameCandyTextOptions, CandyTextOptions titleCandyTextOptions)
		{
		}

		public Vector2 GetSize()
		{
			return default;
		}

		public bool HasTexture()
		{
			return false;
		}
	}
}
