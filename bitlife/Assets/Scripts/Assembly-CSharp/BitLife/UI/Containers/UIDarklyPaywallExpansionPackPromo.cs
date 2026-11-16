using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Containers
{
	public class UIDarklyPaywallExpansionPackPromo : UIDarkly
	{
		private sealed class _003CUpdateLayoutRoutine_003Ed__34 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIDarklyPaywallExpansionPackPromo _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CUpdateLayoutRoutine_003Ed__34(int _003C_003E1__state)
			{
			}

			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			void IEnumerator.Reset()
			{
			}
		}

		private const string TITLE_CASINO = "Casino Expansion Pack";

		private const string TITLE_LUXURY = "Luxury Lifestyle";

		private const string TITLE_INVESTOR = "Investor Expansion Pack";

		private const string TITLE_LANDLORD = "Land Lord Expansion Pack";

		private const string TITLE_OUTDOOR_LIFESTYLE = "Outdoor Lifestyle";

		private const string DETAILS_INVESTOR = "Are you a whale with cash to burn? Make (or lose) tons of money in stocks, bonds, crypto, and more in this expansion pack!";

		private const string DETAILS_CASINO = "Calling all high rollers! Bet it all on BitLife with the Casino Expansion Pack!!";

		private const string DETAILS_LUXURY = "#wip";

		private const string DETAILS_OUTDOOR_LIFESTYLE = "#wip";

		private const string DETAILS_LANDLORD = "";

		private const string DOWNLOAD_BUTTON_TEXT = "Unlock on this life";

		private const string DOWNLOAD_BUTTON_IMAGE = "a16_alertButtonVideoIcon";

		private const string BUY_BUTTON_FORMAT_TEXT = "buy it for {0} instead";

		private const string PREFIX = "a16_darkExPackPromo_";

		private const string IMAGE_INVESTOR = "a16_darkExPackPromo_Investor";

		private const string IMAGE_CASINO = "a16_darkExPackPromo_Casino";

		private const string IMAGE_LUXURY = "a16_darkExPackPromo_Casino";

		private const string IMAGE_OUTDOOR_LIFESTYLE = "a16_darkExPackPromo_OutdoorLifestyle";

		private RectTransform layoutGroupRect;

		private CandyImage headerImage;

		private CandyText headerTitle;

		private CandyText headerDetails;

		private CandyText dealDetails;

		private CandyButton downloadButton;

		private CandyImage downloadButtonEmoji;

		private HorizontalLayoutGroup downloadButtonTitleGroup;

		private CandyButton buyButton;

		private bool isInitialDarkly;

		private int darklyType;

		private int IAP_Origin;

		private int exPackType;

		public void ReSetup()
		{
		}

		public void Setup(bool isInitialDarkly, int darklyType, int IAP_Origin, int exPackType)
		{
		}

		public override void UpdateDarklyLayout()
		{
		}

		private IEnumerator UpdateLayoutRoutine()
		{
			return null;
		}

		private void DownloadButtonPress()
		{
		}

		private void BuyButtonPress()
		{
		}

		private void ExitPaywall()
		{
		}
	}
}
