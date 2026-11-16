using System.Collections.Generic;

namespace Tracking
{
	public class BitLifeAdRewardVideoComplete : IGameTelemetryEvent
	{
		private string RewardVideo__BackingField;

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

		public Dictionary<string, string> GetData()
		{
			return null;
		}
	}
}
