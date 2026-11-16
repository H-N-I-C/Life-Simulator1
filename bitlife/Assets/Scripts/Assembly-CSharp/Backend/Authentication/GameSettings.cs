using System.Collections.Generic;

namespace Backend.Authentication
{
	public class GameSettings
	{
		private sealed class _003C_003Ec__DisplayClass73_0
		{
			public IAP_TYPE iapType;

			internal bool _003CGetDiscountPriceForWebshopItem_003Eb__0(SalesModuleConfig t)
			{
				return false;
			}
		}

		private bool VampireShowEntryPoint__BackingField;

		private string DisableReceiptValidation__BackingField;

		private bool IsAlternateIconsEnabled__BackingField;

		private bool EnableScrollBlockeriOS__BackingField;

		private bool EnableScrollBlockerAndroid__BackingField;

		private TranslationInitModel TranslationInitModel__BackingField;

		private string QrCodeFeatureUrl__BackingField;

		private bool ShowDeleteButton__BackingField;

		private bool IsTelemetryEnabled__BackingField;

		private bool UploadGraves__BackingField;

		private List<CompatibleVersionItem> CompatibleVersionItems__BackingField;

		private SerializedPushScenarioConfig PushScenarioData__BackingField;

		private List<SalesModuleConfig> SalesModuleConfig__BackingField;

		private bool LocalNotificationV1Enabled__BackingField;

		private string LocalNotificationV1DeviceTimeNotificationTime1__BackingField;

		private string LocalNotificationV1DeviceTimeNotificationTime2__BackingField;

		private bool MovieDirectorTeaserSwitch__BackingField;

		private int MovieDirectorTeaserMaxView__BackingField;

		public bool VampireShowEntryPoint
		{
			get
			{
				return VampireShowEntryPoint__BackingField;
			}
			set
			{
				VampireShowEntryPoint__BackingField = value;
			}
		}

		public string DisableReceiptValidation
		{
			get
			{
				return DisableReceiptValidation__BackingField;
			}
			set
			{
				DisableReceiptValidation__BackingField = value;
			}
		}

		public bool IsAlternateIconsEnabled
		{
			get
			{
				return IsAlternateIconsEnabled__BackingField;
			}
			set
			{
				IsAlternateIconsEnabled__BackingField = value;
			}
		}

		public bool EnableScrollBlockeriOS
		{
			get
			{
				return EnableScrollBlockeriOS__BackingField;
			}
			set
			{
				EnableScrollBlockeriOS__BackingField = value;
			}
		}

		public bool EnableScrollBlockerAndroid
		{
			get
			{
				return EnableScrollBlockerAndroid__BackingField;
			}
			set
			{
				EnableScrollBlockerAndroid__BackingField = value;
			}
		}

		public TranslationInitModel TranslationInitModel
		{
			get
			{
				return TranslationInitModel__BackingField;
			}
			set
			{
				TranslationInitModel__BackingField = value;
			}
		}

		public string QrCodeFeatureUrl
		{
			get
			{
				return QrCodeFeatureUrl__BackingField;
			}
			set
			{
				QrCodeFeatureUrl__BackingField = value;
			}
		}

		public bool ShowDeleteButton
		{
			get
			{
				return ShowDeleteButton__BackingField;
			}
			set
			{
				ShowDeleteButton__BackingField = value;
			}
		}

		public bool IsTelemetryEnabled
		{
			get
			{
				return IsTelemetryEnabled__BackingField;
			}
			set
			{
				IsTelemetryEnabled__BackingField = value;
			}
		}

		public bool UploadGraves
		{
			get
			{
				return UploadGraves__BackingField;
			}
			set
			{
				UploadGraves__BackingField = value;
			}
		}

		public List<CompatibleVersionItem> CompatibleVersionItems
		{
			get
			{
				return CompatibleVersionItems__BackingField;
			}
			set
			{
				CompatibleVersionItems__BackingField = value;
			}
		}

		public SerializedPushScenarioConfig PushScenarioData
		{
			get
			{
				return PushScenarioData__BackingField;
			}
			set
			{
				PushScenarioData__BackingField = value;
			}
		}

		public List<SalesModuleConfig> SalesModuleConfig
		{
			get
			{
				return SalesModuleConfig__BackingField;
			}
			set
			{
				SalesModuleConfig__BackingField = value;
			}
		}

		public bool LocalNotificationV1Enabled
		{
			get
			{
				return LocalNotificationV1Enabled__BackingField;
			}
			set
			{
				LocalNotificationV1Enabled__BackingField = value;
			}
		}

		public string LocalNotificationV1DeviceTimeNotificationTime1
		{
			get
			{
				return LocalNotificationV1DeviceTimeNotificationTime1__BackingField;
			}
			set
			{
				LocalNotificationV1DeviceTimeNotificationTime1__BackingField = value;
			}
		}

		public string LocalNotificationV1DeviceTimeNotificationTime2
		{
			get
			{
				return LocalNotificationV1DeviceTimeNotificationTime2__BackingField;
			}
			set
			{
				LocalNotificationV1DeviceTimeNotificationTime2__BackingField = value;
			}
		}

		public bool MovieDirectorTeaserSwitch
		{
			get
			{
				return MovieDirectorTeaserSwitch__BackingField;
			}
			set
			{
				MovieDirectorTeaserSwitch__BackingField = value;
			}
		}

		public int MovieDirectorTeaserMaxView
		{
			get
			{
				return MovieDirectorTeaserMaxView__BackingField;
			}
			set
			{
				MovieDirectorTeaserMaxView__BackingField = value;
			}
		}

		public bool IsScrollBlockerEnabled()
		{
			return false;
		}

		public float GetDiscountPriceForWebshopItem(IAP_TYPE iapType)
		{
			return 0f;
		}
	}
}
