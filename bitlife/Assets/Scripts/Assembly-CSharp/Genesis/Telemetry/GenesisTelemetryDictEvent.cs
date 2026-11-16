using System.Collections.Generic;
using Tracking;

namespace Genesis.Telemetry
{
	public sealed class GenesisTelemetryDictEvent : IGenesisTelemetryEvent, IGameTelemetryEvent
	{
		private readonly Dictionary<string, string> _payload;

		private readonly string EventName__BackingField;

		public string EventName => EventName__BackingField;

		public GenesisTelemetryDictEvent(string eventName, Dictionary<string, string> payload)
		{
		}

		public Dictionary<string, string> GetData()
		{
			return null;
		}

		public Dictionary<string, object> GetDataCleverTap()
		{
			return null;
		}

		public override string ToString()
		{
			return null;
		}
	}
}
