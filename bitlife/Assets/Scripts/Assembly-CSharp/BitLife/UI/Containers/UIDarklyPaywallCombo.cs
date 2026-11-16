using System;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Containers
{
	public class UIDarklyPaywallCombo : UIDarklyMarketPlace
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<bool> _003C_003E9__53_0;

			internal bool _003CSetup_003Eb__53_0()
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass53_0
		{
			public string originalPriceString;

			internal string _003CSetup_003Eb__1()
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass53_1
		{
			public IAP_TYPE typeItem;

			internal string _003CSetup_003Eb__2()
			{
				return null;
			}
		}

		private readonly Vector2 LOGO_SIZE_BITIZENSHIP;

		private readonly Vector2 BUY_BUTTON_SIZE_BITIZENSHIP;

		private readonly Vector2 EMOJI_SIZE_LARGE;

		private readonly Vector2 EMOJI_SIZE_SMALL;

		private const float GAP_SIZE_LARGE = 45f;

		private const float GAP_SIZE_SMALL = 33f;

		private const float ITEM_HEIGHT_LARGE = 160f;

		private const float ITEM_HEIGHT_SMALL = 91f;

		private const float STACK_HEIGHT_LARGE = 1846f;

		private const float CONTENT_POS_BITIZEN_ONLY = -350f;

		private const float CONTENT_POS_GODMODE_ONLY = -366f;

		private const float LOGO_POS_BITIZEN_ONLY = -186f;

		private const float LOGO_POS_GODMODE_ONLY = -196f;

		private RectTransform stack;

		private RectTransform entryTemplate;

		private CandyImage entryTemplateBackground;

		private CandyText entryTemplateText;

		private CandyImage entryTemplateEmoji;

		private RectTransform entryTitleContainer;

		private Color itemBgColor;

		private RectTransform contentParent;

		private GameObject lineBreak;

		private GameObject lineBreakSpacer;

		private RectTransform spacer5;

		private CandyButton buyButton;

		private CandyImage buyButtonEmoji;

		private HorizontalLayoutGroup buyButtonTitleGroup;

		private CandyButton restorePurchaseButton;

		private UIAreaToggle singlePurchaseToggle;

		private UIAreaToggle comboPurchaseToggle;

		private UIToggleSetManager toggleSetManager;

		private GameObject bestValueBadge;

		private CandyText bitizenshipClarifier;

		private CandyText bundleClarifier;

		private const string RewardedVideoClarifier = "You can still watch videos for rewards";

		private CandyText adRemovalBenefit;

		private bool wordingLayoutReady;

		private bool isComboOffer;

		private float originalStackHeight;

		private float originalSingleHeight;

		private float originalComboHeight;

		private string singlePurchaseLabel;

		private string comboPurchaseLabel;

		private int darklyType;

		private int IAP_Origin;

		private bool isBitizenIAP;

		private string priceBitizen;

		private string priceGodMode;

		private string priceCombo;

		public void ReSetup()
		{
		}

		private void OnDestroy()
		{
		}

		public void UAP_FeaturedToggleCallbackOnSelection()
		{
		}

		public void UAP_ComboToggleCallbackOnSelection()
		{
		}

		public void Setup(bool isInitialDarkly, int darklyType, int IAP_Origin, Scenario scenario)
		{
		}

		private void RestoreAdWordingLayout()
		{
		}

		private void ApplyAdWording()
		{
		}

		private string AddCurrency(string p1, string p2)
		{
			return null;
		}

		private string SubtractCurrency(string p1, string p2)
		{
			return null;
		}

		private Dictionary<string, List<string>> GetPaywallBulletDictionary(bool forceSingleLayoutOnBitizenPaywall, bool forceSingleLayoutOnGodModePaywall)
		{
			return null;
		}

		private void RestoreButtonPress()
		{
		}

		protected override void BuyButtonPress()
		{
		}

		protected override void ExitPaywall()
		{
		}

		protected override void BuyWithBitLifeButtonPress()
		{
		}

		private void CheckToggleSelection()
		{
		}
	}
}
