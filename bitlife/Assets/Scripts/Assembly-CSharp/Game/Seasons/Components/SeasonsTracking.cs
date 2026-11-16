using System.Collections.Generic;
using Tracking;

namespace Game.Seasons.Components
{
	public class SeasonsTracking
	{
		private static Dictionary<BitLifeEvents, GameTelemetryEvent> _events;

		private static Dictionary<BitLifeEvents, int> _allowedMultipleEvents;

		public static void TrackEvent(BitLifeEvents eventName)
		{
		}
	}
}
