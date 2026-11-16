using System.Collections.Generic;

namespace Tracking
{
	public class BitLifeAdRewardVideoDecisionMade : IGameTelemetryEvent
	{
		private string RewardVideo__BackingField;

		private bool IsPlayerDecisionToWatch__BackingField;

		public string RewardVideo
		{
			get
			{
				return RewardVideo__BackingField;
			}
			set
			{
				RewardVideo__BackingField = value;
			}
		}

		public bool IsPlayerDecisionToWatch
		{
			get
			{
				return IsPlayerDecisionToWatch__BackingField;
			}
			set
			{
				IsPlayerDecisionToWatch__BackingField = value;
			}
		}

		public Dictionary<string, string> GetData()
		{
			return null;
		}

		public static void NotifyNotWatched(REWARDED_VIDEO_ORIGIN origin)
		{
		}

		public static void SendEvent(string rewardVideo, bool isPlayerDecisionToWatch)
		{
		}
	}
}
