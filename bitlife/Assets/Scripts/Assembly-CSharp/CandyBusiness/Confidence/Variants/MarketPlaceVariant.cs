using CandyBusiness.Confidence.Flags;
using CandyBusiness.Confidence.Properties;

namespace CandyBusiness.Confidence.Variants
{
	public class MarketPlaceVariant : ConfidenceVariant
	{
		public static class Defaults
		{
			public const string MarketPlaceId = "MKT_DEFAULT";
		}

		private readonly ConfidenceProperty<string> MarketPlaceId__BackingField;

		public override ConfidenceFlag Flag => null;

		public ConfidenceProperty<string> MarketPlaceId => MarketPlaceId__BackingField;

		public bool TryGetMarketPlaceId(out string id)
		{
			id = null;
			return false;
		}
	}
}
