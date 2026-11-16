using System.Collections.Generic;

namespace Tracking
{
	public class BitLife_EconomyTransaction : GameTelemetryEvent
	{
		private string Currency__BackingField;

		private string TransactionType__BackingField;

		private string TransactionInsightLevelA__BackingField;

		private string TransactionInsightLevelB__BackingField;

		private string TransactionInsightLevelC__BackingField;

		private string BalancePostTransaction__BackingField;

		private int TransactionAmount__BackingField;

		private int? StreakSnapshot__BackingField;

		private string UserIdSnapshot__BackingField;

		public const string Source = "source";

		public const string Sink = "sink";

		public const string TrackFree = "season_track_free";

		public const string TrackPremium = "season_track_premium";

		public const string IAP = "iap";

		public const string Shop = "seasonal_shop";

		public const string EndWipe = "end_of_season_wipe";

		public const string AutomaticUse = "automatic_use";

		public const string PlayerConsumed = "player_consumed";

		public const string HardCurrency = "HardCurrency";

		public const string DailyReturn = "daily_return";

		public const string LifeCompletion = "life_completion";

		public const string HardCurrencyMarketplace = "hard_currency_marketplace";

		public const string TimeMachine = "time_machine";

		public const string Buy48Hours = "buy_48_hours";

		public string Currency
		{
			get
			{
				return Currency__BackingField;
			}
			set
			{
				Currency__BackingField = value;
			}
		}

		public string TransactionType
		{
			get
			{
				return TransactionType__BackingField;
			}
			set
			{
				TransactionType__BackingField = value;
			}
		}

		public string TransactionInsightLevelA
		{
			get
			{
				return TransactionInsightLevelA__BackingField;
			}
			set
			{
				TransactionInsightLevelA__BackingField = value;
			}
		}

		public string TransactionInsightLevelB
		{
			get
			{
				return TransactionInsightLevelB__BackingField;
			}
			set
			{
				TransactionInsightLevelB__BackingField = value;
			}
		}

		public string TransactionInsightLevelC
		{
			get
			{
				return TransactionInsightLevelC__BackingField;
			}
			set
			{
				TransactionInsightLevelC__BackingField = value;
			}
		}

		public string BalancePostTransaction
		{
			get
			{
				return BalancePostTransaction__BackingField;
			}
			set
			{
				BalancePostTransaction__BackingField = value;
			}
		}

		public int TransactionAmount
		{
			get
			{
				return TransactionAmount__BackingField;
			}
			set
			{
				TransactionAmount__BackingField = value;
			}
		}

		public int? StreakSnapshot
		{
			get
			{
				return StreakSnapshot__BackingField;
			}
			set
			{
				StreakSnapshot__BackingField = value;
			}
		}

		public string UserIdSnapshot
		{
			get
			{
				return UserIdSnapshot__BackingField;
			}
			set
			{
				UserIdSnapshot__BackingField = value;
			}
		}

		public override Dictionary<string, string> GetData()
		{
			return null;
		}

		public static void SendStreakSaverEvent(string transaction)
		{
		}
	}
}
