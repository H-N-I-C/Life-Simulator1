using System.Collections.Generic;

namespace Unity.Notifications.iOS
{
	public class iOSNotification
	{
		private float? SoundVolume__BackingField;

		private List<iOSNotificationAttachment> Attachments__BackingField;

		private iOSNotificationData data;

		private Dictionary<string, string> userInfo;

		public string Identifier
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public string CategoryIdentifier
		{
			set
			{
			}
		}

		public string ThreadIdentifier
		{
			set
			{
			}
		}

		public string Title
		{
			set
			{
			}
		}

		public string Body
		{
			set
			{
			}
		}

		public bool ShowInForeground
		{
			set
			{
			}
		}

		public PresentationOption ForegroundPresentationOption
		{
			set
			{
			}
		}

		public float? SoundVolume => SoundVolume__BackingField;

		public NotificationInterruptionLevel InterruptionLevel
		{
			set
			{
			}
		}

		public double RelevanceScore
		{
			set
			{
			}
		}

		public string Data
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public List<iOSNotificationAttachment> Attachments
		{
			get
			{
				return Attachments__BackingField;
			}
			set
			{
				Attachments__BackingField = value;
			}
		}

		public iOSNotificationTrigger Trigger
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		private static string GenerateUniqueID()
		{
			return null;
		}

		public iOSNotification()
		{
		}

		public iOSNotification(string identifier)
		{
		}

		internal iOSNotification(iOSNotificationWithUserInfo data)
		{
		}

		internal iOSNotificationWithUserInfo GetDataForSending()
		{
			return default;
		}
	}
}
