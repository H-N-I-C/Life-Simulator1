namespace Game.Seasons.Models
{
	public class CommunityGoalModel
	{
		private string CommunityId__BackingField;

		private int Completions__BackingField;

		public string CommunityId
		{
			get
			{
				return CommunityId__BackingField;
			}
			set
			{
				CommunityId__BackingField = value;
			}
		}

		public int Completions
		{
			get
			{
				return Completions__BackingField;
			}
			set
			{
				Completions__BackingField = value;
			}
		}
	}
}
