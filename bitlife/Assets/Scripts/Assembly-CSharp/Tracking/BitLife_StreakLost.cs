using System.Collections.Generic;

namespace Tracking
{
	public class BitLife_StreakLost : GameTelemetryEvent
	{
		private int PreviousStreak__BackingField;

		private int? NumRefreshPeriodsMissed__BackingField;

		private int? StreakSnapshot__BackingField;

		private string UserIdSnapshot__BackingField;

		public int PreviousStreak
		{
			get
			{
				return PreviousStreak__BackingField;
			}
			set
			{
				PreviousStreak__BackingField = value;
			}
		}

		public int? NumRefreshPeriodsMissed
		{
			get
			{
				return NumRefreshPeriodsMissed__BackingField;
			}
			set
			{
				NumRefreshPeriodsMissed__BackingField = value;
			}
		}

		public int? StreakSnapshot
		{
			get
			{
				return StreakSnapshot__BackingField;
			}
			set
			{
				StreakSnapshot__BackingField = value;
			}
		}

		public string UserIdSnapshot
		{
			get
			{
				return UserIdSnapshot__BackingField;
			}
			set
			{
				UserIdSnapshot__BackingField = value;
			}
		}

		public override Dictionary<string, string> GetData()
		{
			return null;
		}
	}
}
