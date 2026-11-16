using System.Collections.Generic;
using Game;

namespace Backend
{
	public class StuffItem : IStuffItem
	{
		private sealed class _003C_003Ec__DisplayClass42_0
		{
			public string bundleName;

			internal bool _003CIsTimeIapEnabled_003Eb__0(string t)
			{
				return false;
			}
		}

		public string ITEM_ID;

		private int Amount__BackingField;

		private int TimeRemaining__BackingField;

		private int MinutesAvailable__BackingField;

		public bool IsCurrency;

		public bool HideInGame;

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

		public int TimeRemaining
		{
			get
			{
				return TimeRemaining__BackingField;
			}
			set
			{
				TimeRemaining__BackingField = value;
			}
		}

		public int MinutesAvailable
		{
			get
			{
				return MinutesAvailable__BackingField;
			}
			set
			{
				MinutesAvailable__BackingField = value;
			}
		}

		public string ItemName => null;

		public string PowerUp => null;

		public bool CanUseItem()
		{
			return false;
		}

		public string GetDescriptionByItemId()
		{
			return null;
		}

		private string GetNameByItemId()
		{
			return null;
		}

		public string GetRewardEmoji()
		{
			return null;
		}

		public string GetFeedbackString()
		{
			return null;
		}

		public string GetLogString()
		{
			return null;
		}

		public string GetSuccessString()
		{
			return null;
		}

		public Scenario GetNextScenario(IMyStuffUseCheck check)
		{
			return null;
		}

		public string GetButtonText()
		{
			return null;
		}

		public string GetConfirmationString()
		{
			return null;
		}

		public MYSTUFF_CATEGORY GetCategory()
		{
			return MYSTUFF_CATEGORY.None;
		}

		public string GetCategoryName()
		{
			return null;
		}

		public string GetFormattedQuantity(string timeLockedLabel = "Locked", string defaultLabel = "")
		{
			return null;
		}

		public string GetFormattedQuantity(DataMyStuffCatalog item, string timeLockedLabel = "Locked", string defaultLabel = "")
		{
			return null;
		}

		public static string GetBalance(StuffItem item)
		{
			return null;
		}

		public bool IsItemTimeBlocked()
		{
			return false;
		}

		public Scenario GetScenarioForUse()
		{
			return null;
		}

		public int GetTotalRemainingAmount()
		{
			return 0;
		}

		public string GetSeason()
		{
			return null;
		}

		public List<string> GetIapBundleNames()
		{
			return null;
		}

		public int GetTimeRemaining()
		{
			return 0;
		}

		public bool IsTimedItem()
		{
			return false;
		}

		public string GetRedeemId()
		{
			return null;
		}

		public bool IsTimeIapEnabled(string bundleName)
		{
			return false;
		}

		public string GetGenderString(string tag)
		{
			return null;
		}

		public bool TryGetGender(out GENDER gender)
		{
			gender = default;
			return false;
		}

		public string GetSecondaryEmoji()
		{
			return null;
		}
	}
}
