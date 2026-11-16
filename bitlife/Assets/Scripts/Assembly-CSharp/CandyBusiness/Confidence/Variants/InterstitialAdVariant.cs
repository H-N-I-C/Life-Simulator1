using CandyBusiness.Confidence.Flags;
using CandyBusiness.Confidence.Properties;

namespace CandyBusiness.Confidence.Variants
{
	public class InterstitialAdVariant : EnabledVariant
	{
		public class ConfigVariant : ConfidenceVariant
		{
			public static class Defaults
			{
				public const int TimeBetweenSeconds = 300;

				public const int TimeToFirstInSessionSeconds = 300;

				public const int TimeToFirstSeconds = 300;
			}

			private readonly ConfidenceProperty<int> TimeBetweenSeconds__BackingField;

			private readonly ConfidenceProperty<int> TimeToFirstInSessionSeconds__BackingField;

			private readonly ConfidenceProperty<int> TimeToFirstSeconds__BackingField;

			public override ConfidenceFlag Flag => null;

			public ConfidenceProperty<int> TimeBetweenSeconds => TimeBetweenSeconds__BackingField;

			public ConfidenceProperty<int> TimeToFirstInSessionSeconds => TimeToFirstInSessionSeconds__BackingField;

			public ConfidenceProperty<int> TimeToFirstSeconds => TimeToFirstSeconds__BackingField;
		}

		public ConfigVariant Config;

		public InterstitialAdVariant()
			: base(null)
		{
		}

		public override void Update()
		{
		}
	}
}
