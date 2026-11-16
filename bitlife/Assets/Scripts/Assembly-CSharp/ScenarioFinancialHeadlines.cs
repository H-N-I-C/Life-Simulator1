using System;
using System.Collections.Generic;

public class ScenarioFinancialHeadlines : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimStock, bool> _003C_003E9__3_1;

		public static Func<SimInvestment, double> _003C_003E9__3_2;

		internal bool _003CRefreshScenario_003Eb__3_1(SimStock x)
		{
			return false;
		}

		internal double _003CRefreshScenario_003Eb__3_2(SimInvestment x)
		{
			return 0.0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass3_0
	{
		public int ageToCheck;

		internal bool _003CRefreshScenario_003Eb__0(SimStock x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass3_1
	{
		public int sector;

		internal bool _003CRefreshScenario_003Eb__3(SimStockSector x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass3_2
	{
		public SimStock randomStock;

		public int sector;

		public SimCrypto crypto;

		public Func<string, bool> _003C_003E9__5;

		public Func<string, bool> _003C_003E9__6;

		internal bool _003CRefreshScenario_003Eb__5(string x)
		{
			return false;
		}

		internal bool _003CRefreshScenario_003Eb__4(SimStockSector x)
		{
			return false;
		}

		internal bool _003CRefreshScenario_003Eb__6(string x)
		{
			return false;
		}
	}

	protected MiniGameDataFinancialHeadlines _gameData;

	protected List<DataFinancialHeadline> UsedHeadlinesList;

	public override void RefreshScenario()
	{
	}
}
