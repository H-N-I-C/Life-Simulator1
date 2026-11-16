using CandyBusiness.Confidence.Flags;
using CandyBusiness.Confidence.Properties;
using CandyBusiness.NativeAds;

namespace CandyBusiness.Confidence.Variants
{
	public class ImmersiveAdVariant : EnabledVariant
	{
		public class ConfigVariant : ConfidenceVariant
		{
			public static class Defaults
			{
				public const bool RetryControl = true;

				public const string ProviderAndroid = "google";

				public const string ProviderIOS = "google";

				public const string AdStyle = "rectangle";

				public const int ShowFirstAtPosition = 4;

				public const int ShowEveryNTiles = 12;

				public const int RowHeight = 400;

				public const int MinListRequiredToAppear = 4;

				public const int TimeToFirstSeconds = 300;

				public const int CooldownSeconds = 0;

				public const int ProbabilityToShowInList = 100;
			}

			private readonly ConfidenceProperty<bool> RetryControl__BackingField;

			private readonly ConfidenceProperty<string> ProviderAndroid__BackingField;

			private readonly ConfidenceProperty<string> ProviderIOS__BackingField;

			private readonly ConfidenceProperty<string> AdStyle__BackingField;

			private readonly ConfidenceProperty<int> ShowFirstAtPosition__BackingField;

			private readonly ConfidenceProperty<int> ShowEveryNTiles__BackingField;

			private readonly ConfidenceProperty<int> RowHeight__BackingField;

			private readonly ConfidenceProperty<int> MinListRequiredToAppear__BackingField;

			private readonly ConfidenceProperty<int> TimeToFirstSeconds__BackingField;

			private readonly ConfidenceProperty<int> CooldownSeconds__BackingField;

			private readonly ConfidenceProperty<int> ProbabilityToShowInList__BackingField;

			public override ConfidenceFlag Flag => null;

			public ConfidenceProperty<bool> RetryControl => RetryControl__BackingField;

			public ConfidenceProperty<string> ProviderAndroid => ProviderAndroid__BackingField;

			public ConfidenceProperty<string> ProviderIOS => ProviderIOS__BackingField;

			public ConfidenceProperty<string> AdStyle => AdStyle__BackingField;

			public ConfidenceProperty<int> ShowFirstAtPosition => ShowFirstAtPosition__BackingField;

			public ConfidenceProperty<int> ShowEveryNTiles => ShowEveryNTiles__BackingField;

			public ConfidenceProperty<int> RowHeight => RowHeight__BackingField;

			public ConfidenceProperty<int> MinListRequiredToAppear => MinListRequiredToAppear__BackingField;

			public ConfidenceProperty<int> TimeToFirstSeconds => TimeToFirstSeconds__BackingField;

			public ConfidenceProperty<int> CooldownSeconds => CooldownSeconds__BackingField;

			public ConfidenceProperty<int> ProbabilityToShowInList => ProbabilityToShowInList__BackingField;

			public NativeAdsProvider NativeAdsProvider => NativeAdsProvider.GoogleImmersive;

			private NativeAdsProvider GetNativeAdsProvider()
			{
				return NativeAdsProvider.GoogleImmersive;
			}
		}

		public ConfigVariant Config;

		public ImmersiveAdVariant()
			: base(null)
		{
		}

		public override void Update()
		{
		}
	}
}
