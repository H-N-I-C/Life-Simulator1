using System;

namespace Game.Seasons.Models
{
	public class SeasonsStreakModel
	{
		private string ServerDateTime__BackingField;

		private string StreakEndDateTime__BackingField;

		private int CurrentStreakDay__BackingField;

		private string JeopardyTimeStart__BackingField;

		private int JeopardyMinutes__BackingField;

		private STREAK_STATUS StreakStatus__BackingField;

		private int StreakCount__BackingField;

		private int LastStreakCount__BackingField;

		private int LastStreakDay__BackingField;

		private TimeSpan TimeRemaining__BackingField;

		private float Progress__BackingField;

		private int CurrentStreakDayOrdinal__BackingField;

		private DateTime? JeopardyTarget;

		private STREAK_STATUS LastStatus;

		private static DateTime? OverrideDailyTarget;

		private const int DailyDays = 1;

		private const int FallbackJeopardyMinutes = 15;

		private static int _lastDayTrackingReportedJeopardy;

		public string ServerDateTime
		{
			get
			{
				return ServerDateTime__BackingField;
			}
			set
			{
				ServerDateTime__BackingField = value;
			}
		}

		public string StreakEndDateTime
		{
			get
			{
				return StreakEndDateTime__BackingField;
			}
			set
			{
				StreakEndDateTime__BackingField = value;
			}
		}

		public int CurrentStreakDay
		{
			get
			{
				return CurrentStreakDay__BackingField;
			}
			set
			{
				CurrentStreakDay__BackingField = value;
			}
		}

		public string JeopardyTimeStart
		{
			get
			{
				return JeopardyTimeStart__BackingField;
			}
			set
			{
				JeopardyTimeStart__BackingField = value;
			}
		}

		public int JeopardyMinutes
		{
			get
			{
				return JeopardyMinutes__BackingField;
			}
			set
			{
				JeopardyMinutes__BackingField = value;
			}
		}

		public STREAK_STATUS StreakStatus
		{
			get
			{
				return StreakStatus__BackingField;
			}
			set
			{
				StreakStatus__BackingField = value;
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

		public int LastStreakCount
		{
			get
			{
				return LastStreakCount__BackingField;
			}
			set
			{
				LastStreakCount__BackingField = value;
			}
		}

		public int LastStreakDay
		{
			get
			{
				return LastStreakDay__BackingField;
			}
			set
			{
				LastStreakDay__BackingField = value;
			}
		}

		public TimeSpan TimeRemaining
		{
			get
			{
				return TimeRemaining__BackingField;
			}
			private set
			{
				TimeRemaining__BackingField = value;
			}
		}

		public float Progress
		{
			get
			{
				return Progress__BackingField;
			}
			private set
			{
				Progress__BackingField = value;
			}
		}

		public int CurrentStreakDayOrdinal
		{
			get
			{
				return CurrentStreakDayOrdinal__BackingField;
			}
			private set
			{
				CurrentStreakDayOrdinal__BackingField = value;
			}
		}

		private static TimeSpan Duration => default;

		public bool IsFullStreak()
		{
			return false;
		}

		public static void ToggleOverrideDaily(double minutesFromNow = 1.0)
		{
		}

		public SeasonsStreakModel Update()
		{
			return null;
		}

		private void SetJeopardyTarget(int minutes)
		{
		}
	}
}
