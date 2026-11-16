using CandyBusiness.Confidence.Flags;
using CandyBusiness.Confidence.Properties;

namespace CandyBusiness.Confidence.Variants
{
	public sealed class RewardExperienceVariant : ConfidenceVariant
	{
		public const string PropertyKey = "seasons-system-selection.systemSelected";

		public const string Streaks = "streaks";

		public const string DailyRewards = "dailyreward";

		private readonly ConfidenceProperty<string> SystemSelected__BackingField;

		public override ConfidenceFlag Flag => null;

		public ConfidenceProperty<string> SystemSelected => SystemSelected__BackingField;

		public static bool IsValid(string mode)
		{
			return false;
		}

		public static bool AllowsStreaks(string mode, bool killSwitchEnabled)
		{
			return false;
		}

		public static bool AllowsDailyRewards(string mode, bool killSwitchEnabled)
		{
			return false;
		}
	}
}
