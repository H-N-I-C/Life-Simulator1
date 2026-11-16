using System;

[Serializable]
public class SimStockEvent
{
	private string Description__BackingField;

	private double StockPrice__BackingField;

	private bool GoesBankrupt__BackingField;

	public string Description
	{
		get
		{
			return Description__BackingField;
		}
		set
		{
			Description__BackingField = value;
		}
	}

	public double StockPrice
	{
		get
		{
			return StockPrice__BackingField;
		}
		set
		{
			StockPrice__BackingField = value;
		}
	}

	public bool GoesBankrupt
	{
		get
		{
			return GoesBankrupt__BackingField;
		}
		set
		{
			GoesBankrupt__BackingField = value;
		}
	}

	private SimStockEvent()
	{
	}

	public SimStockEvent(double price)
	{
	}
}
