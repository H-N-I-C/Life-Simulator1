using System;
using System.Collections.Generic;

[Serializable]
public class SimCrypto
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<int, int> _003C_003E9__30_1;

		public static Func<SimCryptoEvent, bool> _003C_003E9__30_2;

		internal int _003CSetupCryptoLifecycle_003Eb__30_1(int x)
		{
			return 0;
		}

		internal bool _003CSetupCryptoLifecycle_003Eb__30_2(SimCryptoEvent x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass30_0
	{
		public int i;

		internal bool _003CSetupCryptoLifecycle_003Eb__0(int x)
		{
			return false;
		}
	}

	private string Name__BackingField;

	private int LifecycleType__BackingField;

	private double Trendiness__BackingField;

	private double StartingPrice__BackingField;

	private List<SimCryptoEvent> CryptoLifecycleDict__BackingField;

	private Dictionary<int, List<double>> MonthlyValuesList__BackingField;

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

	public DataFinancialCryptocurrency Data
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int LifecycleType
	{
		get
		{
			return LifecycleType__BackingField;
		}
		set
		{
			LifecycleType__BackingField = value;
		}
	}

	public double Trendiness
	{
		get
		{
			return Trendiness__BackingField;
		}
		set
		{
			Trendiness__BackingField = value;
		}
	}

	public double StartingPrice
	{
		get
		{
			return StartingPrice__BackingField;
		}
		set
		{
			StartingPrice__BackingField = value;
		}
	}

	public List<SimCryptoEvent> CryptoLifecycleDict
	{
		get
		{
			return CryptoLifecycleDict__BackingField;
		}
		set
		{
			CryptoLifecycleDict__BackingField = value;
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

	public SimCrypto(DataFinancialCryptocurrency data, int type)
	{
	}

	public void IncrementAge()
	{
	}

	private List<double> GetMonthlyValueList(int index)
	{
		return null;
	}

	public void SetupCryptoLifecycle()
	{
	}

	public double GetCurrentPrice()
	{
		return 0.0;
	}

	public double TrendScore()
	{
		return 0.0;
	}

	private List<int> GetPeakAgeList()
	{
		return null;
	}
}
