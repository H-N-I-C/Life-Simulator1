using CandyBusiness.Confidence.Flags;
using CandyBusiness.Confidence.Properties;

namespace CandyBusiness.Confidence.Variants
{
	public sealed class ImmersiveAdListDataVariant : ConfidenceVariant
	{
		private readonly ConfidenceProperty<string> AndroidAdMob__BackingField;

		private readonly ConfidenceProperty<string> IOSAdMob__BackingField;

		public override ConfidenceFlag Flag => null;

		public ConfidenceProperty<string> AndroidAdMob => AndroidAdMob__BackingField;

		public ConfidenceProperty<string> IOSAdMob => IOSAdMob__BackingField;
	}
}
