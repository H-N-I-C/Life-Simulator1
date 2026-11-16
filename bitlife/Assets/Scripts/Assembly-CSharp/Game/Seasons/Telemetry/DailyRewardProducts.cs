using System.Collections.Generic;

namespace Game.Seasons.Telemetry
{
	public static class DailyRewardProducts
	{
		private static readonly Dictionary<IAP_TYPE, string> Canonical;

		public static bool IsSupported(IAP_TYPE type)
		{
			return false;
		}

		public static bool TryGetIapType(string identifier, out IAP_TYPE type)
		{
			type = default;
			return false;
		}

		public static string CanonicalFor(IAP_TYPE type)
		{
			return null;
		}

		public static string TrialItemIdFor(IAP_TYPE type)
		{
			return null;
		}

		public static bool TryGetCanonicalItemId(string identifier, out string canonical)
		{
			canonical = null;
			return false;
		}

		public static string NormalizeProductIdentifier(string identifier)
		{
			return null;
		}

		public static bool GrantsContent(string rentalCanonical, string contentCanonical)
		{
			return false;
		}
	}
}
