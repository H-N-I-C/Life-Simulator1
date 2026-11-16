namespace Backend.Authentication
{
	public class WebShopSettingsItem
	{
		private bool IsUSFlow__BackingField;

		private APP_PLATFORM PlatformType__BackingField;

		private bool IsEnabled__BackingField;

		private bool BannerEnabled__BackingField;

		private int MinLivesCompleted__BackingField;

		private bool IsEnabledStore__BackingField;

		private bool EnforceRulesForWebShop__BackingField;

		private bool EnforcePurchaseRulesForWebShop__BackingField;

		public bool IsUSFlow
		{
			get
			{
				return IsUSFlow__BackingField;
			}
			set
			{
				IsUSFlow__BackingField = value;
			}
		}

		public APP_PLATFORM PlatformType
		{
			get
			{
				return PlatformType__BackingField;
			}
			set
			{
				PlatformType__BackingField = value;
			}
		}

		public bool IsEnabled
		{
			get
			{
				return IsEnabled__BackingField;
			}
			set
			{
				IsEnabled__BackingField = value;
			}
		}

		public bool BannerEnabled
		{
			get
			{
				return BannerEnabled__BackingField;
			}
			set
			{
				BannerEnabled__BackingField = value;
			}
		}

		public int MinLivesCompleted
		{
			get
			{
				return MinLivesCompleted__BackingField;
			}
			set
			{
				MinLivesCompleted__BackingField = value;
			}
		}

		public bool IsEnabledStore
		{
			get
			{
				return IsEnabledStore__BackingField;
			}
			set
			{
				IsEnabledStore__BackingField = value;
			}
		}

		public bool EnforceRulesForWebShop
		{
			get
			{
				return EnforceRulesForWebShop__BackingField;
			}
			set
			{
				EnforceRulesForWebShop__BackingField = value;
			}
		}

		public bool EnforcePurchaseRulesForWebShop
		{
			get
			{
				return EnforcePurchaseRulesForWebShop__BackingField;
			}
			set
			{
				EnforcePurchaseRulesForWebShop__BackingField = value;
			}
		}
	}
}
