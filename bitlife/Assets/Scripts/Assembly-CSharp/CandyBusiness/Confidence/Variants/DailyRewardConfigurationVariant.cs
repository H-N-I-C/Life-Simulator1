using CandyBusiness.Confidence.Flags;
using CandyBusiness.Confidence.Properties;

namespace CandyBusiness.Confidence.Variants
{
	public sealed class DailyRewardConfigurationVariant : ConfidenceVariant
	{
		public const string PropertyKey = "daily-reward-configuration.config_name";

		private readonly ConfidenceProperty<string> ConfigurationName__BackingField;

		public override ConfidenceFlag Flag => null;

		public ConfidenceProperty<string> ConfigurationName => ConfigurationName__BackingField;
	}
}
