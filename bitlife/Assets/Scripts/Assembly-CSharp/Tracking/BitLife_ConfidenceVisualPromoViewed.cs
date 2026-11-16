using System.Collections.Generic;

namespace Tracking
{
	public sealed class BitLife_ConfidenceVisualPromoViewed : IGameTelemetryEvent
	{
		private readonly ConfidenceVisualPromoTelemetryContext _context;

		public BitLife_ConfidenceVisualPromoViewed(ConfidenceVisualPromoTelemetryContext context)
		{
		}

		public Dictionary<string, string> GetData()
		{
			return null;
		}
	}
}
