namespace Game.Seasons.Models
{
	public class CommunityGoalDataModel
	{
		public string EventId;

		public int RequiredCompletions;

		public string ObjectiveText;

		public bool Active;

		public string ObjectiveTitle;

		public string Emoji;

		public string ObjectiveSubTitle;

		public int TotalCompletions;

		public int AmountToGain;

		public bool IsComplete => false;

		public int GetCompletion()
		{
			return 0;
		}
	}
}
