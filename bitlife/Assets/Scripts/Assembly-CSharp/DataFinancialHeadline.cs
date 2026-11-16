using System.Collections;

public class DataFinancialHeadline
{
	private string Headline__BackingField;

	private bool Current__BackingField;

	private bool Future__BackingField;

	private ArrayList MarketTrend__BackingField;

	private ArrayList SectorTrend__BackingField;

	private ArrayList AssetTrend__BackingField;

	public string Headline
	{
		get
		{
			return Headline__BackingField;
		}
		set
		{
			Headline__BackingField = value;
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

	public ArrayList AssetTrend
	{
		get
		{
			return AssetTrend__BackingField;
		}
		set
		{
			AssetTrend__BackingField = value;
		}
	}
}
