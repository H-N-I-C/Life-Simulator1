using System;
using BitLife.UI.CandyObjects;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Containers
{
	public class UIDarklyPaywallComboCareer : UIDarklyMarketPlace
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<bool> _003C_003E9__16_0;

			internal bool _003CSetup_003Eb__16_0()
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass16_0
		{
			public IAP_TYPE typeItem;

			internal string _003CSetup_003Eb__1()
			{
				return null;
			}

			internal string _003CSetup_003Eb__2()
			{
				return null;
			}
		}

		private RectTransform stack;

		private GameObject lineBreak;

		private GameObject lineBreakSpacer;

		private RectTransform spacer5;

		private CandyButton buyButton;

		private CandyImage buyButtonEmoji;

		private HorizontalLayoutGroup buyButtonTitleGroup;

		private CandyButton restorePurchaseButton;

		private UIAreaToggle_Career singlePurchaseToggle;

		private UIAreaToggle_Career comboPurchaseToggle;

		private UIToggleSetManager_Career toggleSetManagerCareer;

		private GameObject bestValueBadge;

		private int darklyType;

		private int IAP_Origin;

		private bool userIsBoss;

		public void ReSetup()
		{
		}

		public void Setup(bool isInitialDarkly, int darklyType, int IAP_Origin, Scenario followUpScenario = null, Scenario followUpChainedScenario = null)
		{
		}

		private string AddCurrency(string p1, string p2)
		{
			return null;
		}

		private void RestoreButtonPress()
		{
		}

		private void OnDestroy()
		{
		}

		protected override void BuyButtonPress()
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
