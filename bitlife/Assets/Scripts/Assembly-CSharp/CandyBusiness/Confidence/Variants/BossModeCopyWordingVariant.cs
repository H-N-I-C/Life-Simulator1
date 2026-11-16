using CandyBusiness.Confidence.Flags;
using CandyBusiness.Confidence.Properties;

namespace CandyBusiness.Confidence.Variants
{
	public class BossModeCopyWordingVariant : ConfidenceVariant
	{
		private const string EnabledPropertyName = "enabled";

		private readonly ConfidenceProperty<bool> Enabled__BackingField;

		public override ConfidenceFlag Flag => null;

		public ConfidenceProperty<bool> Enabled => Enabled__BackingField;
	}
}
