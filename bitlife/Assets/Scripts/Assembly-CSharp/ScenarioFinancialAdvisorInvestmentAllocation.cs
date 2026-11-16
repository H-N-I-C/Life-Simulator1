using System;
using System.Collections.Generic;

public class ScenarioFinancialAdvisorInvestmentAllocation : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<KeyValuePair<SimBond, double>, bool> _003C_003E9__16_1;

		public static Func<KeyValuePair<SimBond, double>, double> _003C_003E9__16_2;

		public static Func<KeyValuePair<SimBond, double>, bool> _003C_003E9__16_3;

		public static Func<KeyValuePair<SimBond, double>, double> _003C_003E9__16_4;

		public static Func<KeyValuePair<SimBond, double>, bool> _003C_003E9__16_5;

		public static Func<KeyValuePair<SimBond, double>, double> _003C_003E9__16_6;

		public static Func<SimStock, bool> _003C_003E9__16_9;

		public static Func<SimStock, bool> _003C_003E9__16_10;

		public static Func<SimStock, bool> _003C_003E9__16_11;

		public static Func<SimStock, bool> _003C_003E9__16_12;

		public static Func<SimBond, bool> _003C_003E9__16_14;

		public static Func<SimBond, bool> _003C_003E9__16_15;

		public static Func<SimBond, bool> _003C_003E9__16_16;

		public static Func<KeyValuePair<SimBond, double>, bool> _003C_003E9__16_17;

		public static Func<KeyValuePair<SimBond, double>, double> _003C_003E9__16_18;

		public static Func<KeyValuePair<SimBond, double>, bool> _003C_003E9__16_19;

		public static Func<KeyValuePair<SimBond, double>, double> _003C_003E9__16_20;

		public static Func<KeyValuePair<SimBond, double>, bool> _003C_003E9__16_21;

		public static Func<KeyValuePair<SimBond, double>, double> _003C_003E9__16_22;

		public static Func<int, int> _003C_003E9__18_0;

		public static Func<int, int> _003C_003E9__18_1;

		internal bool _003CInvestAvailableFunds_003Eb__16_1(KeyValuePair<SimBond, double> x)
		{
			return false;
		}

		internal double _003CInvestAvailableFunds_003Eb__16_2(KeyValuePair<SimBond, double> x)
		{
			return 0.0;
		}

		internal bool _003CInvestAvailableFunds_003Eb__16_3(KeyValuePair<SimBond, double> x)
		{
			return false;
		}

		internal double _003CInvestAvailableFunds_003Eb__16_4(KeyValuePair<SimBond, double> x)
		{
			return 0.0;
		}

		internal bool _003CInvestAvailableFunds_003Eb__16_5(KeyValuePair<SimBond, double> x)
		{
			return false;
		}

		internal double _003CInvestAvailableFunds_003Eb__16_6(KeyValuePair<SimBond, double> x)
		{
			return 0.0;
		}

		internal bool _003CInvestAvailableFunds_003Eb__16_9(SimStock x)
		{
			return false;
		}

		internal bool _003CInvestAvailableFunds_003Eb__16_10(SimStock x)
		{
			return false;
		}

		internal bool _003CInvestAvailableFunds_003Eb__16_11(SimStock x)
		{
			return false;
		}

		internal bool _003CInvestAvailableFunds_003Eb__16_12(SimStock x)
		{
			return false;
		}

		internal bool _003CInvestAvailableFunds_003Eb__16_14(SimBond x)
		{
			return false;
		}

		internal bool _003CInvestAvailableFunds_003Eb__16_15(SimBond x)
		{
			return false;
		}

		internal bool _003CInvestAvailableFunds_003Eb__16_16(SimBond x)
		{
			return false;
		}

		internal bool _003CInvestAvailableFunds_003Eb__16_17(KeyValuePair<SimBond, double> x)
		{
			return false;
		}

		internal double _003CInvestAvailableFunds_003Eb__16_18(KeyValuePair<SimBond, double> x)
		{
			return 0.0;
		}

		internal bool _003CInvestAvailableFunds_003Eb__16_19(KeyValuePair<SimBond, double> x)
		{
			return false;
		}

		internal double _003CInvestAvailableFunds_003Eb__16_20(KeyValuePair<SimBond, double> x)
		{
			return 0.0;
		}

		internal bool _003CInvestAvailableFunds_003Eb__16_21(KeyValuePair<SimBond, double> x)
		{
			return false;
		}

		internal double _003CInvestAvailableFunds_003Eb__16_22(KeyValuePair<SimBond, double> x)
		{
			return 0.0;
		}

		internal int _003CDistributeAmounts_003Eb__18_0(int x)
		{
			return 0;
		}

		internal int _003CDistributeAmounts_003Eb__18_1(int x)
		{
			return 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass16_0
	{
		public SimStock theStock;

		internal bool _003CInvestAvailableFunds_003Eb__0(SimInvestment x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass16_1
	{
		public SimBond theBond;

		internal bool _003CInvestAvailableFunds_003Eb__7(SimInvestment x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass16_2
	{
		public SimFund theFund;

		internal bool _003CInvestAvailableFunds_003Eb__8(SimInvestment x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass16_3
	{
		public SimStock stock;

		internal bool _003CInvestAvailableFunds_003Eb__13(SimInvestment x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass16_4
	{
		public SimBond theBond;

		internal bool _003CInvestAvailableFunds_003Eb__23(SimInvestment x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass16_5
	{
		public SimFund theFund;

		internal bool _003CInvestAvailableFunds_003Eb__27(SimInvestment x)
		{
			return false;
		}
	}

	protected SimFinancialAdvisor _advisor;

	protected double _totalInvestment;

	private Dictionary<SimStock, double> _tempStockInvestmentDict__BackingField;

	private Dictionary<SimBond, double> _tempBondInvestmentDict__BackingField;

	private Dictionary<SimFund, double> _tempFundInvestmentDict__BackingField;

	protected Dictionary<SimStock, double> _tempStockInvestmentDict
	{
		get
		{
			return _tempStockInvestmentDict__BackingField;
		}
		set
		{
			_tempStockInvestmentDict__BackingField = value;
		}
	}

	protected Dictionary<SimBond, double> _tempBondInvestmentDict
	{
		get
		{
			return _tempBondInvestmentDict__BackingField;
		}
		set
		{
			_tempBondInvestmentDict__BackingField = value;
		}
	}

	protected Dictionary<SimFund, double> _tempFundInvestmentDict
	{
		get
		{
			return _tempFundInvestmentDict__BackingField;
		}
		set
		{
			_tempFundInvestmentDict__BackingField = value;
		}
	}

	public ScenarioFinancialAdvisorInvestmentAllocation(SimFinancialAdvisor advisor, double totalInvestment)
	{
	}

	public override void RefreshScenario()
	{
	}

	private void InvestAvailableFunds(bool hasInvestedFunds)
	{
	}

	private int FundPerformance(SimFund fund)
	{
		return 0;
	}

	private List<double> DistributeAmounts(double total, int numItems)
	{
		return null;
	}

	public double SanitizeNumber(double number)
	{
		return 0.0;
	}

	private bool _003CInvestAvailableFunds_003Eb__16_24(SimFund x)
	{
		return false;
	}

	private bool _003CInvestAvailableFunds_003Eb__16_25(SimFund x)
	{
		return false;
	}

	private bool _003CInvestAvailableFunds_003Eb__16_26(SimFund x)
	{
		return false;
	}
}
