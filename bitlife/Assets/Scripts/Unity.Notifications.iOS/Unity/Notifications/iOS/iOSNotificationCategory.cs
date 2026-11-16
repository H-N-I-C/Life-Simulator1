using System.Collections.Generic;

namespace Unity.Notifications.iOS
{
	public class iOSNotificationCategory
	{
		private List<iOSNotificationAction> m_Actions;

		private List<string> m_IntentIdentifiers;

		private string Id__BackingField;

		private string HiddenPreviewsBodyPlaceholder__BackingField;

		private string SummaryFormat__BackingField;

		private iOSNotificationCategoryOptions Options__BackingField;

		public string Id => Id__BackingField;

		public iOSNotificationAction[] Actions => null;

		public string[] IntentIdentifiers => null;

		public string HiddenPreviewsBodyPlaceholder => HiddenPreviewsBodyPlaceholder__BackingField;

		public string SummaryFormat => SummaryFormat__BackingField;

		public iOSNotificationCategoryOptions Options => Options__BackingField;
	}
}
