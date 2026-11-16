using System;
using System.Collections.Generic;

[Serializable]
public class SimPortfolioAnnualReport
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimInvestmentAnnualReport, double> _003C_003E9__29_0;

		public static Func<SimInvestmentAnnualReport, double> _003C_003E9__30_0;

		internal double _003CBestPerformingAssetString_003Eb__29_0(SimInvestmentAnnualReport x)
		{
			return 0.0;
		}

		internal double _003CWorstPerformingAssetString_003Eb__30_0(SimInvestmentAnnualReport x)
		{
			return 0.0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass31_0
	{
		public SimInvestment investment;

		internal bool _003CGetAnnualPortfolioReturn_003Eb__0(SimInvestmentAnnualReport x)
		{
			return false;
		}
	}

	private double PortfolioValue__BackingField;

	private double PortfolioReturnAmount__BackingField;

	private double TotalCost__BackingField;

	private double TotalGainedValue__BackingField;

	private int HeroAge__BackingField;

	private string ReportColorString__BackingField;

	private List<SimInvestmentAnnualReport> InvestmentReportDict__BackingField;

	public double PortfolioValue
	{
		get
		{
			return PortfolioValue__BackingField;
		}
		set
		{
			PortfolioValue__BackingField = value;
		}
	}

	public double PortfolioReturnAmount
	{
		get
		{
			return PortfolioReturnAmount__BackingField;
		}
		set
		{
			PortfolioReturnAmount__BackingField = value;
		}
	}

	public double TotalCost
	{
		get
		{
			return TotalCost__BackingField;
		}
		set
		{
			TotalCost__BackingField = value;
		}
	}

	public double TotalGainedValue
	{
		get
		{
			return TotalGainedValue__BackingField;
		}
		set
		{
			TotalGainedValue__BackingField = value;
		}
	}

	public int HeroAge
	{
		get
		{
			return HeroAge__BackingField;
		}
		set
		{
			HeroAge__BackingField = value;
		}
	}

	public string ReportColorString
	{
		get
		{
			return ReportColorString__BackingField;
		}
		set
		{
			ReportColorString__BackingField = value;
		}
	}

	public List<SimInvestmentAnnualReport> InvestmentReportDict
	{
		get
		{
			return InvestmentReportDict__BackingField;
		}
		set
		{
			InvestmentReportDict__BackingField = value;
		}
	}

	public SimPortfolioAnnualReport(double cost, double shortGains)
	{
	}

	public string BestPerformingAssetString()
	{
		return null;
	}

	public string WorstPerformingAssetString()
	{
		return null;
	}

	public double GetAnnualPortfolioReturn()
	{
		return 0.0;
	}
}
