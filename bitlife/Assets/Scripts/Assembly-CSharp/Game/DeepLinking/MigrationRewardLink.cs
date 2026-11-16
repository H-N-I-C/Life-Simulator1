using System;

namespace Game.DeepLinking
{
	public static class MigrationRewardLink
	{
		private const string NativeScheme = "bitlife";

		private static readonly DateTime MigrationEndExclusiveUtc;

		public static bool IsWithinMigrationWindow(DateTime? utcNow = null)
		{
			return false;
		}

		public static bool IsMigrationRoute(Uri uri)
		{
			return false;
		}

		public static bool IsMigrationRewardId(string rewardId)
		{
			return false;
		}

		public static bool TryGetPreferredLanguage(Uri uri, out string preferredLanguage, DateTime? utcNow = null)
		{
			preferredLanguage = null;
			return false;
		}
	}
}
