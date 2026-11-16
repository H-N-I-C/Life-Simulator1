using System;

namespace Game.Seasons.Models
{
	[Serializable]
	public class DailyPendingRewardModel
	{
		private string RewardId__BackingField;

		private int PeriodId__BackingField;

		private int Tier__BackingField;

		private int Amount__BackingField;

		private int? QualifiedStreakCount__BackingField;

		private string ConfigVersion__BackingField;

		public string RewardId
		{
			get
			{
				return RewardId__BackingField;
			}
			set
			{
				RewardId__BackingField = value;
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

		public int Tier
		{
			get
			{
				return Tier__BackingField;
			}
			set
			{
				Tier__BackingField = value;
			}
		}

		public int Amount
		{
			get
			{
				return Amount__BackingField;
			}
			set
			{
				Amount__BackingField = value;
			}
		}

		public int? QualifiedStreakCount
		{
			get
			{
				return QualifiedStreakCount__BackingField;
			}
			set
			{
				QualifiedStreakCount__BackingField = value;
			}
		}

		public string ConfigVersion
		{
			get
			{
				return ConfigVersion__BackingField;
			}
			set
			{
				ConfigVersion__BackingField = value;
			}
		}
	}
}
