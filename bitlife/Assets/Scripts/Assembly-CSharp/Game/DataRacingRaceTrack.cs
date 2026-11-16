namespace Game
{
	public class DataRacingRaceTrack
	{
		private RACING_TRACK_CATEGORY TrackType__BackingField;

		private RACING_TRACK_INDEX TrackIndex__BackingField;

		private string TrackName__BackingField;

		private int BaselineLaps__BackingField;

		private int Difficulty__BackingField;

		private RACING_LICENSE UnlockRank__BackingField;

		private string GoalOne__BackingField;

		private string GoalTwo__BackingField;

		private string GoalThree__BackingField;

		private RACING_CAR_STAT RecommendedStat__BackingField;

		private string QATime__BackingField;

		private int MinWinnings__BackingField;

		private int MaxWinnings__BackingField;

		public RACING_TRACK_CATEGORY TrackType
		{
			get
			{
				return TrackType__BackingField;
			}
			set
			{
				TrackType__BackingField = value;
			}
		}

		public RACING_TRACK_INDEX TrackIndex
		{
			get
			{
				return TrackIndex__BackingField;
			}
			set
			{
				TrackIndex__BackingField = value;
			}
		}

		public string TrackName
		{
			get
			{
				return TrackName__BackingField;
			}
			set
			{
				TrackName__BackingField = value;
			}
		}

		public int BaselineLaps
		{
			get
			{
				return BaselineLaps__BackingField;
			}
			set
			{
				BaselineLaps__BackingField = value;
			}
		}

		public int Difficulty
		{
			get
			{
				return Difficulty__BackingField;
			}
			set
			{
				Difficulty__BackingField = value;
			}
		}

		public RACING_LICENSE UnlockRank
		{
			get
			{
				return UnlockRank__BackingField;
			}
			set
			{
				UnlockRank__BackingField = value;
			}
		}

		public string GoalOne
		{
			get
			{
				return GoalOne__BackingField;
			}
			set
			{
				GoalOne__BackingField = value;
			}
		}

		public string GoalTwo
		{
			get
			{
				return GoalTwo__BackingField;
			}
			set
			{
				GoalTwo__BackingField = value;
			}
		}

		public string GoalThree
		{
			get
			{
				return GoalThree__BackingField;
			}
			set
			{
				GoalThree__BackingField = value;
			}
		}

		public RACING_CAR_STAT RecommendedStat
		{
			get
			{
				return RecommendedStat__BackingField;
			}
			set
			{
				RecommendedStat__BackingField = value;
			}
		}

		public string QATime
		{
			get
			{
				return QATime__BackingField;
			}
			set
			{
				QATime__BackingField = value;
			}
		}

		public int MinWinnings
		{
			get
			{
				return MinWinnings__BackingField;
			}
			set
			{
				MinWinnings__BackingField = value;
			}
		}

		public int MaxWinnings
		{
			get
			{
				return MaxWinnings__BackingField;
			}
			set
			{
				MaxWinnings__BackingField = value;
			}
		}
	}
}
