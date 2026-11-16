using System;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Containers
{
	public class UIDarklyPaywallItem : UIDarklyMarketPlace
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<bool> _003C_003E9__36_0;

			internal bool _003CSetup_003Eb__36_0()
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass36_0
		{
			public UIDarklyPaywallItem _003C_003E4__this;

			public int itemType;

			internal string _003CSetup_003Eb__1()
			{
				return null;
			}

			internal string _003CSetup_003Eb__2()
			{
				return null;
			}
		}

		private readonly Vector2 EMOJI_SIZE_SMALL;

		private const float GAP_SIZE_PIGGY_BANK = 0f;

		private const float ENTRY_TOTAL_MARGIN_VERTICAL = 60f;

		private const float TOP_SPACE_PIGGY_BANK = 895f;

		private const float BOTTOM_FEATURE_PIGGY_BANK = -73f;

		private const string COLOR_WHITE = "FFFFFF";

		private const string COLOR_YELLOW_GOLD = "F0BD5E";

		private const string COLOR_DARK_MAGENTA = "592248";

		private const string BG_PIGGY = "a16_darkItemPiggyBankBg";

		private const string LOGO_PIGGY = "a16_darkItemPiggyBankLogo";

		private const string UAP_TITLE_PIGGY = "Piggy Bank Item";

		private const string BULLET_NAME_PIGGY = "";

		private RectTransform stack;

		private CandyImage backgroundBorder;

		private CandyImage backgroundColor;

		private CandyImage backgroundImage;

		private CandyImage logoImage;

		private RectTransform entryTemplate;

		private CandyImage entryTemplateBackground;

		private CandyText entryTemplateText;

		private CandyImage entryTemplateEmoji;

		private RectTransform entryTitleContainer;

		private Color itemBgColor;

		private RectTransform contentParent;

		private CandyImage bottomAccentFeature;

		private RectTransform spacer5;

		private CandyButton buyButton;

		private CandyImage buyButtonEmoji;

		private HorizontalLayoutGroup buyButtonTitleGroup;

		private CandyButton restorePurchaseButton;

		private UIAreaToggle singlePurchaseToggle;

		private int darklyType;

		private int IAP_Origin;

		private string price;

		private int itemType;

		public void ReSetup()
		{
		}

		public void Setup(bool isInitialDarkly, int darklyType, int IAP_Origin, Scenario followUpScenario = null, Scenario followUpChainedScenario = null, int itemType = 0)
		{
		}

		private Dictionary<string, List<string>> PiggyBankBulletPoints()
		{
			return null;
		}

		private void RestoreButtonPress()
		{
		}

		protected override void BuyButtonPress()
		{
		}

		protected override void BuyWithBitLifeButtonPress()
		{
		}

		public override void RebuildUI()
		{
		}
	}
}
