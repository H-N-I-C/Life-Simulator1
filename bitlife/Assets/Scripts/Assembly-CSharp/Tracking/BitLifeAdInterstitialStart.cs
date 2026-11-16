using System.Collections.Generic;

namespace Tracking
{
	public class BitLifeAdInterstitialStart : IGameTelemetryEvent
	{
		private string Origin__BackingField;

		public string Origin
		{
			get
			{
				return Origin__BackingField;
			}
			set
			{
				Origin__BackingField = value;
			}
		}

		public Dictionary<string, string> GetData()
		{
			return null;
		}

		public Dictionary<string, object> GetDataCleverTap()
		{
			return null;
		}
	}
}
