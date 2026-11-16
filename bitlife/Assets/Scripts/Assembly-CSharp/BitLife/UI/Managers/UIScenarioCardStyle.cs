using UnityEngine;

namespace BitLife.UI.Managers
{
	public class UIScenarioCardStyle
	{
		public Texture2D scenarioBackground;

		public Texture2D focusBar;

		public Color titleColor;

		public Color titleColorBottom;

		public Color bodyColor;

		public Color tableColor;

		public Color barGraphsBgBarColor;

		public Color barGraphsTitleColor;

		public Color inputFieldTextColor;

		public Color placeholderTextColor;

		public UIScenarioCardStyle(Texture2D scenarioBackground, Texture2D focusBar, Color titleColor, Color bodyColor, Color tableColor, Color barGraphsBgBarColor, Color barGraphsTitleColor, Color inputFieldTextColor, Color placeholderTextColor)
		{
		}

		public UIScenarioCardStyle(Texture2D scenarioBackground, Texture2D focusBar, Color titleColor, Color titleColorBottom, Color bodyColor, Color tableColor, Color barGraphsBgBarColor, Color barGraphsTitleColor, Color inputFieldTextColor, Color placeholderTextColor)
		{
		}
	}
}
