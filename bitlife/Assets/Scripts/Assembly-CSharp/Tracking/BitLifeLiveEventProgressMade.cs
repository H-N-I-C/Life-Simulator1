using System.Collections.Generic;

namespace Tracking
{
	public class BitLifeLiveEventProgressMade : IGameTelemetryEvent
	{
		private string EventType__BackingField;

		private string EventName__BackingField;

		private string Objective__BackingField;

		private int NumObjectivesComplete__BackingField;

		private int NumObjectivesTotal__BackingField;

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

		public string Objective
		{
			get
			{
				return Objective__BackingField;
			}
			set
			{
				Objective__BackingField = value;
			}
		}

		public int NumObjectivesComplete
		{
			get
			{
				return NumObjectivesComplete__BackingField;
			}
			set
			{
				NumObjectivesComplete__BackingField = value;
			}
		}

		public int NumObjectivesTotal
		{
			get
			{
				return NumObjectivesTotal__BackingField;
			}
			set
			{
				NumObjectivesTotal__BackingField = value;
			}
		}

		public Dictionary<string, string> GetData()
		{
			return null;
		}

		public static void SendLiveEventProgress(HolidayCollectibleMetadata metadata)
		{
		}

		private static void SendScavengerHuntProgress<T>(HolidayCollectibleMetadata metadata) where T : ScavengerHunt, new()
		{
		}

		private void DebugPrint()
		{
		}
	}
}
