using System.Collections.Generic;

namespace Game.Seasons.Telemetry
{
	public static class DailyRentalOwnership
	{
		private static readonly IAP_TYPE[] BitizenGod;

		private static readonly IAP_TYPE[] Billionaire;

		private static readonly IAP_TYPE[] Crime;

		private static readonly IAP_TYPE[] Fame;

		private static readonly IAP_TYPE[] Party;

		private static readonly IAP_TYPE[] Time;

		private static readonly IAP_TYPE[] BirthdayLegends;

		private static readonly IAP_TYPE[] BirthdayStarter;

		private static readonly IAP_TYPE[] Starter1;

		private static readonly IAP_TYPE[] Starter2;

		private static readonly IAP_TYPE[] Director;

		public static bool IsPermanentlyOwned(IAP_TYPE type)
		{
			return false;
		}

		public static bool HasPermanentOwnership(MonetizationVars owned, IAP_TYPE type, bool passesAccountCheck, GameVars gameVars = null)
		{
			return false;
		}

		public static IReadOnlyList<IAP_TYPE> GetBundleComponents(IAP_TYPE type)
		{
			return null;
		}
	}
}
