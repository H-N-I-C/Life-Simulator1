using System;

namespace Unity.Notifications.iOS
{
	public class iOSNotificationCenter
	{
		public delegate void NotificationReceivedCallback(iOSNotification notification);

		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			internal void _003C_002Ecctor_003Eb__39_0(iOSNotification _003Cp0_003E)
			{
			}

			internal void _003C_002Ecctor_003Eb__39_1(iOSNotification _003Cp0_003E)
			{
			}
		}

		private static bool s_Initialized;

		private static NotificationReceivedCallback s_OnNotificationReceived;

		private static NotificationReceivedCallback s_OnRemoteNotificationReceived;

		public static int ApplicationBadge
		{
			set
			{
			}
		}

		private static bool Initialize()
		{
			return false;
		}

		public static void ScheduleNotification(iOSNotification notification)
		{
		}

		public static iOSNotification GetLastRespondedNotification()
		{
			return null;
		}

		public static void RemoveAllScheduledNotifications()
		{
		}

		public static void RemoveAllDeliveredNotifications()
		{
		}

		public static iOSNotificationSettings GetNotificationSettings()
		{
			return default;
		}

		internal static void OnReceivedRemoteNotification(iOSNotificationWithUserInfo data)
		{
		}

		internal static void OnSentNotification(iOSNotificationWithUserInfo data)
		{
		}
	}
}
