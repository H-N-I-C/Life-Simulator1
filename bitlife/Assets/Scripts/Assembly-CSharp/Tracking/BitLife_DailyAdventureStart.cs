using System.Collections.Generic;

namespace Tracking
{
	public class BitLife_DailyAdventureStart : GameTelemetryEvent
	{
		private string DailyAdventureID__BackingField;

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

		public override Dictionary<string, string> GetData()
		{
			return null;
		}
	}
}
