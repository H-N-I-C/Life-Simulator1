using System;
using System.Collections.Generic;

[Serializable]
public class SimPortfolio
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimInvestment, double> _003C_003E9__44_0;

		public static Func<SimInvestment, double> _003C_003E9__45_0;

		public static Func<SimInvestment, double> _003C_003E9__46_0;

		internal double _003CGetCurrentPortfolioReturn_003Eb__44_0(SimInvestment x)
		{
			return 0.0;
		}

		internal double _003CGetPortfolioValue_003Eb__45_0(SimInvestment x)
		{
			return 0.0;
		}

		internal double _003CGetPortfolioCostBasis_003Eb__46_0(SimInvestment x)
		{
			return 0.0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass42_0
	{
		public SimStock stock;

		internal bool _003CGetInvestmentForStock_003Eb__0(SimInvestment x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass43_0
	{
		public SimBond bond;

		internal bool _003CGetInvestmentForBond_003Eb__0(SimInvestment x)
		{
			return false;
		}
	}

	private SimFinancialAdvisor FinancialAdvisor__BackingField;

	private List<SimInvestment> Assets__BackingField;

	private Dictionary<int, SimPortfolioAnnualReport> AnnualReportDict__BackingField;

	private int HeroAgeAtStartedInvesting__BackingField;

	private int HeroAgeAtLastFailedToFindFinancialAdvisor__BackingField;

	private int NumTimesRejectedFinancialAdvisorAtCurrentYear__BackingField;

	private double StockProfit__BackingField;

	private double CryptoProfit__BackingField;

	private double AnnualCost__BackingField;

	private double AnnualSaleValue__BackingField;

	public SimFinancialAdvisor FinancialAdvisor
	{
		get
		{
			return FinancialAdvisor__BackingField;
		}
		set
		{
			FinancialAdvisor__BackingField = value;
		}
	}

	public List<SimInvestment> Assets
	{
		get
		{
			return Assets__BackingField;
		}
		set
		{
			Assets__BackingField = value;
		}
	}

	public Dictionary<int, SimPortfolioAnnualReport> AnnualReportDict
	{
		get
		{
			return AnnualReportDict__BackingField;
		}
		set
		{
			AnnualReportDict__BackingField = value;
		}
	}

	public int HeroAgeAtStartedInvesting
	{
		get
		{
			return HeroAgeAtStartedInvesting__BackingField;
		}
		set
		{
			HeroAgeAtStartedInvesting__BackingField = value;
		}
	}

	public int HeroAgeAtLastFailedToFindFinancialAdvisor
	{
		get
		{
			return HeroAgeAtLastFailedToFindFinancialAdvisor__BackingField;
		}
		set
		{
			HeroAgeAtLastFailedToFindFinancialAdvisor__BackingField = value;
		}
	}

	public int NumTimesRejectedFinancialAdvisorAtCurrentYear
	{
		get
		{
			return NumTimesRejectedFinancialAdvisorAtCurrentYear__BackingField;
		}
		set
		{
			NumTimesRejectedFinancialAdvisorAtCurrentYear__BackingField = value;
		}
	}

	public double StockProfit
	{
		get
		{
			return StockProfit__BackingField;
		}
		set
		{
			StockProfit__BackingField = value;
		}
	}

	public double CryptoProfit
	{
		get
		{
			return CryptoProfit__BackingField;
		}
		set
		{
			CryptoProfit__BackingField = value;
		}
	}

	public double AnnualCost
	{
		get
		{
			return AnnualCost__BackingField;
		}
		set
		{
			AnnualCost__BackingField = value;
		}
	}

	public double AnnualSaleValue
	{
		get
		{
			return AnnualSaleValue__BackingField;
		}
		set
		{
			AnnualSaleValue__BackingField = value;
		}
	}

	public void IncrementAge()
	{
	}

	public SimInvestment GetInvestmentForStock(SimStock stock)
	{
		return null;
	}

	public SimInvestment GetInvestmentForBond(SimBond bond)
	{
		return null;
	}

	public double GetCurrentPortfolioReturn()
	{
		return 0.0;
	}

	public double GetPortfolioValue()
	{
		return 0.0;
	}

	public double GetPortfolioCostBasis()
	{
		return 0.0;
	}

	public double LiquidateAllAssets()
	{
		return 0.0;
	}
}
