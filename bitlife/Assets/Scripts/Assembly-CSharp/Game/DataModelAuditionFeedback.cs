namespace Game
{
	public class DataModelAuditionFeedback
	{
		private string Reason__BackingField;

		private int MinLooks__BackingField;

		private int MaxLooks__BackingField;

		private int MinModelSkill__BackingField;

		private int MaxModelSkill__BackingField;

		public string Reason
		{
			get
			{
				return Reason__BackingField;
			}
			set
			{
				Reason__BackingField = value;
			}
		}

		public int MinLooks
		{
			get
			{
				return MinLooks__BackingField;
			}
			set
			{
				MinLooks__BackingField = value;
			}
		}

		public int MaxLooks
		{
			get
			{
				return MaxLooks__BackingField;
			}
			set
			{
				MaxLooks__BackingField = value;
			}
		}

		public int MinModelSkill
		{
			get
			{
				return MinModelSkill__BackingField;
			}
			set
			{
				MinModelSkill__BackingField = value;
			}
		}

		public int MaxModelSkill
		{
			get
			{
				return MaxModelSkill__BackingField;
			}
			set
			{
				MaxModelSkill__BackingField = value;
			}
		}
	}
}
