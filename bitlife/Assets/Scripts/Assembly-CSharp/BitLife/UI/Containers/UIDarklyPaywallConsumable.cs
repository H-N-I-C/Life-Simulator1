using BitLife.UI.CandyObjects;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Containers
{
	public class UIDarklyPaywallConsumable : UIDarklyMarketPlace
	{
		private sealed class _003C_003Ec__DisplayClass18_0
		{
			public IAP_TYPE iapType;

			public UIDarklyPaywallConsumable _003C_003E4__this;

			internal bool _003CSetup_003Eb__0()
			{
				return false;
			}

			internal string _003CSetup_003Eb__1()
			{
				return null;
			}

			internal string _003CSetup_003Eb__2()
			{
				return null;
			}
		}

		private const string UAP_TITLE_STREAKS = "Streak Saver";

		private const string UAP_TITLE_BATTLE_PASS = "Seasonal Battle Pass";

		private const string BG_STREAKS = "a16_darkSeasonsStreakBg";

		private RectTransform stack;

		private CandyImage backgroundImage;

		private RectTransform contentParent;

		private RectTransform spacer5;

		private CandyButton buyButton;

		private CandyImage buyButtonEmoji;

		private HorizontalLayoutGroup buyButtonTitleGroup;

		private CandyButton restorePurchaseButton;

		private AccessibleCandyButton uapButton;

		private CandyText priceTag;

		private int darklyType;

		private int IAP_Origin;

		private string price;

		private IAP_TYPE _iapType;

		public void ReSetup()
		{
		}

		public void Setup(bool isInitialDarkly, int darklyType, int IAP_Origin, Scenario followUpScenario, Scenario followUpChainedScenario, IAP_TYPE iapType)
		{
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
	}
}
