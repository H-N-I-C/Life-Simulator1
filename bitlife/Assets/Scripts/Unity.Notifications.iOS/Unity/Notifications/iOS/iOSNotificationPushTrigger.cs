using System.Runtime.InteropServices;

namespace Unity.Notifications.iOS
{
	[StructLayout((LayoutKind)0, Size = 1)]
	public struct iOSNotificationPushTrigger : iOSNotificationTrigger
	{
		public iOSNotificationTriggerType Type => iOSNotificationTriggerType.TimeInterval;
	}
}
