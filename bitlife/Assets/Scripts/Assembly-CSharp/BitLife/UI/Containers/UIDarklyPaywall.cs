using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIDarklyPaywall : UIDarkly
	{
		private readonly Vector2 LOGO_SIZE_GOD_MODE;

		private readonly Vector2 LOGO_SIZE_BITIZENSHIP;

		private readonly Vector2 EMOJI_SIZE_GOD_MODE;

		private readonly Vector2 EMOJI_SIZE_BITIZENSHIP;

		private const float GAP_SIZE_GOD_MODE = 45f;

		private const float GAP_SIZE_BITIZENSHIP = 33f;

		private const float ITEM_HEIGHT_GOD_MODE = 150f;

		private const float ITEM_HEIGHT_BITIZENSHIP = 91f;

		private readonly Color ITEM_BG_COLOR;

		private readonly Vector2 BUY_BUTTON_SIZE_GOD_MODE;

		private readonly Vector2 BUY_BUTTON_SIZE_BITIZENSHIP;

		private CandyImage roundedBackground;

		private CandyImage logo;

		private CandyText details;

		private RectTransform entry;

		private CandyImage entryTemplateBackground;

		private CandyText entryTemplateText;

		private CandyImage entryTemplateEmoji;

		private RectTransform entryTitleContainer;

		private RectTransform contentParent;

		private CandyButton buyButton;

		private CandyButton removeAdsButton;

		private CandyButton restorePurchaseButton;

		private GameObject removeAdsTopSpace;

		private GameObject restorePurchaseTopSpace;

		private CandyScroller scroller;

		private Transform buttonObjectStack;

		private Transform sperm;

		private CandyText price;

		private bool isInitialDarkly;

		private int darklyType;

		private int IAP_Origin;

		public void Setup(bool isInitialDarkly, int darklyType, int IAP_Origin)
		{
		}

		private Dictionary<string, List<string>> GetPaywallBulletDictionary()
		{
			return null;
		}

		private void BuyButtonPress()
		{
		}

		private void RemoveAdsButtonPress()
		{
		}

		private void RestorePurchasesButtonPress()
		{
		}

		private void ExitPaywall()
		{
		}
	}
}
