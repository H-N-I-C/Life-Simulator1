using System.Collections.Generic;

namespace Tracking
{
	public class TempBitLifeVampirePlayerTurnsIntoVampire : IGameTelemetryEvent
	{
		private string Method__BackingField;

		public string Method
		{
			get
			{
				return Method__BackingField;
			}
			set
			{
				Method__BackingField = value;
			}
		}

		public Dictionary<string, string> GetData()
		{
			return null;
		}
	}
}
