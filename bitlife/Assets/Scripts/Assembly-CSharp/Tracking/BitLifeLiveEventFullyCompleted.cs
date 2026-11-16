using System.Collections.Generic;

namespace Tracking
{
	public class BitLifeLiveEventFullyCompleted : IGameTelemetryEvent
	{
		private string EventType__BackingField;

		private string EventName__BackingField;

		public string EventType
		{
			get
			{
				return EventType__BackingField;
			}
			set
			{
				EventType__BackingField = value;
			}
		}

		public string EventName
		{
			get
			{
				return EventName__BackingField;
			}
			set
			{
				EventName__BackingField = value;
			}
		}

		public BitLifeLiveEventFullyCompleted(LiveEventType eventType, ScavengerItem item)
		{
		}

		public Dictionary<string, string> GetData()
		{
			return null;
		}
	}
}
