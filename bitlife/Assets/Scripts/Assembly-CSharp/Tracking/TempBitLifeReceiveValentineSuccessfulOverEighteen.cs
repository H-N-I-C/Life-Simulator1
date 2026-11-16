using System.Collections.Generic;

namespace Tracking
{
	public class TempBitLifeReceiveValentineSuccessfulOverEighteen : GameTelemetryEvent
	{
		private string QRID__BackingField;

		public string QRID
		{
			get
			{
				return QRID__BackingField;
			}
			set
			{
				QRID__BackingField = value;
			}
		}

		public override Dictionary<string, string> GetData()
		{
			return null;
		}
	}
}
