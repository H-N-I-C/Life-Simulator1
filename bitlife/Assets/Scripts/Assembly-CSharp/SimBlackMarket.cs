using System;
using System.Collections.Generic;

[Serializable]
public class SimBlackMarket
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimBlackMarketVendor, int> _003C_003E9__13_0;

		internal int _003CIncrementAge_003Eb__13_0(SimBlackMarketVendor x)
		{
			return 0;
		}
	}

	private List<SimBlackMarketVendor> VendorList__BackingField;

	private int NumTimesEnteredBlackMarket__BackingField;

	private int NumTimesGotCaughtBlackMarket__BackingField;

	public List<SimBlackMarketVendor> VendorList
	{
		get
		{
			return VendorList__BackingField;
		}
		set
		{
			VendorList__BackingField = value;
		}
	}

	public int NumTimesEnteredBlackMarket
	{
		get
		{
			return NumTimesEnteredBlackMarket__BackingField;
		}
		set
		{
			NumTimesEnteredBlackMarket__BackingField = value;
		}
	}

	public int NumTimesGotCaughtBlackMarket
	{
		get
		{
			return NumTimesGotCaughtBlackMarket__BackingField;
		}
		set
		{
			NumTimesGotCaughtBlackMarket__BackingField = value;
		}
	}

	public SimBlackMarket(List<int> listAttitude)
	{
	}

	public void IncrementAge()
	{
	}

	public static void CancelAllBlackMarketScenarios()
	{
	}

	public void ImpactAllVendorAttitudes()
	{
	}
}
