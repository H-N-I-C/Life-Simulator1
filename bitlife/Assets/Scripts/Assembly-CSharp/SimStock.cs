using System;
using System.Collections.Generic;

[Serializable]
public class SimStock
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimStock, bool> _003C_003E9__49_0;

		public static Func<SimStock, bool> _003C_003E9__49_1;

		internal bool _003C_002Ector_003Eb__49_0(SimStock x)
		{
			return false;
		}

		internal bool _003C_002Ector_003Eb__49_1(SimStock x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass55_0
	{
		public DataFinancialUniversalStock stock;

		internal bool _003CHandleBankruptcy_003Eb__2(SimStock x)
		{
			return false;
		}
	}

	private string _Symbol;

	private string Name__BackingField;

	private string Ticker__BackingField;

	private bool IsPennyStock__BackingField;

	private double CurrentStockPrice__BackingField;

	private double LastSeenStockPrice__BackingField;

	private Dictionary<int, double> AnnualPlayerSideValueList__BackingField;

	private Dictionary<int, List<double>> MonthlyValuesList__BackingField;

	private List<SimStockEvent> StockLifecycleList__BackingField;

	private int LifecyclePattern__BackingField;

	private int PennyStockSector__BackingField;

	private string CountryCode__BackingField;

	public DataFinancialUniversalStock StockData
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

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

	public string Ticker
	{
		get
		{
			return Ticker__BackingField;
		}
		set
		{
			Ticker__BackingField = value;
		}
	}

	public bool IsPennyStock
	{
		get
		{
			return IsPennyStock__BackingField;
		}
		set
		{
			IsPennyStock__BackingField = value;
		}
	}

	public double CurrentStockPrice
	{
		get
		{
			return CurrentStockPrice__BackingField;
		}
		set
		{
			CurrentStockPrice__BackingField = value;
		}
	}

	public double LastSeenStockPrice
	{
		get
		{
			return LastSeenStockPrice__BackingField;
		}
		set
		{
			LastSeenStockPrice__BackingField = value;
		}
	}

	public Dictionary<int, double> AnnualPlayerSideValueList
	{
		get
		{
			return AnnualPlayerSideValueList__BackingField;
		}
		set
		{
			AnnualPlayerSideValueList__BackingField = value;
		}
	}

	public Dictionary<int, List<double>> MonthlyValuesList
	{
		get
		{
			return MonthlyValuesList__BackingField;
		}
		set
		{
			MonthlyValuesList__BackingField = value;
		}
	}

	public List<SimStockEvent> StockLifecycleList
	{
		get
		{
			return StockLifecycleList__BackingField;
		}
		set
		{
			StockLifecycleList__BackingField = value;
		}
	}

	public int LifecyclePattern
	{
		get
		{
			return LifecyclePattern__BackingField;
		}
		set
		{
			LifecyclePattern__BackingField = value;
		}
	}

	public int PennyStockSector
	{
		get
		{
			return PennyStockSector__BackingField;
		}
		set
		{
			PennyStockSector__BackingField = value;
		}
	}

	public string CountryCode
	{
		get
		{
			return CountryCode__BackingField;
		}
		set
		{
			CountryCode__BackingField = value;
		}
	}

	public SimStock(DataFinancialUniversalStock stockData)
	{
	}

	public SimStock(SimCompany company, SimStockSector sector)
	{
	}

	public SimStock(DataFinancialUniversalStock stockData, int overrideLifecyclePattern)
	{
	}

	public int GetSectorForStock()
	{
		return 0;
	}

	public void SetupStockLifecycle(SimInvestmentMarket market = null, bool isPennyStock = false)
	{
	}

	public List<double> GetMonthlyValueList(int index)
	{
		return null;
	}

	public void IncrementAge()
	{
	}

	public void HandleBankruptcy()
	{
	}

	public void SetNextLiveValue()
	{
	}

	public double MaxPrice()
	{
		return 0.0;
	}

	public double MinPrice()
	{
		return 0.0;
	}

	public double GetPriceForStockAge(int age)
	{
		return 0.0;
	}

	private void SetupLifecyclePattern(int winnerScore)
	{
	}

	public bool WillGoBankrupt()
	{
		return false;
	}

	private bool _003CGetSectorForStock_003Eb__51_0(SimStockSector x)
	{
		return false;
	}

	private bool _003CSetupStockLifecycle_003Eb__52_0(SimStockSector x)
	{
		return false;
	}

	private bool _003CHandleBankruptcy_003Eb__55_0(SimStockSector x)
	{
		return false;
	}

	private bool _003CHandleBankruptcy_003Eb__55_1(SimInvestment x)
	{
		return false;
	}
}
