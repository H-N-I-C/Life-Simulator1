namespace Game.Seasons.Models
{
	public class SeasonRewardModelItem
	{
		private string ITEM_ID__BackingField;

		private string RewardTitle__BackingField;

		private string RewardSubTitle__BackingField;

		private string RewardDescription__BackingField;

		private string Category__BackingField;

		private string Emoji__BackingField;

		private bool OnlyInformation__BackingField;

		public string ITEM_ID
		{
			get
			{
				return ITEM_ID__BackingField;
			}
			set
			{
				ITEM_ID__BackingField = value;
			}
		}

		public string RewardTitle
		{
			get
			{
				return RewardTitle__BackingField;
			}
			set
			{
				RewardTitle__BackingField = value;
			}
		}

		public string RewardSubTitle
		{
			get
			{
				return RewardSubTitle__BackingField;
			}
			set
			{
				RewardSubTitle__BackingField = value;
			}
		}

		public string RewardDescription
		{
			get
			{
				return RewardDescription__BackingField;
			}
			set
			{
				RewardDescription__BackingField = value;
			}
		}

		public string Category
		{
			get
			{
				return Category__BackingField;
			}
			set
			{
				Category__BackingField = value;
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

		public bool OnlyInformation
		{
			get
			{
				return OnlyInformation__BackingField;
			}
			set
			{
				OnlyInformation__BackingField = value;
			}
		}
	}
}
