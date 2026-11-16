using System;

namespace Game.Dynamic.Marketplace
{
	public static class NativeMarketplaceTimedAccess
	{
		public static bool IsOwned(IAP_TYPE type, bool existingOwnership)
		{
			return false;
		}

		public static bool IsOwned(int selectionAction, bool existingOwnership)
		{
			return false;
		}

		public static bool ResolveOwnership(bool existingOwnership, bool permanentlyOwned, double remainingSeconds, bool hasTimedComponent = false)
		{
			return false;
		}

		public static bool TryGetAvailableTime(int selectionAction, out string text)
		{
			text = null;
			return false;
		}

		public static double GetRemainingSeconds(IAP_TYPE type)
		{
			return 0.0;
		}

		private static double GetInheritedRemainingSeconds(IAP_TYPE type)
		{
			return 0.0;
		}

		public static double GetBundleRemainingSeconds(IAP_TYPE type, Func<IAP_TYPE, bool> isPermanent, Func<IAP_TYPE, double> remainingForComponent)
		{
			return 0.0;
		}

		public static double RemainingSeconds(int grantedRemainingMinutes, DateTime refreshedAt, DateTime now)
		{
			return 0.0;
		}

		public static string FormatDuration(double remainingSeconds)
		{
			return null;
		}

		public static bool IsSupported(IAP_TYPE type)
		{
			return false;
		}

		public static bool TryGetIapType(int selectionAction, out IAP_TYPE type)
		{
			type = default;
			return false;
		}
	}
}
