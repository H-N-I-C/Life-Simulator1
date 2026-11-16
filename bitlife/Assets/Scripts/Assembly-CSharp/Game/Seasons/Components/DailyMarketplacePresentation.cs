using System;
using Game.Seasons.Models;
using UnityEngine;

namespace Game.Seasons.Components
{
	public static class DailyMarketplacePresentation
	{
		public static bool IsSupportedOffer(DailyMarketplaceOfferModel offer)
		{
			return false;
		}

		public static bool TryGetNativeProduct(DailyMarketplaceOfferModel offer, out ListItem product)
		{
			product = null;
			return false;
		}

		public static bool IsPermanentlyOwnedForPresentation(DailyMarketplaceOfferModel offer, Func<IAP_TYPE, bool> nativeOwnership = null)
		{
			return false;
		}

		public static bool CanPurchaseForPresentation(DailyMarketplaceOfferModel offer, Func<IAP_TYPE, bool> nativeOwnership = null)
		{
			return false;
		}

		public static bool TryResolveIapType(string bundleId, out IAP_TYPE type)
		{
			type = default;
			return false;
		}

		public static bool TryResolveIapType(string bundleId, Func<IAP_TYPE, string> bundleForType, out IAP_TYPE type)
		{
			type = default;
			return false;
		}

		private static string NormalizeBundle(string bundleId)
		{
			return null;
		}

		public static string Title(DailyMarketplaceOfferModel offer, ListItem product = null)
		{
			return null;
		}

		public static string CompactTitle(DailyMarketplaceOfferModel offer, ListItem product = null)
		{
			return null;
		}

		public static string AccessLabel(DailyMarketplaceOfferModel offer)
		{
			return null;
		}

		public static string UnlockLabel(DailyMarketplaceOfferModel offer)
		{
			return null;
		}

		public static string Description(DailyMarketplaceOfferModel offer, ListItem product = null)
		{
			return null;
		}

		public static Sprite Icon(DailyMarketplaceOfferModel offer, ListItem product = null)
		{
			return null;
		}

		public static string PurchaseConfirmation(DailyMarketplaceOfferModel offer)
		{
			return null;
		}

		public static string PurchaseSuccess(DailyMarketplaceOfferModel offer)
		{
			return null;
		}

		private static string ProductName(DailyMarketplaceOfferModel offer, ListItem product = null)
		{
			return null;
		}

		private static string AccessDuration(DailyMarketplaceOfferModel offer)
		{
			return null;
		}

		public static string Duration(int minutes)
		{
			return null;
		}
	}
}
