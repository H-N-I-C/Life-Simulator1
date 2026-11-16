using BitLife.UI.CandyObjects;
using BitLife.UI.Interfaces;

namespace BitLife.UI.Containers
{
	public class UIScenarioCardOptions
	{
		public bool isShowingDismissButton;

		public bool isShowingFullScreenDismissButton;

		public bool isFeedbackStyle;

		public CandyImageOptions bgOptions;

		public IguiOptions[] candyOptions;

		public int damage;

		public Scenario scenario;

		public UIScenarioCardOptions(bool isShowingDismissButton, bool isShowingFullScreenDismissButton, bool isFeedbackStyle, CandyImageOptions bgOptions, IguiOptions[] candyOptions, int damage, Scenario scenario = null)
		{
		}
	}
}
