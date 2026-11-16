using BitLife.UI.CandyObjects;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Containers
{
	public class UIDarklyPaywallGenericIAP : UIDarklyMarketPlace
	{
		private sealed class _003C_003Ec__DisplayClass21_0
		{
			public IAP_TYPE iapType;

			public UIDarklyPaywallGenericIAP _003C_003E4__this;

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

		public const string PAYWALL_NAME = "GenericContentPaywall";

		private static readonly Vector2 VISUAL_SIZE;

		private static readonly Color DEFAULT_BLUE;

		private RectTransform stack;

		private CandyImage backgroundImage;

		private CandyButton buyButton;

		private CandyImage buyButtonEmoji;

		private HorizontalLayoutGroup buyButtonTitleGroup;

		private CandyButton restorePurchaseButton;

		private AccessibleCandyButton uapButton;

		private CandyText priceTag;

		private RectTransform parentPrefab;

		private RectTransform visualPlaceholderRect;

		private GameObject placeholderFallback;

		private int darklyType;

		private int iapOrigin;

		private string price;

		private IAP_TYPE iapType;

		private IGenericPaywallContent _genericPaywallContent;

		private string _paywallName;

		public void ReSetup()
		{
		}

		public void Setup(bool isInitialDarkly, int darklyType, int IAP_Origin, Scenario followUpScenario, Scenario followUpChainedScenario, IAP_TYPE iapType)
		{
		}

		private string GetUapTitle(IAP_TYPE type)
		{
			return null;
		}

		private void SetupVisual()
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

		public override void RebuildUI()
		{
		}
	}
}
