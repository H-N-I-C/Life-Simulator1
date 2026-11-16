using System;

namespace Game.Seasons.Models
{
	[Serializable]
	public class DailyLifeCompletionStateModel
	{
		private int Reward__BackingField;

		private int PeriodGranted__BackingField;

		private int PeriodCap__BackingField;

		private bool CanClaimThisPeriod__BackingField;

		public int Reward
		{
			get
			{
				return Reward__BackingField;
			}
			set
			{
				Reward__BackingField = value;
			}
		}

		public int PeriodGranted
		{
			get
			{
				return PeriodGranted__BackingField;
			}
			set
			{
				PeriodGranted__BackingField = value;
			}
		}

		public int PeriodCap
		{
			get
			{
				return PeriodCap__BackingField;
			}
			set
			{
				PeriodCap__BackingField = value;
			}
		}

		public bool CanClaimThisPeriod
		{
			get
			{
				return CanClaimThisPeriod__BackingField;
			}
			set
			{
				CanClaimThisPeriod__BackingField = value;
			}
		}
	}
}
