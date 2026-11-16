using System.Collections.Generic;

namespace Tracking
{
	public class EventTracking
	{
		private static List<TrackingEvent> _ignoreList;

		public static bool TrackFunnelEventStart(TrackingEvent evt, string level)
		{
			return false;
		}

		public static bool TrackFunnelEventComplete(TrackingEvent evt, string level)
		{
			return false;
		}

		public static void TrackFunnelEventFail(TrackingEvent evt, string level)
		{
		}
	}
}
