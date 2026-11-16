using CandyBusiness.Confidence.Flags;
using CandyBusiness.Confidence.Properties;

namespace CandyBusiness.Confidence.Variants
{
	public sealed class BitizenshipAdWordingVariant : ConfidenceVariant
	{
		private readonly ConfidenceProperty<bool> Enabled__BackingField;

		public override ConfidenceFlag Flag => null;

		public ConfidenceProperty<bool> Enabled => Enabled__BackingField;

		public bool UseForcedAds => false;
	}
}
