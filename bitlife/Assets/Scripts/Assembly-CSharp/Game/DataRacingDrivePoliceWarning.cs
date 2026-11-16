namespace Game
{
	public class DataRacingDrivePoliceWarning
	{
		private string FeedbackString__BackingField;

		private string LogVersion__BackingField;

		private bool HeroGoingTooSlow__BackingField;

		private bool HeroGoingTooFast__BackingField;

		public string FeedbackString
		{
			get
			{
				return FeedbackString__BackingField;
			}
			set
			{
				FeedbackString__BackingField = value;
			}
		}

		public string LogVersion
		{
			get
			{
				return LogVersion__BackingField;
			}
			set
			{
				LogVersion__BackingField = value;
			}
		}

		public bool HeroGoingTooSlow
		{
			get
			{
				return HeroGoingTooSlow__BackingField;
			}
			set
			{
				HeroGoingTooSlow__BackingField = value;
			}
		}

		public bool HeroGoingTooFast
		{
			get
			{
				return HeroGoingTooFast__BackingField;
			}
			set
			{
				HeroGoingTooFast__BackingField = value;
			}
		}
	}
}
