using System;
using System.Collections.Generic;

[Serializable]
public class SimFund
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimStock, bool> _003C_003E9__35_6;

		internal bool _003CSetupFund_003Eb__35_6(SimStock x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass32_0
	{
		public string name;

		internal bool _003C_002Ector_003Eb__0(SimFund x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass35_0
	{
		public int i;

		internal bool _003CSetupFund_003Eb__0(SimStock x)
		{
			return false;
		}

		internal double _003CSetupFund_003Eb__1(SimStock x)
		{
			return 0.0;
		}

		internal bool _003CSetupFund_003Eb__2(SimStock x)
		{
			return false;
		}

		internal double _003CSetupFund_003Eb__3(SimStock x)
		{
			return 0.0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass35_1
	{
		public int i;

		internal bool _003CSetupFund_003Eb__4(SimStock x)
		{
			return false;
		}

		internal double _003CSetupFund_003Eb__5(SimStock x)
		{
			return 0.0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass35_2
	{
		public int i;

		internal bool _003CSetupFund_003Eb__7(SimStock x)
		{
			return false;
		}

		internal double _003CSetupFund_003Eb__8(SimStock x)
		{
			return 0.0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass35_3
	{
		public int i;

		internal bool _003CSetupFund_003Eb__10(SimStock x)
		{
			return false;
		}

		internal double _003CSetupFund_003Eb__11(SimStock x)
		{
			return 0.0;
		}
	}

	private string Name__BackingField;

	private int Type__BackingField;

	private float Performance__BackingField;

	private double BondDivision__BackingField;

	private double StockDivision__BackingField;

	private int Sector__BackingField;

	private int IndexFundType__BackingField;

	private List<double> AnnualValueList__BackingField;

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

	public int Type
	{
		get
		{
			return Type__BackingField;
		}
		set
		{
			Type__BackingField = value;
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

	public double BondDivision
	{
		get
		{
			return BondDivision__BackingField;
		}
		set
		{
			BondDivision__BackingField = value;
		}
	}

	public double StockDivision
	{
		get
		{
			return StockDivision__BackingField;
		}
		set
		{
			StockDivision__BackingField = value;
		}
	}

	public int Sector
	{
		get
		{
			return Sector__BackingField;
		}
		set
		{
			Sector__BackingField = value;
		}
	}

	public int IndexFundType
	{
		get
		{
			return IndexFundType__BackingField;
		}
		set
		{
			IndexFundType__BackingField = value;
		}
	}

	public List<double> AnnualValueList
	{
		get
		{
			return AnnualValueList__BackingField;
		}
		set
		{
			AnnualValueList__BackingField = value;
		}
	}

	public SimFund(double stockDivision)
	{
	}

	public SimFund(string name, int indexType, SimStockSector sector = null)
	{
	}

	public double CurrentFundValue()
	{
		return 0.0;
	}

	private void SetupFund()
	{
	}

	private bool _003CSetupFund_003Eb__35_9(SimStockSector x)
	{
		return false;
	}
}
