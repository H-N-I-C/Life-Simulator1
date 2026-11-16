namespace Unity.Notifications.iOS
{
	public struct iOSNotificationLocationTrigger : iOSNotificationTrigger
	{
		private double Latitude__BackingField;

		private double Longitude__BackingField;

		private float Radius__BackingField;

		private bool NotifyOnEntry__BackingField;

		private bool NotifyOnExit__BackingField;

		private bool Repeats__BackingField;

		public iOSNotificationTriggerType Type => iOSNotificationTriggerType.TimeInterval;

		public double Latitude
		{
			get
			{
				return Latitude__BackingField;
			}
			set
			{
				Latitude__BackingField = value;
			}
		}

		public double Longitude
		{
			get
			{
				return Longitude__BackingField;
			}
			set
			{
				Longitude__BackingField = value;
			}
		}

		public float Radius
		{
			get
			{
				return Radius__BackingField;
			}
			set
			{
				Radius__BackingField = value;
			}
		}

		public bool NotifyOnEntry
		{
			get
			{
				return NotifyOnEntry__BackingField;
			}
			set
			{
				NotifyOnEntry__BackingField = value;
			}
		}

		public bool NotifyOnExit
		{
			get
			{
				return NotifyOnExit__BackingField;
			}
			set
			{
				NotifyOnExit__BackingField = value;
			}
		}

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
