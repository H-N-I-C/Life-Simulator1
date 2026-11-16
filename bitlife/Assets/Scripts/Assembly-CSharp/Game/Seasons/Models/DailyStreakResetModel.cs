using System;

namespace Game.Seasons.Models
{
	[Serializable]
	public class DailyStreakResetModel
	{
		private string ResetId__BackingField;

		private int PreviousStreak__BackingField;

		private int StreakCount__BackingField;

		private int PeriodId__BackingField;

		private string ResetAt__BackingField;

		private string Reason__BackingField;

		private int? MissedPeriods__BackingField;

		private bool IsDebug__BackingField;

		public string ResetId
		{
			get
			{
				return ResetId__BackingField;
			}
			set
			{
				ResetId__BackingField = value;
			}
		}

		public int PreviousStreak
		{
			get
			{
				return PreviousStreak__BackingField;
			}
			set
			{
				PreviousStreak__BackingField = value;
			}
		}

		public int StreakCount
		{
			get
			{
				return StreakCount__BackingField;
			}
			set
			{
				StreakCount__BackingField = value;
			}
		}

		public int PeriodId
		{
			get
			{
				return PeriodId__BackingField;
			}
			set
			{
				PeriodId__BackingField = value;
			}
		}

		public string ResetAt
		{
			get
			{
				return ResetAt__BackingField;
			}
			set
			{
				ResetAt__BackingField = value;
			}
		}

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

		public int? MissedPeriods
		{
			get
			{
				return MissedPeriods__BackingField;
			}
			set
			{
				MissedPeriods__BackingField = value;
			}
		}

		public bool IsDebug
		{
			get
			{
				return IsDebug__BackingField;
			}
			set
			{
				IsDebug__BackingField = value;
			}
		}
	}
}
