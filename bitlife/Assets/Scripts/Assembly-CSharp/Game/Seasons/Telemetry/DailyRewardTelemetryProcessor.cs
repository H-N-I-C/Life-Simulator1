using System;
using System.Collections.Generic;
using Game.Seasons.Models;
using Tracking;

namespace Game.Seasons.Telemetry
{
	public sealed class DailyRewardTelemetryProcessor
	{
		private readonly Func<string, bool> hasSeen;

		private readonly Action<string> remember;

		private readonly Action<IGameTelemetryEvent> emit;

		public DailyRewardTelemetryProcessor(Func<string, bool> hasSeen, Action<string> remember, Action<IGameTelemetryEvent> emit)
		{
		}

		public bool RecordReceipt(string account, DailyRewardGrantModel receipt, bool allowDebug = false)
		{
			return false;
		}

		public static IEnumerable<DailyRewardGrantModel> GetReplayReceipts(SeasonsDailyRewardModel model)
		{
			return null;
		}

		public bool RecordReset(string account, DailyStreakResetModel reset, bool allowDebug = false)
		{
			return false;
		}

		private bool RecordOnce(string key, IGameTelemetryEvent payload)
		{
			return false;
		}

		public static string OperationKey(string account, string kind, string id)
		{
			return null;
		}

		public static bool TryCreateTransaction(string account, DailyRewardGrantModel receipt, bool allowDebug, out BitLife_EconomyTransaction transaction)
		{
			transaction = null;
			return false;
		}
	}
}
