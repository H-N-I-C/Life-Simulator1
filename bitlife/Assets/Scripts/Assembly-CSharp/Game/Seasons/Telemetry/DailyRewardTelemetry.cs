using System;
using System.Collections.Generic;
using Game.Seasons.Models;
using Tracking;

namespace Game.Seasons.Telemetry
{
	public static class DailyRewardTelemetry
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<string, bool> _003C_003E9__20_1;

			internal bool _003CIngest_003Eb__20_1(string key)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass17_0
		{
			public string action;

			internal void _003CClick_003Eb__0()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass18_0
		{
			public BitLifeUIViews screen;

			internal void _003CView_003Eb__0()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass20_0
		{
			public IEnumerable<DailyRewardGrantModel> receipts;

			public DailyRewardGrantModel directReceipt;

			public DailyStreakResetModel reset;

			internal void _003CIngest_003Eb__0()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass21_0
		{
			public string canonicalItemId;

			public string surface;

			public bool accepted;

			internal void _003CEmitRentalActivation_003Eb__0()
			{
			}
		}

		public const string DailyRewardEntry = "DailyRewardEntry";

		public const string MarketplaceEntry = "MarketplaceEntry";

		public const string ClaimDailyReward = "ClaimDailyReward";

		public const string MarketplaceOffer = "MarketplaceOffer";

		public const string PurchaseConfirm = "PurchaseConfirm";

		public const string PurchaseCancel = "PurchaseCancel";

		public const string ClaimLifeReward = "ClaimLifeReward";

		public const string PanelDismiss = "PanelDismiss";

		private const int HistoryLimit = 512;

		private static string historyKey;

		private static string latestResetKey;

		private static HashSet<string> seen;

		private static Queue<string> history;

		public static bool IsDevelopmentStream => false;

		private static bool IsAvailable => false;

		public static void Click(string action)
		{
		}

		public static void View(BitLifeUIViews screen)
		{
		}

		public static void Ingest(SeasonsDailyRewardModel model)
		{
		}

		public static void Ingest(IEnumerable<DailyRewardGrantModel> receipts, DailyStreakResetModel reset = null, DailyRewardGrantModel directReceipt = null)
		{
		}

		public static bool EmitRentalActivation(string canonicalItemId, string surface)
		{
			return false;
		}

		public static bool TryGetCanonicalItemId(string identifier, out string canonical)
		{
			canonical = null;
			return false;
		}

		public static void ResetCachedHistory()
		{
		}

		private static void EnsureHistory(string account)
		{
		}

		private static void Remember(string operation)
		{
		}

		private static void RememberReset(string operation)
		{
		}

		private static void TrimHistory()
		{
		}

		internal static string Hash(string value)
		{
			return null;
		}

		private static void Safely(Action action)
		{
		}
	}
}
