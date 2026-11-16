using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Containers
{
	public class UIDarklyPaywallChallengeVault : UIDarkly
	{
		private const string BULLET_NAME = "challenge";

		private readonly Vector2 EMOJI_SIZE_LARGE;

		private readonly Vector2 EMOJI_SIZE_SMALL;

		private const float GAP_SIZE_LARGE = 45f;

		private const float GAP_SIZE_SMALL = 33f;

		private const float ITEM_HEIGHT_LARGE = 160f;

		private const float ITEM_HEIGHT_SMALL = 91f;

		private const float STACK_HEIGHT_LARGE = 1846f;

		private const float ENTRY_TOTAL_MARGIN_VERTICAL = 60f;

		private RectTransform stack;

		private CandyImage roundedBackground;

		private RectTransform entryTemplate;

		private CandyImage entryTemplateBackground;

		private CandyText entryTemplateText;

		private CandyImage entryTemplateEmoji;

		private RectTransform entryTitleContainer;

		private Color itemBgColor;

		private RectTransform contentParent;

		private RectTransform spacer5;

		private CandyButton buyButton;

		private CandyImage buyButtonEmoji;

		private HorizontalLayoutGroup buyButtonTitleGroup;

		private CandyButton restorePurchaseButton;

		private UIAreaToggle singlePurchaseToggle;

		private bool isInitialDarkly;

		private int darklyType;

		private int IAP_Origin;

		private string price;

		private Scenario followUpScenario;

		private Scenario followUpChainedScenario;

		public void ReSetup()
		{
		}

		public void Setup(bool isInitialDarkly, int darklyType, int IAP_Origin, Scenario scenario)
		{
		}

		private Dictionary<string, List<string>> GetPaywallBulletDictionary()
		{
			return null;
		}

		private void RestoreButtonPress()
		{
		}

		private void BuyButtonPress()
		{
		}

		private void ExitPaywall()
		{
		}

		public void LaunchFollowUpScenario()
		{
		}
	}
}
