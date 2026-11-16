using System.Collections.Generic;

namespace Game.Seasons.Models
{
	public class SeasonsPlayerDataModel
	{
		private int PlayerCurrency__BackingField;

		private int PlayerCredits__BackingField;

		private bool BattlePassPurchased__BackingField;

		private bool SeasonUnlocked__BackingField;

		private List<SeasonRedeemModel> Redeemed__BackingField;

		private List<SeasonNarrativeCompletionModel> Narratives__BackingField;

		private List<PlayerObjectivesCompletion> ObjectivesCompleted__BackingField;

		private List<CommunityGoalModel> CommunityGoalCompletions__BackingField;

		private List<string> UnlockedRewards__BackingField;

		private bool ViewSeasonStoryUnlock__BackingField;

		public int PlayerCurrency
		{
			get
			{
				return PlayerCurrency__BackingField;
			}
			set
			{
				PlayerCurrency__BackingField = value;
			}
		}

		public int PlayerCredits
		{
			get
			{
				return PlayerCredits__BackingField;
			}
			set
			{
				PlayerCredits__BackingField = value;
			}
		}

		public bool BattlePassPurchased
		{
			get
			{
				return BattlePassPurchased__BackingField;
			}
			set
			{
				BattlePassPurchased__BackingField = value;
			}
		}

		public bool SeasonUnlocked
		{
			get
			{
				return SeasonUnlocked__BackingField;
			}
			set
			{
				SeasonUnlocked__BackingField = value;
			}
		}

		public List<SeasonRedeemModel> Redeemed
		{
			get
			{
				return Redeemed__BackingField;
			}
			set
			{
				Redeemed__BackingField = value;
			}
		}

		public List<SeasonNarrativeCompletionModel> Narratives
		{
			get
			{
				return Narratives__BackingField;
			}
			set
			{
				Narratives__BackingField = value;
			}
		}

		public List<PlayerObjectivesCompletion> ObjectivesCompleted
		{
			get
			{
				return ObjectivesCompleted__BackingField;
			}
			set
			{
				ObjectivesCompleted__BackingField = value;
			}
		}

		public List<CommunityGoalModel> CommunityGoalCompletions
		{
			get
			{
				return CommunityGoalCompletions__BackingField;
			}
			set
			{
				CommunityGoalCompletions__BackingField = value;
			}
		}

		public List<string> UnlockedRewards
		{
			get
			{
				return UnlockedRewards__BackingField;
			}
			set
			{
				UnlockedRewards__BackingField = value;
			}
		}

		public bool ViewSeasonStoryUnlock
		{
			get
			{
				return ViewSeasonStoryUnlock__BackingField;
			}
			set
			{
				ViewSeasonStoryUnlock__BackingField = value;
			}
		}

		public SeasonRewardModelItem UnlockNextReward(List<SeasonRewardModelItem> rewards)
		{
			return null;
		}

		public SeasonRewardModelItem GetFirstUnlockedReward(List<SeasonRewardModelItem> rewards)
		{
			return null;
		}
	}
}
