using System.Collections.Generic;

namespace Tracking
{
	public sealed class BitLife_RentalContentActivated : GameTelemetryEvent
	{
		private string ItemID__BackingField;

		private string Surface__BackingField;

		private int? StreakSnapshot__BackingField;

		private string UserIdSnapshot__BackingField;

		public string ItemID
		{
			get
			{
				return ItemID__BackingField;
			}
			set
			{
				ItemID__BackingField = value;
			}
		}

		public string InteractionType => null;

		public string Surface
		{
			get
			{
				return Surface__BackingField;
			}
			set
			{
				Surface__BackingField = value;
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
