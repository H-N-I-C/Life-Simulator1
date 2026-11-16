using System.Collections.Generic;

namespace Tracking
{
	public sealed class TempBitLife1asMigrationWelcome : TempBitLife1asMigrationEvent
	{
		private readonly bool _isPlayerNew;

		public TempBitLife1asMigrationWelcome(string appsFlyerId, string deviceId, string backendId)
		{
		}

		public static TempBitLife1asMigrationWelcome ForCurrentPlayer()
		{
			return null;
		}

		public static bool HasSeenPlayerBefore(string appsFlyerId, string deviceId, string backendId)
		{
			return false;
		}

		public override Dictionary<string, string> GetData()
		{
			return null;
		}
	}
}
