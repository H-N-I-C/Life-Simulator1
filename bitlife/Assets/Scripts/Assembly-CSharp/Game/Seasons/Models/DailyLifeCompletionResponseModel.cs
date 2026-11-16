using System;
using System.Collections.Generic;

namespace Game.Seasons.Models
{
	[Serializable]
	public class DailyLifeCompletionResponseModel
	{
		private DailyRewardGrantModel Grant__BackingField;

		private string LifeId__BackingField;

		private int CoinsAdded__BackingField;

		private long TotalCoins__BackingField;

		private DailyLifeCompletionStateModel LifeCompletion__BackingField;

		private string ConfigVersion__BackingField;

		private List<DailyRewardGrantModel> EconomicReceipts__BackingField;

		private DailyStreakResetModel StreakReset__BackingField;

		private List<DailyRentalEntitlementModel> RentalEntitlements__BackingField;

		public DailyRewardGrantModel Grant
		{
			get
			{
				return Grant__BackingField;
			}
			set
			{
				Grant__BackingField = value;
			}
		}

		public string LifeId
		{
			get
			{
				return LifeId__BackingField;
			}
			set
			{
				LifeId__BackingField = value;
			}
		}

		public int CoinsAdded
		{
			get
			{
				return CoinsAdded__BackingField;
			}
			set
			{
				CoinsAdded__BackingField = value;
			}
		}

		public long TotalCoins
		{
			get
			{
				return TotalCoins__BackingField;
			}
			set
			{
				TotalCoins__BackingField = value;
			}
		}

		public DailyLifeCompletionStateModel LifeCompletion
		{
			get
			{
				return LifeCompletion__BackingField;
			}
			set
			{
				LifeCompletion__BackingField = value;
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

		public List<DailyRewardGrantModel> EconomicReceipts
		{
			get
			{
				return EconomicReceipts__BackingField;
			}
			set
			{
				EconomicReceipts__BackingField = value;
			}
		}

		public DailyStreakResetModel StreakReset
		{
			get
			{
				return StreakReset__BackingField;
			}
			set
			{
				StreakReset__BackingField = value;
			}
		}

		public List<DailyRentalEntitlementModel> RentalEntitlements
		{
			get
			{
				return RentalEntitlements__BackingField;
			}
			set
			{
				RentalEntitlements__BackingField = value;
			}
		}
	}
}
