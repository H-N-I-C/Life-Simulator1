namespace Unity.Notifications.iOS
{
	public struct iOSNotificationCalendarTrigger : iOSNotificationTrigger
	{
		private int? Year__BackingField;

		private int? Month__BackingField;

		private int? Day__BackingField;

		private int? Hour__BackingField;

		private int? Minute__BackingField;

		private int? Second__BackingField;

		private bool UtcTime__BackingField;

		private bool Repeats__BackingField;

		public iOSNotificationTriggerType Type => iOSNotificationTriggerType.TimeInterval;

		public int? Year
		{
			get
			{
				return Year__BackingField;
			}
			set
			{
				Year__BackingField = value;
			}
		}

		public int? Month
		{
			get
			{
				return Month__BackingField;
			}
			set
			{
				Month__BackingField = value;
			}
		}

		public int? Day
		{
			get
			{
				return Day__BackingField;
			}
			set
			{
				Day__BackingField = value;
			}
		}

		public int? Hour
		{
			get
			{
				return Hour__BackingField;
			}
			set
			{
				Hour__BackingField = value;
			}
		}

		public int? Minute
		{
			get
			{
				return Minute__BackingField;
			}
			set
			{
				Minute__BackingField = value;
			}
		}

		public int? Second
		{
			get
			{
				return Second__BackingField;
			}
			set
			{
				Second__BackingField = value;
			}
		}

		public bool UtcTime
		{
			get
			{
				return UtcTime__BackingField;
			}
			set
			{
				UtcTime__BackingField = value;
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
