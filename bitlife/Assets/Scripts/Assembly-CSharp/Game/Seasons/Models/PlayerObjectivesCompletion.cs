namespace Game.Seasons.Models
{
	public class PlayerObjectivesCompletion
	{
		private string ObjectiveId__BackingField;

		private int Completion__BackingField;

		private int StreakDay__BackingField;

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

		public int Completion
		{
			get
			{
				return Completion__BackingField;
			}
			set
			{
				Completion__BackingField = value;
			}
		}

		public int StreakDay
		{
			get
			{
				return StreakDay__BackingField;
			}
			set
			{
				StreakDay__BackingField = value;
			}
		}
	}
}
