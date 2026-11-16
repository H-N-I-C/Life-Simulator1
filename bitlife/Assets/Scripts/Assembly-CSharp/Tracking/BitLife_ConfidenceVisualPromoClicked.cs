using System.Collections.Generic;

namespace Tracking
{
	public sealed class BitLife_ConfidenceVisualPromoClicked : IGameTelemetryEvent
	{
		private readonly string _promoID;

		private readonly ConfidenceVisualPromoClickedOn _clickedOn;

		public BitLife_ConfidenceVisualPromoClicked(string promoID, ConfidenceVisualPromoClickedOn clickedOn)
		{
		}

		public Dictionary<string, string> GetData()
		{
			return null;
		}
	}
}
