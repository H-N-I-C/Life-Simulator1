using System;
using System.Collections.Generic;
using Backend;

namespace Game.Seasons.Models
{
	[Serializable]
	public class SeasonsDailyRewardModel
	{
		private int PeriodId__BackingField;

		private string NextRefreshAt__BackingField;

		private int StreakCount__BackingField;

		private int CurrentDay__BackingField;

		private List<int> DailyRewards__BackingField;

		private long TotalCoins__BackingField;

		private bool RewardedToday__BackingField;

		private bool NewRewardAvailable__BackingField;

		private DailyRewardGrantModel Grant__BackingField;

		private Dictionary<string, int> RedeemedItems__BackingField;

		private DailyLifeCompletionStateModel LifeCompletion__BackingField;

		private DailyMarketplaceModel Marketplace__BackingField;

		private DailyTimeMachineModel TimeMachine__BackingField;

		private List<StuffItem> MyStuffItems__BackingField;

		private string ConfigVersion__BackingField;

		private string ServerNow__BackingField;

		private int StateVersion__BackingField;

		private int TodayRewardAmount__BackingField;

		private int NextRewardAmount__BackingField;

		private int TotalPendingAmount__BackingField;

		private List<DailyPendingRewardModel> PendingRewards__BackingField;

		private List<DailyRewardGrantModel> RecoveryReceipts__BackingField;

		private DailyRewardGrantModel Receipt__BackingField;

		private int CoinsAdded__BackingField;

		private bool? Duplicate__BackingField;

		private List<DailyRewardGrantModel> EconomicReceipts__BackingField;

		private DailyStreakResetModel StreakReset__BackingField;

		private List<DailyRentalEntitlementModel> RentalEntitlements__BackingField;

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

		public string NextRefreshAt
		{
			get
			{
				return NextRefreshAt__BackingField;
			}
			set
			{
				NextRefreshAt__BackingField = value;
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

		public int CurrentDay
		{
			get
			{
				return CurrentDay__BackingField;
			}
			set
			{
				CurrentDay__BackingField = value;
			}
		}

		public List<int> DailyRewards
		{
			get
			{
				return DailyRewards__BackingField;
			}
			set
			{
				DailyRewards__BackingField = value;
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

		public bool RewardedToday
		{
			get
			{
				return RewardedToday__BackingField;
			}
			set
			{
				RewardedToday__BackingField = value;
			}
		}

		public bool NewRewardAvailable
		{
			get
			{
				return NewRewardAvailable__BackingField;
			}
			set
			{
				NewRewardAvailable__BackingField = value;
			}
		}

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

		public Dictionary<string, int> RedeemedItems
		{
			get
			{
				return RedeemedItems__BackingField;
			}
			set
			{
				RedeemedItems__BackingField = value;
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

		public DailyMarketplaceModel Marketplace
		{
			get
			{
				return Marketplace__BackingField;
			}
			set
			{
				Marketplace__BackingField = value;
			}
		}

		public DailyTimeMachineModel TimeMachine
		{
			get
			{
				return TimeMachine__BackingField;
			}
			set
			{
				TimeMachine__BackingField = value;
			}
		}

		public List<StuffItem> MyStuffItems
		{
			get
			{
				return MyStuffItems__BackingField;
			}
			set
			{
				MyStuffItems__BackingField = value;
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

		public string ServerNow
		{
			get
			{
				return ServerNow__BackingField;
			}
			set
			{
				ServerNow__BackingField = value;
			}
		}

		public int StateVersion
		{
			get
			{
				return StateVersion__BackingField;
			}
			set
			{
				StateVersion__BackingField = value;
			}
		}

		public int TodayRewardAmount
		{
			get
			{
				return TodayRewardAmount__BackingField;
			}
			set
			{
				TodayRewardAmount__BackingField = value;
			}
		}

		public int NextRewardAmount
		{
			get
			{
				return NextRewardAmount__BackingField;
			}
			set
			{
				NextRewardAmount__BackingField = value;
			}
		}

		public int TotalPendingAmount
		{
			get
			{
				return TotalPendingAmount__BackingField;
			}
			set
			{
				TotalPendingAmount__BackingField = value;
			}
		}

		public List<DailyPendingRewardModel> PendingRewards
		{
			get
			{
				return PendingRewards__BackingField;
			}
			set
			{
				PendingRewards__BackingField = value;
			}
		}

		public List<DailyRewardGrantModel> RecoveryReceipts
		{
			get
			{
				return RecoveryReceipts__BackingField;
			}
			set
			{
				RecoveryReceipts__BackingField = value;
			}
		}

		public DailyRewardGrantModel Receipt
		{
			get
			{
				return Receipt__BackingField;
			}
			set
			{
				Receipt__BackingField = value;
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

		public bool? Duplicate
		{
			get
			{
				return Duplicate__BackingField;
			}
			set
			{
				Duplicate__BackingField = value;
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

		public DateTime NextRefreshUtc => default;

		public int RewardForDay(int day)
		{
			return 0;
		}
	}
}
