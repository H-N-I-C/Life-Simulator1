using System.Collections.Generic;

namespace Tracking
{
	public class BitLifeEventAppStart : IGameTelemetryEvent
	{
		private readonly string NumLivesInProgress__BackingField;

		private readonly string LivesCompleted__BackingField;

		private readonly string ColorTheme__BackingField;

		private readonly string WebShopVariant__BackingField;

		private readonly string LinkUsedGivesRewardID__BackingField;

		private readonly string ReportBalancesBinaryBitPass__BackingField;

		private readonly string ReportBalancesCountStreakSaver__BackingField;

		private readonly string ReportBalancesCountSeasonalCurrency__BackingField;

		private readonly string ReportBalancesCountHardCurrency__BackingField;

		private readonly string ReportBalancesInfoSeasonalCurrency__BackingField;

		private readonly string ReportBalancesCountable__BackingField;

		private readonly string ReportBalancesTimed__BackingField;

		private readonly string DoesPlayerOwnUltimateFighterMode__BackingField;

		public string NumLivesInProgress => NumLivesInProgress__BackingField;

		public string LivesCompleted => LivesCompleted__BackingField;

		public string ColorTheme => ColorTheme__BackingField;

		public string WebShopVariant => WebShopVariant__BackingField;

		public string LinkUsedGivesRewardID => LinkUsedGivesRewardID__BackingField;

		public string ReportBalancesBinaryBitPass => ReportBalancesBinaryBitPass__BackingField;

		public string ReportBalancesCountStreakSaver => ReportBalancesCountStreakSaver__BackingField;

		public string ReportBalancesCountSeasonalCurrency => ReportBalancesCountSeasonalCurrency__BackingField;

		public string ReportBalancesCountHardCurrency => ReportBalancesCountHardCurrency__BackingField;

		public string ReportBalancesInfoSeasonalCurrency => ReportBalancesInfoSeasonalCurrency__BackingField;

		public string ReportBalancesCountable => ReportBalancesCountable__BackingField;

		public string ReportBalancesTimed => ReportBalancesTimed__BackingField;

		public string DoesPlayerOwnUltimateFighterMode => DoesPlayerOwnUltimateFighterMode__BackingField;

		private static string GetHardCurrencyBalance()
		{
			return null;
		}

		private static string GetCurrencyNameForSeason()
		{
			return null;
		}

		public static string GetWebShopVariant()
		{
			return null;
		}

		private static string GetTheme()
		{
			return null;
		}

		public Dictionary<string, string> GetData()
		{
			return null;
		}
	}
}
