using System;

namespace Unity.Notifications.iOS
{
	public class iOSNotificationAction
	{
		internal iOSNotificationActionIconType _imageType;

		internal string _image;

		private string Id__BackingField;

		private string Title__BackingField;

		private iOSNotificationActionOptions Options__BackingField;

		public string Id => Id__BackingField;

		public string Title => Title__BackingField;

		public iOSNotificationActionOptions Options => Options__BackingField;

		internal virtual IntPtr CreateUNNotificationAction()
		{
			return (IntPtr)0;
		}
	}
}
