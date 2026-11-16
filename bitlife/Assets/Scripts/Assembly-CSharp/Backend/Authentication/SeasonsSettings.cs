using System.Collections.Generic;

namespace Backend.Authentication
{
	public class SeasonsSettings
	{
		private string DateStartNextSeason__BackingField;

		private bool ShowWelcomeScenario__BackingField;

		private SeasonsSettingsWelcome SeasonsSettingsWelcome__BackingField;

		private string SeasonAssetsBaseUrl__BackingField;

		private List<int> ShopPriceLives__BackingField;

		private List<int> ShopPriceIap__BackingField;

		private bool HideStreaksUI__BackingField;

		private int AssetVersion__BackingField;

		private SeasonsBarMessages SeasonsMessages__BackingField;

		private bool StreakIsActive__BackingField;

		private bool SeasonIsActive__BackingField;

		private bool DailyRewardsIsActive__BackingField;

		private int RefreshDataCoolDown__BackingField;

		private int ReloadDataCoolDown__BackingField;

		private int RolloutiOS__BackingField;

		private int RolloutAndroid__BackingField;

		private bool ContributeToCommunityGoalEnabled__BackingField;

		private List<string> ListValidSeasonsId__BackingField;

		private string ActiveSeasonId__BackingField;

		private string NextSeasonId__BackingField;

		private string NextSeasonMessage__BackingField;

		private int CoolDownMessages__BackingField;

		private string NextSeasonShortName__BackingField;

		private string NextSeasonName__BackingField;

		private string SeasonName__BackingField;

		private string SeasonShortName__BackingField;

		public string DateStartNextSeason
		{
			get
			{
				return DateStartNextSeason__BackingField;
			}
			set
			{
				DateStartNextSeason__BackingField = value;
			}
		}

		public bool ShowWelcomeScenario
		{
			get
			{
				return ShowWelcomeScenario__BackingField;
			}
			set
			{
				ShowWelcomeScenario__BackingField = value;
			}
		}

		public SeasonsSettingsWelcome SeasonsSettingsWelcome
		{
			get
			{
				return SeasonsSettingsWelcome__BackingField;
			}
			set
			{
				SeasonsSettingsWelcome__BackingField = value;
			}
		}

		public string SeasonAssetsBaseUrl
		{
			get
			{
				return SeasonAssetsBaseUrl__BackingField;
			}
			set
			{
				SeasonAssetsBaseUrl__BackingField = value;
			}
		}

		public List<int> ShopPriceLives
		{
			get
			{
				return ShopPriceLives__BackingField;
			}
			set
			{
				ShopPriceLives__BackingField = value;
			}
		}

		public List<int> ShopPriceIap
		{
			get
			{
				return ShopPriceIap__BackingField;
			}
			set
			{
				ShopPriceIap__BackingField = value;
			}
		}

		public bool HideStreaksUI
		{
			get
			{
				return HideStreaksUI__BackingField;
			}
			set
			{
				HideStreaksUI__BackingField = value;
			}
		}

		public int AssetVersion
		{
			get
			{
				return AssetVersion__BackingField;
			}
			set
			{
				AssetVersion__BackingField = value;
			}
		}

		public SeasonsBarMessages SeasonsMessages
		{
			get
			{
				return SeasonsMessages__BackingField;
			}
			set
			{
				SeasonsMessages__BackingField = value;
			}
		}

		public bool StreakIsActive
		{
			get
			{
				return StreakIsActive__BackingField;
			}
			set
			{
				StreakIsActive__BackingField = value;
			}
		}

		public bool SeasonIsActive
		{
			get
			{
				return SeasonIsActive__BackingField;
			}
			set
			{
				SeasonIsActive__BackingField = value;
			}
		}

		public bool DailyRewardsIsActive
		{
			get
			{
				return DailyRewardsIsActive__BackingField;
			}
			set
			{
				DailyRewardsIsActive__BackingField = value;
			}
		}

		public int RefreshDataCoolDown
		{
			get
			{
				return RefreshDataCoolDown__BackingField;
			}
			set
			{
				RefreshDataCoolDown__BackingField = value;
			}
		}

		public int ReloadDataCoolDown
		{
			get
			{
				return ReloadDataCoolDown__BackingField;
			}
			set
			{
				ReloadDataCoolDown__BackingField = value;
			}
		}

		public int RolloutiOS
		{
			get
			{
				return RolloutiOS__BackingField;
			}
			set
			{
				RolloutiOS__BackingField = value;
			}
		}

		public int RolloutAndroid
		{
			get
			{
				return RolloutAndroid__BackingField;
			}
			set
			{
				RolloutAndroid__BackingField = value;
			}
		}

		public bool ContributeToCommunityGoalEnabled
		{
			get
			{
				return ContributeToCommunityGoalEnabled__BackingField;
			}
			set
			{
				ContributeToCommunityGoalEnabled__BackingField = value;
			}
		}

		public List<string> ListValidSeasonsId
		{
			get
			{
				return ListValidSeasonsId__BackingField;
			}
			set
			{
				ListValidSeasonsId__BackingField = value;
			}
		}

		public string ActiveSeasonId
		{
			get
			{
				return ActiveSeasonId__BackingField;
			}
			set
			{
				ActiveSeasonId__BackingField = value;
			}
		}

		public string NextSeasonId
		{
			get
			{
				return NextSeasonId__BackingField;
			}
			set
			{
				NextSeasonId__BackingField = value;
			}
		}

		public string NextSeasonMessage
		{
			get
			{
				return NextSeasonMessage__BackingField;
			}
			set
			{
				NextSeasonMessage__BackingField = value;
			}
		}

		public int CoolDownMessages
		{
			get
			{
				return CoolDownMessages__BackingField;
			}
			set
			{
				CoolDownMessages__BackingField = value;
			}
		}

		public string NextSeasonShortName
		{
			get
			{
				return NextSeasonShortName__BackingField;
			}
			set
			{
				NextSeasonShortName__BackingField = value;
			}
		}

		public string NextSeasonName
		{
			get
			{
				return NextSeasonName__BackingField;
			}
			set
			{
				NextSeasonName__BackingField = value;
			}
		}

		public string SeasonName
		{
			get
			{
				return SeasonName__BackingField;
			}
			set
			{
				SeasonName__BackingField = value;
			}
		}

		public string SeasonShortName
		{
			get
			{
				return SeasonShortName__BackingField;
			}
			set
			{
				SeasonShortName__BackingField = value;
			}
		}

		private bool RolloutActive()
		{
			return false;
		}

		public bool IsSeasonActive()
		{
			return false;
		}
	}
}
