using System;
using System.Collections.Generic;

namespace Game.Seasons.Models
{
	public class SeasonsDataModel
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<SeasonObjectiveModel, bool> _003C_003E9__86_0;

			internal bool _003CGetObjectives_003Eb__86_0(SeasonObjectiveModel o)
			{
				return false;
			}
		}

		private bool Active__BackingField;

		private string ServerDateTime__BackingField;

		private string ServerStartDateTime__BackingField;

		private string ServerEndDateTime__BackingField;

		private SeasonNarrativeModel CurrentNarrative__BackingField;

		public List<TrackItemModel> TrackData;

		private ShopModel ShopList__BackingField;

		private List<SeasonObjectiveModel> Objectives__BackingField;

		private bool SeasonalCommunityGoalsEnabled__BackingField;

		private List<CommunityGoalDataModel> CommunityGoals__BackingField;

		private string CurrencySingular__BackingField;

		private string CurrencyPlural__BackingField;

		private string SeasonDescription__BackingField;

		private bool ShopEnabled__BackingField;

		private const string DefaultShopTitle = "Shop";

		private const string DefaultCurrencyTitle = "Seasonal Currency";

		private TimeSpan TimeRemaining__BackingField;

		private TimeSpan Duration__BackingField;

		private float Progress__BackingField;

		private TimeSpan TimeSinceStart__BackingField;

		private int RequiredAgeUps__BackingField;

		private string CurrencyId__BackingField;

		private bool DailyNarrativeActive__BackingField;

		public bool Active
		{
			get
			{
				return Active__BackingField;
			}
			set
			{
				Active__BackingField = value;
			}
		}

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

		public string ServerStartDateTime
		{
			get
			{
				return ServerStartDateTime__BackingField;
			}
			set
			{
				ServerStartDateTime__BackingField = value;
			}
		}

		public string ServerEndDateTime
		{
			get
			{
				return ServerEndDateTime__BackingField;
			}
			set
			{
				ServerEndDateTime__BackingField = value;
			}
		}

		public SeasonNarrativeModel CurrentNarrative
		{
			get
			{
				return CurrentNarrative__BackingField;
			}
			set
			{
				CurrentNarrative__BackingField = value;
			}
		}

		public ShopModel ShopList
		{
			get
			{
				return ShopList__BackingField;
			}
			set
			{
				ShopList__BackingField = value;
			}
		}

		public List<SeasonObjectiveModel> Objectives
		{
			get
			{
				return Objectives__BackingField;
			}
			set
			{
				Objectives__BackingField = value;
			}
		}

		public bool SeasonalCommunityGoalsEnabled
		{
			get
			{
				return SeasonalCommunityGoalsEnabled__BackingField;
			}
			set
			{
				SeasonalCommunityGoalsEnabled__BackingField = value;
			}
		}

		public List<CommunityGoalDataModel> CommunityGoals
		{
			get
			{
				return CommunityGoals__BackingField;
			}
			set
			{
				CommunityGoals__BackingField = value;
			}
		}

		public string CurrencySingular
		{
			get
			{
				return CurrencySingular__BackingField;
			}
			set
			{
				CurrencySingular__BackingField = value;
			}
		}

		public string CurrencyPlural
		{
			get
			{
				return CurrencyPlural__BackingField;
			}
			set
			{
				CurrencyPlural__BackingField = value;
			}
		}

		public string SeasonDescription
		{
			get
			{
				return SeasonDescription__BackingField;
			}
			set
			{
				SeasonDescription__BackingField = value;
			}
		}

		public bool ShopEnabled
		{
			get
			{
				return ShopEnabled__BackingField;
			}
			set
			{
				ShopEnabled__BackingField = value;
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

		public TimeSpan Duration
		{
			get
			{
				return Duration__BackingField;
			}
			private set
			{
				Duration__BackingField = value;
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

		public TimeSpan TimeSinceStart
		{
			get
			{
				return TimeSinceStart__BackingField;
			}
			private set
			{
				TimeSinceStart__BackingField = value;
			}
		}

		public int RequiredAgeUps
		{
			get
			{
				return RequiredAgeUps__BackingField;
			}
			set
			{
				RequiredAgeUps__BackingField = value;
			}
		}

		public string CurrencyId
		{
			get
			{
				return CurrencyId__BackingField;
			}
			set
			{
				CurrencyId__BackingField = value;
			}
		}

		public bool DailyNarrativeActive
		{
			get
			{
				return DailyNarrativeActive__BackingField;
			}
			set
			{
				DailyNarrativeActive__BackingField = value;
			}
		}

		public void GetTitleAndCurrency(bool plural, out string title, out string currency)
		{
			title = null;
			currency = null;
		}

		public bool IsServerTimeNewerThan(SeasonsDataModel other)
		{
			return false;
		}

		public SeasonsDataModel Update(SeasonsStreakModel streak)
		{
			return null;
		}

		public List<SeasonObjectiveModel> GetObjectives()
		{
			return null;
		}
	}
}
