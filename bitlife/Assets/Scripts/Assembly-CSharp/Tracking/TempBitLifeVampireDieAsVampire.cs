using System.Collections.Generic;

namespace Tracking
{
	public class TempBitLifeVampireDieAsVampire : IGameTelemetryEvent
	{
		private string DeathType__BackingField;

		public string DeathType
		{
			get
			{
				return DeathType__BackingField;
			}
			set
			{
				DeathType__BackingField = value;
			}
		}

		public Dictionary<string, string> GetData()
		{
			return null;
		}
	}
}
