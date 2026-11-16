using System;
using System.Collections.Generic;
using Game.Seasons.Models;

namespace Game.Seasons.Telemetry
{
	public static class DailyRentalActivation
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action<string> _003C_003E9__0_0;

			internal void _003CRecord_003Eb__0_0(string key)
			{
			}
		}

		public static void Record(IAP_TYPE type, string surface)
		{
		}

		public static void RecordLegacyContentUse(string eventName, Dictionary<string, string> values)
		{
		}

		public static IAP_TYPE CommittedLegacyProduct(string eventName, Dictionary<string, string> values)
		{
			return IAP_TYPE.NONE;
		}

		public static bool IsLegacyContentUse(string eventName, Dictionary<string, string> values)
		{
			return false;
		}

		public static string CanonicalFor(IAP_TYPE type)
		{
			return null;
		}

		public static void RecordSpecialCareerUse(SPECIAL_JOB_TYPE career)
		{
		}

		public static bool HasRewardedVideoAccess(IAP_TYPE type, bool unlocked, int expansionPack)
		{
			return false;
		}

		public static IAP_TYPE ExpansionProduct(EXPANSION_PACK_TYPE expansion)
		{
			return IAP_TYPE.NONE;
		}

		public static DailyRentalAccessSource ResolveSource(string canonical, bool permanentlyOwned, bool rewardedVideo, IEnumerable<string> activeMatchingWalletItems, IEnumerable<DailyRentalEntitlementModel> rentals, DateTime now, out string windowId, bool allowDebug = false)
		{
			windowId = null;
			return DailyRentalAccessSource.Unknown;
		}

		public static DailyRentalAccessSource ResolveSource(string canonical, bool permanentlyOwned, bool rewardedVideo, IEnumerable<string> activeMatchingWalletItems, IEnumerable<DailyRentalEntitlementModel> rentals, DateTime now, out string windowId, out string rentalCanonical, bool allowDebug = false)
		{
			windowId = null;
			rentalCanonical = null;
			return DailyRentalAccessSource.Unknown;
		}

		public static bool TryRecord(string account, string canonical, string window, string surface, Func<string, bool> wasRecorded, Action<string> markRecorded, Func<string, string, bool> emit, bool developmentStream = false)
		{
			return false;
		}

		internal static string ActivationKey(string account, string canonical, string window, bool developmentStream)
		{
			return null;
		}
	}
}
