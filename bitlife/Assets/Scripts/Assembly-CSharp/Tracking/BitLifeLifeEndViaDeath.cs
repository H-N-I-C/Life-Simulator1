using System.Collections.Generic;

namespace Tracking
{
	public class BitLifeLifeEndViaDeath : IGameTelemetryEvent
	{
		private string LifeGUID__BackingField;

		private int AgeOfDeathActual__BackingField;

		private string DeathReason__BackingField;

		public string LifeGUID
		{
			get
			{
				return LifeGUID__BackingField;
			}
			set
			{
				LifeGUID__BackingField = value;
			}
		}

		public int AgeOfDeathActual
		{
			get
			{
				return AgeOfDeathActual__BackingField;
			}
			set
			{
				AgeOfDeathActual__BackingField = value;
			}
		}

		public string DeathReason
		{
			get
			{
				return DeathReason__BackingField;
			}
			set
			{
				DeathReason__BackingField = value;
			}
		}

		public Dictionary<string, string> GetData()
		{
			return null;
		}
	}
}
