using System.Collections.Generic;

namespace Tracking
{
	public class BitLife_DailyAdventureComplete : GameTelemetryEvent
	{
		private string DailyAdventureID__BackingField;

		private string Result__BackingField;

		public const string Win = "win";

		public const string Lose = "lose";

		public const string Complete = "complete";

		public const string Retry = "retry";

		public const string GiveUp = "give_up";

		public string DailyAdventureID
		{
			get
			{
				return DailyAdventureID__BackingField;
			}
			set
			{
				DailyAdventureID__BackingField = value;
			}
		}

		public string Result
		{
			get
			{
				return Result__BackingField;
			}
			set
			{
				Result__BackingField = value;
			}
		}

		public override Dictionary<string, string> GetData()
		{
			return null;
		}
	}
}
