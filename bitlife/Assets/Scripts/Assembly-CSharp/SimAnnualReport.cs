using System;
using System.Collections.Generic;

[Serializable]
public class SimAnnualReport
{
	private string Name__BackingField;

	private string CEOName__BackingField;

	private string CEOMetaHeroID__BackingField;

	private int FiscalYear__BackingField;

	private double Revenue__BackingField;

	private double Costs__BackingField;

	private Dictionary<SimProduct, double> ProductionDict__BackingField;

	private Dictionary<SimProduct, float> ProductCustomerRating__BackingField;

	private double NetIncome__BackingField;

	private double CookedNetIncomeAmount__BackingField;

	private int UnitSales__BackingField;

	private float Performance__BackingField;

	private Dictionary<SimProduct, KeyValuePair<int, double>> ProductsSoldList__BackingField;

	private Dictionary<SimProduct, double> ProductCostDict__BackingField;

	public string Name
	{
		get
		{
			return Name__BackingField;
		}
		set
		{
			Name__BackingField = value;
		}
	}

	public string CEOName
	{
		get
		{
			return CEOName__BackingField;
		}
		set
		{
			CEOName__BackingField = value;
		}
	}

	public string CEOMetaHeroID
	{
		get
		{
			return CEOMetaHeroID__BackingField;
		}
		set
		{
			CEOMetaHeroID__BackingField = value;
		}
	}

	public int FiscalYear
	{
		get
		{
			return FiscalYear__BackingField;
		}
		set
		{
			FiscalYear__BackingField = value;
		}
	}

	public double Revenue
	{
		get
		{
			return Revenue__BackingField;
		}
		set
		{
			Revenue__BackingField = value;
		}
	}

	public double Costs
	{
		get
		{
			return Costs__BackingField;
		}
		set
		{
			Costs__BackingField = value;
		}
	}

	public Dictionary<SimProduct, double> ProductionDict
	{
		get
		{
			return ProductionDict__BackingField;
		}
		set
		{
			ProductionDict__BackingField = value;
		}
	}

	public Dictionary<SimProduct, float> ProductCustomerRating
	{
		get
		{
			return ProductCustomerRating__BackingField;
		}
		set
		{
			ProductCustomerRating__BackingField = value;
		}
	}

	public double NetIncome
	{
		get
		{
			return NetIncome__BackingField;
		}
		set
		{
			NetIncome__BackingField = value;
		}
	}

	public double CookedNetIncomeAmount
	{
		get
		{
			return CookedNetIncomeAmount__BackingField;
		}
		set
		{
			CookedNetIncomeAmount__BackingField = value;
		}
	}

	public int UnitSales
	{
		get
		{
			return UnitSales__BackingField;
		}
		set
		{
			UnitSales__BackingField = value;
		}
	}

	public float Performance
	{
		get
		{
			return Performance__BackingField;
		}
		set
		{
			Performance__BackingField = value;
		}
	}

	public Dictionary<SimProduct, KeyValuePair<int, double>> ProductsSoldList
	{
		get
		{
			return ProductsSoldList__BackingField;
		}
		set
		{
			ProductsSoldList__BackingField = value;
		}
	}

	public Dictionary<SimProduct, double> ProductCostDict
	{
		get
		{
			return ProductCostDict__BackingField;
		}
		set
		{
			ProductCostDict__BackingField = value;
		}
	}
}
