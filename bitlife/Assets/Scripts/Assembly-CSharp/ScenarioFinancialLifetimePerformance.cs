using System;
using System.Collections.Generic;

public class ScenarioFinancialLifetimePerformance : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<KeyValuePair<int, SimPortfolioAnnualReport>, double> _003C_003E9__3_0;

		public static Func<KeyValuePair<int, SimPortfolioAnnualReport>, double> _003C_003E9__3_1;

		internal double _003CRefreshScenario_003Eb__3_0(KeyValuePair<int, SimPortfolioAnnualReport> x)
		{
			return 0.0;
		}

		internal double _003CRefreshScenario_003Eb__3_1(KeyValuePair<int, SimPortfolioAnnualReport> x)
		{
			return 0.0;
		}
	}

	protected SimPortfolioAnnualReport _lastReport;

	protected MiniGameDataFinancialLifetimePerformance _gameData;

	public override void RefreshScenario()
	{
	}
}
