using System;
using Backend;
using BitLife.UI.CandyObjects;
using Game.Dynamic.Marketplace;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIDarklyMarketPlace : UIDarkly
	{
		protected CandyImage roundedBackground;

		protected GameObject newPaymentFlow;

		protected GameObject newPlaymentFlow_US;

		protected GameObject newPaymentFlow_Others;

		protected CandyButton buyWithBitLifeButton_iOS_US;

		protected CandyButton buyWithAppleButton_iOS_US;

		protected CandyText labelPriceApple_iOS_US;

		protected CandyText labelPriceBitLife_iOS_US;

		protected CandyButton buyWithBitLifeButton;

		protected CandyButton buyWithAppleButton;

		protected CandyText labelPriceApple;

		protected UIMarketPlacePurchase purchaseFlowWebShopSolo;

		private bool IsUSWebShop__BackingField;

		private bool IsWebShopSolo__BackingField;

		private WebShopStatus ShopStatus__BackingField;

		protected bool isInitialDarkly;

		protected Scenario followUpScenario;

		protected Scenario followUpChainedScenario;

		protected bool IsUSWebShop
		{
			get
			{
				return IsUSWebShop__BackingField;
			}
			private set
			{
				IsUSWebShop__BackingField = value;
			}
		}

		protected UIShopType ShopType => UIShopType.Original;

		protected bool IsWebShopSolo
		{
			get
			{
				return IsWebShopSolo__BackingField;
			}
			private set
			{
				IsWebShopSolo__BackingField = value;
			}
		}

		protected WebShopStatus ShopStatus
		{
			get
			{
				return ShopStatus__BackingField;
			}
			private set
			{
				ShopStatus__BackingField = value;
			}
		}

		protected virtual bool SetupPurchaseFlow(Func<bool> enabled, Func<string> platformPrice, Func<string> webShopPrice)
		{
			return false;
		}

		protected static void SetUapLabel(CandyButton button, string label)
		{
		}

		protected static string BuildBuyUapLabel(string action, string price)
		{
			return null;
		}

		protected virtual void ShowPaymentFlow(bool original, bool webShopSolo)
		{
		}

		protected virtual void ExitPaywall()
		{
		}

		protected virtual void BuyWithBitLifeButtonPress()
		{
		}

		protected virtual void BuyButtonPress()
		{
		}

		public virtual void LaunchFollowUpScenario()
		{
		}
	}
}
