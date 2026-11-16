using System.Collections;

public class DataFinancialStockNameStructure
{
	private int StockSector__BackingField;

	private ArrayList Format__BackingField;

	private ArrayList Prefix__BackingField;

	private ArrayList Suffix__BackingField;

	public int StockSector
	{
		get
		{
			return StockSector__BackingField;
		}
		set
		{
			StockSector__BackingField = value;
		}
	}

	public ArrayList Format
	{
		get
		{
			return Format__BackingField;
		}
		set
		{
			Format__BackingField = value;
		}
	}

	public ArrayList Prefix
	{
		get
		{
			return Prefix__BackingField;
		}
		set
		{
			Prefix__BackingField = value;
		}
	}

	public ArrayList Suffix
	{
		get
		{
			return Suffix__BackingField;
		}
		set
		{
			Suffix__BackingField = value;
		}
	}
}
