using CandyBusiness.Confidence.Flags;
using CandyBusiness.Confidence.Properties;

namespace CandyBusiness.Confidence.Variants
{
	public class ScavengerHuntVariant : ConfidenceVariant
	{
		public static class Defaults
		{
			public const string ScavengerHuntId = "NONE";

			public const bool IsActive = false;
		}

		private readonly ConfidenceProperty<string> ScavengerHuntId__BackingField;

		private readonly ConfidenceProperty<bool> IsActive__BackingField;

		public override ConfidenceFlag Flag => null;

		public ConfidenceProperty<string> ScavengerHuntId => ScavengerHuntId__BackingField;

		public ConfidenceProperty<bool> IsActive => IsActive__BackingField;
	}
}
