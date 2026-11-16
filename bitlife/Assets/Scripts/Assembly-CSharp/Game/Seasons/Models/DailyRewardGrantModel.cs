using System;
using System.Collections.Generic;

namespace Game.Seasons.Models
{
	[Serializable]
	public class DailyRewardGrantModel
	{
		private string GrantId__BackingField;

		private int PeriodId__BackingField;

		private int RewardDay__BackingField;

		private int Amount__BackingField;

		private string GrantedAt__BackingField;

		private bool IsNew__BackingField;

		private string OperationKind__BackingField;

		private string Outcome__BackingField;

		private long? BalanceAfter__BackingField;

		private int? StreakCount__BackingField;

		private string CommittedAt__BackingField;

		private string PurchasedAt__BackingField;

		private int? Price__BackingField;

		private string ItemId__BackingField;

		private string CanonicalItemId__BackingField;

		private string Variant__BackingField;

		private string RentalWindowId__BackingField;

		private bool IsDebug__BackingField;

		private List<DailyPendingRewardModel> Rewards__BackingField;

		public string GrantId
		{
			get
			{
				return GrantId__BackingField;
			}
			set
			{
				GrantId__BackingField = value;
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

		public int RewardDay
		{
			get
			{
				return RewardDay__BackingField;
			}
			set
			{
				RewardDay__BackingField = value;
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

		public string GrantedAt
		{
			get
			{
				return GrantedAt__BackingField;
			}
			set
			{
				GrantedAt__BackingField = value;
			}
		}

		public bool IsNew
		{
			get
			{
				return IsNew__BackingField;
			}
			set
			{
				IsNew__BackingField = value;
			}
		}

		public string OperationKind
		{
			get
			{
				return OperationKind__BackingField;
			}
			set
			{
				OperationKind__BackingField = value;
			}
		}

		public string Outcome
		{
			get
			{
				return Outcome__BackingField;
			}
			set
			{
				Outcome__BackingField = value;
			}
		}

		public long? BalanceAfter
		{
			get
			{
				return BalanceAfter__BackingField;
			}
			set
			{
				BalanceAfter__BackingField = value;
			}
		}

		public int? StreakCount
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

		public string CommittedAt
		{
			get
			{
				return CommittedAt__BackingField;
			}
			set
			{
				CommittedAt__BackingField = value;
			}
		}

		public string PurchasedAt
		{
			get
			{
				return PurchasedAt__BackingField;
			}
			set
			{
				PurchasedAt__BackingField = value;
			}
		}

		public int? Price
		{
			get
			{
				return Price__BackingField;
			}
			set
			{
				Price__BackingField = value;
			}
		}

		public string ItemId
		{
			get
			{
				return ItemId__BackingField;
			}
			set
			{
				ItemId__BackingField = value;
			}
		}

		public string CanonicalItemId
		{
			get
			{
				return CanonicalItemId__BackingField;
			}
			set
			{
				CanonicalItemId__BackingField = value;
			}
		}

		public string Variant
		{
			get
			{
				return Variant__BackingField;
			}
			set
			{
				Variant__BackingField = value;
			}
		}

		public string RentalWindowId
		{
			get
			{
				return RentalWindowId__BackingField;
			}
			set
			{
				RentalWindowId__BackingField = value;
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

		public List<DailyPendingRewardModel> Rewards
		{
			get
			{
				return Rewards__BackingField;
			}
			set
			{
				Rewards__BackingField = value;
			}
		}
	}
}
