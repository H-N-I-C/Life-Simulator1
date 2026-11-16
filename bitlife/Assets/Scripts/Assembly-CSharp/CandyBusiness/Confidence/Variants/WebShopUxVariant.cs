using CandyBusiness.Confidence.Flags;
using CandyBusiness.Confidence.Properties;

namespace CandyBusiness.Confidence.Variants
{
	public class WebShopUxVariant : ConfidenceVariant
	{
		public static class Defaults
		{
			public const PlayerExperienceType PlayerExperience = PlayerExperienceType.DIRECT_TABS_ONLY_LAND_OFFICIAL;
		}

		public enum PlayerExperienceType
		{
			ORIGINAL = 0,
			DIRECT_TABS_BANNER_LAND_OFFICIAL = 1,
			DIRECT_TABS_ONLY_LAND_OFFICIAL = 2,
			DIRECT_TABS_BANNER_LAND_PLATFORM = 3,
			DIRECT_TABS_ONLY_LAND_PLATFORM = 4
		}

		private readonly ConfidenceProperty<PlayerExperienceType> WebShopType__BackingField;

		public override ConfidenceFlag Flag => null;

		public ConfidenceProperty<PlayerExperienceType> WebShopType => WebShopType__BackingField;
	}
}
