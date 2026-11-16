using CandyBusiness.Confidence.Flags;
using CandyBusiness.Confidence.Properties;

namespace CandyBusiness.Confidence.Variants
{
	public class BirthdayInviteCopyControlVariant : ConfidenceVariant
	{
		private const string MessageSharePropertyName = "copy";

		private const string EnabledPropertyName = "enabled";

		private readonly ConfidenceProperty<bool> Enabled__BackingField;

		private readonly ConfidenceProperty<string> Message__BackingField;

		public override ConfidenceFlag Flag => null;

		public ConfidenceProperty<bool> Enabled => Enabled__BackingField;

		public ConfidenceProperty<string> Message => Message__BackingField;
	}
}
