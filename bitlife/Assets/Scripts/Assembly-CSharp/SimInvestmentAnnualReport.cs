using System;

[Serializable]
public class SimInvestmentAnnualReport
{
	private SimInvestment Investment__BackingField;

	private double ReturnValue__BackingField;

	private double Quantity__BackingField;

	private double PerformanceDelta__BackingField;

	public SimInvestment Investment
	{
		get
		{
			return Investment__BackingField;
		}
		set
		{
			Investment__BackingField = value;
		}
	}

	public double ReturnValue
	{
		get
		{
			return ReturnValue__BackingField;
		}
		set
		{
			ReturnValue__BackingField = value;
		}
	}

	public double Quantity
	{
		get
		{
			return Quantity__BackingField;
		}
		set
		{
			Quantity__BackingField = value;
		}
	}

	public double PerformanceDelta
	{
		get
		{
			return PerformanceDelta__BackingField;
		}
		set
		{
			PerformanceDelta__BackingField = value;
		}
	}

	public SimInvestmentAnnualReport(SimInvestment investment)
	{
	}
}
