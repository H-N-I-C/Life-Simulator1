using System.Collections.Generic;
using CandyBusiness.Confidence.Attributes;

namespace CandyBusiness.Confidence.Flags
{
	public static class ConfidenceFlags
	{
		public static readonly ConfidenceFlag BitizenshipAdWording;

		public static readonly ConfidenceFlag AdInterstitialMasterControl;

		public static readonly ConfidenceFlag AdInterstitialConfig;

		public static readonly ConfidenceFlag AdImmersiveMasterControl;

		public static readonly ConfidenceFlag AdImmersiveConfig;

		public static readonly ConfidenceFlag AdImmersiveListData;

		public static readonly ConfidenceFlag WebShopUx;

		public static readonly ConfidenceFlag MarketplaceExperience;

		public static readonly ConfidenceFlag RewardExperience;

		public static readonly ConfidenceFlag DailyRewardConfiguration;

		public static readonly ConfidenceFlag HomeScreen;

		public static readonly ConfidenceFlag ScavengerHunt;

		public static readonly ConfidenceFlag ModernScenarioAgeUpMasterControl;

		public static readonly ConfidenceFlag ModernScenarioAgeUpConfig;

		public static readonly ConfidenceFlag TranslationMasterControl;

		public static readonly ConfidenceFlag DevTriggerPromotion;

		public static readonly ConfidenceFlag MovieDirectorMasterControl;

		public static readonly ConfidenceFlag MovieDirectorTeaserEnabled;

		public static readonly ConfidenceFlag MovieDirectorTeaserMaxView;

		public static readonly ConfidenceFlag BirthdayInviteButton;

		public static readonly ConfidenceFlag BirthdayInviteCopy;

		public static readonly ConfidenceFlag BossModeCopyWording;

		public static readonly ConfidenceFlag DevIjTestFlag;

		private static readonly IReadOnlyList<ConfidenceFlag> All__BackingField;

		private static readonly HashSet<ConfidenceFlag> Registered;

		private static readonly ArchivedAttribute UnregisteredArchive;

		private static readonly IReadOnlyDictionary<ConfidenceFlag, ArchivedAttribute> Archived__BackingField;

		public static IReadOnlyList<ConfidenceFlag> All => All__BackingField;

		public static IReadOnlyDictionary<ConfidenceFlag, ArchivedAttribute> Archived => Archived__BackingField;

		public static bool IsArchived(ConfidenceFlag flag)
		{
			return false;
		}

		public static bool TryGetArchived(ConfidenceFlag flag, out ArchivedAttribute attribute)
		{
			attribute = null;
			return false;
		}
	}
}
