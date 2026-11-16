namespace Unity.Notifications.iOS
{
	public struct iOSNotificationTimeIntervalTrigger : iOSNotificationTrigger
	{
		internal int timeInterval;

		private bool Repeats__BackingField;

		public iOSNotificationTriggerType Type => iOSNotificationTriggerType.TimeInterval;

		public bool Repeats
		{
			get
			{
				return Repeats__BackingField;
			}
			set
			{
				Repeats__BackingField = value;
			}
		}
	}
}
