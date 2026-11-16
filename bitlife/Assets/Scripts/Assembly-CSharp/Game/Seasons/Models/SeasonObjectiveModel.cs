namespace Game.Seasons.Models
{
	public class SeasonObjectiveModel
	{
		private string ObjectiveId__BackingField;

		private string Name__BackingField;

		private string Description__BackingField;

		private string Emoji__BackingField;

		private int AmountToGain__BackingField;

		private bool IsDivider__BackingField;

		private int RequiredCompletions__BackingField;

		private int TotalCompletions__BackingField;

		private bool IsStreak__BackingField;

		private string SendToLocation__BackingField;

		private bool IsVip__BackingField;

		private string VipType__BackingField;

		private bool IsActive__BackingField;

		public string ObjectiveId
		{
			get
			{
				return ObjectiveId__BackingField;
			}
			set
			{
				ObjectiveId__BackingField = value;
			}
		}

		public string Name
		{
			get
			{
				return Name__BackingField;
			}
			set
			{
				Name__BackingField = value;
			}
		}

		public string Description
		{
			get
			{
				return Description__BackingField;
			}
			set
			{
				Description__BackingField = value;
			}
		}

		public string Emoji
		{
			get
			{
				return Emoji__BackingField;
			}
			set
			{
				Emoji__BackingField = value;
			}
		}

		public int AmountToGain
		{
			get
			{
				return AmountToGain__BackingField;
			}
			set
			{
				AmountToGain__BackingField = value;
			}
		}

		public bool IsDivider
		{
			get
			{
				return IsDivider__BackingField;
			}
			set
			{
				IsDivider__BackingField = value;
			}
		}

		public int RequiredCompletions
		{
			get
			{
				return RequiredCompletions__BackingField;
			}
			set
			{
				RequiredCompletions__BackingField = value;
			}
		}

		public int TotalCompletions
		{
			get
			{
				return TotalCompletions__BackingField;
			}
			set
			{
				TotalCompletions__BackingField = value;
			}
		}

		public bool IsStreak
		{
			get
			{
				return IsStreak__BackingField;
			}
			set
			{
				IsStreak__BackingField = value;
			}
		}

		public string SendToLocation
		{
			get
			{
				return SendToLocation__BackingField;
			}
			set
			{
				SendToLocation__BackingField = value;
			}
		}

		public bool IsVip
		{
			get
			{
				return IsVip__BackingField;
			}
			set
			{
				IsVip__BackingField = value;
			}
		}

		public string VipType
		{
			get
			{
				return VipType__BackingField;
			}
			set
			{
				VipType__BackingField = value;
			}
		}

		public bool IsActive
		{
			get
			{
				return IsActive__BackingField;
			}
			set
			{
				IsActive__BackingField = value;
			}
		}

		public bool IsCommunityGoal()
		{
			return false;
		}
	}
}
