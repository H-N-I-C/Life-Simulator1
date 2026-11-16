using System.Collections.Generic;

namespace Tracking
{
	public class BitLifeUIView : IGameTelemetryEvent
	{
		private string ScreenName__BackingField;

		public string ScreenName
		{
			get
			{
				return ScreenName__BackingField;
			}
			set
			{
				ScreenName__BackingField = value;
			}
		}

		public Dictionary<string, string> GetData()
		{
			return null;
		}
	}
}
