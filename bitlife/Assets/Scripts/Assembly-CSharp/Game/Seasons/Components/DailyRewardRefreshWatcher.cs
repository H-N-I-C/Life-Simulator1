using System;
using Game.Seasons.Models;
using UnityEngine;

namespace Game.Seasons.Components
{
	public sealed class DailyRewardRefreshWatcher : MonoBehaviour
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<DailyMarketplaceOfferModel, bool> _003C_003E9__17_0;

			internal bool _003CLateUpdate_003Eb__17_0(DailyMarketplaceOfferModel offer)
			{
				return false;
			}
		}

		private static DailyRewardRefreshWatcher instance;

		private float nextCheck;

		private List marketplaceList;

		private float nextMarketplaceRender;

		private float nextMarketplaceExpiryRefresh;

		private float nextBenefitCheck;

		private GameManager benefitGame;

		private bool? hadAds;

		private bool? wasBitizen;

		private int? effectiveTheme;

		private bool IsMarketplaceDisplayed()
		{
			return false;
		}

		public static void RefreshDisplayedMarketplace()
		{
		}

		public static void WatchMarketplace(List list)
		{
		}

		public static void CloseDisplayedMarketplace()
		{
		}

		public static void EnsureRunning()
		{
		}

		private void Update()
		{
		}

		private void RefreshAccountBenefits()
		{
		}

		private void LateUpdate()
		{
		}
	}
}
