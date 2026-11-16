using CandyBusiness.Confidence.Flags;
using CandyBusiness.Confidence.Properties;

namespace CandyBusiness.Confidence.Variants
{
	public class IjDevTestVariant : ConfidenceVariant
	{
		private readonly ConfidenceFlag Flag__BackingField;

		private readonly ConfidenceProperty<string> UserMessage__BackingField;

		private readonly ConfidenceProperty<string> ServedAsset__BackingField;

		private const string UserMessageProperty = "user-message";

		private const string ServedAssetProperty = "served-asset";

		private static bool AutoShowServedAssetEnabled => false;

		public override ConfidenceFlag Flag => Flag__BackingField;

		public ConfidenceProperty<string> UserMessage => UserMessage__BackingField;

		public ConfidenceProperty<string> ServedAsset => ServedAsset__BackingField;

		public void PrintState()
		{
		}

		private void AutoShowServedAsset()
		{
		}

		public void ShowServedAsset()
		{
		}
	}
}
