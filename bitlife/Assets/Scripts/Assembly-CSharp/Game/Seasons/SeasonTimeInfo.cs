using Game.Seasons.Models;

namespace Game.Seasons
{
	public class SeasonTimeInfo
	{
		private SeasonsStreakModel Streak__BackingField;

		private SeasonsDataModel Season__BackingField;

		private string SeasonDisplay__BackingField;

		private string DailyDisplay__BackingField;

		public SeasonsStreakModel Streak
		{
			get
			{
				return Streak__BackingField;
			}
			private set
			{
				Streak__BackingField = value;
			}
		}

		public SeasonsDataModel Season
		{
			get
			{
				return Season__BackingField;
			}
			private set
			{
				Season__BackingField = value;
			}
		}

		public string SeasonDisplay
		{
			get
			{
				return SeasonDisplay__BackingField;
			}
			private set
			{
				SeasonDisplay__BackingField = value;
			}
		}

		public string DailyDisplay
		{
			get
			{
				return DailyDisplay__BackingField;
			}
			private set
			{
				DailyDisplay__BackingField = value;
			}
		}

		public bool IsActiveSeason => false;

		public bool IsActiveStreak => false;

		public void Update(SeasonModel data)
		{
		}
	}
}
