using System.Collections.Generic;

namespace Tracking
{
	public class BitLife_StreakSaved : GameTelemetryEvent
	{
		private int SavedStreakAmount__BackingField;

		public int SavedStreakAmount
		{
			get
			{
				return SavedStreakAmount__BackingField;
			}
			set
			{
				SavedStreakAmount__BackingField = value;
			}
		}

		public override Dictionary<string, string> GetData()
		{
			return null;
		}
	}
}
