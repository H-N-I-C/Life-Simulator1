using System.Collections.Generic;

namespace Tracking
{
	public class GameTelemetryEvent : IGameTelemetryEvent
	{
		public virtual Dictionary<string, string> GetData()
		{
			return null;
		}

		protected string FromBool(bool value)
		{
			return null;
		}
	}
}
