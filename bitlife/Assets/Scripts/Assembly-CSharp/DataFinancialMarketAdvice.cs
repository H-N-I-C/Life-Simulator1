using System.Collections;

public class DataFinancialMarketAdvice
{
	private string Feedback__BackingField;

	private int ProfessionalScore__BackingField;

	private bool Current__BackingField;

	private bool Future__BackingField;

	private ArrayList MarketTrend__BackingField;

	private ArrayList SectorTrend__BackingField;

	public string Feedback
	{
		get
		{
			return Feedback__BackingField;
		}
		set
		{
			Feedback__BackingField = value;
		}
	}

	public int ProfessionalScore
	{
		get
		{
			return ProfessionalScore__BackingField;
		}
		set
		{
			ProfessionalScore__BackingField = value;
		}
	}

	public bool Current
	{
		get
		{
			return Current__BackingField;
		}
		set
		{
			Current__BackingField = value;
		}
	}

	public bool Future
	{
		get
		{
			return Future__BackingField;
		}
		set
		{
			Future__BackingField = value;
		}
	}

	public ArrayList MarketTrend
	{
		get
		{
			return MarketTrend__BackingField;
		}
		set
		{
			MarketTrend__BackingField = value;
		}
	}

	public ArrayList SectorTrend
	{
		get
		{
			return SectorTrend__BackingField;
		}
		set
		{
			SectorTrend__BackingField = value;
		}
	}
}
