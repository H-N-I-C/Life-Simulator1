using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SimInvestmentMarket
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<KeyValuePair<int, double>, double> _003C_003E9__46_0;

		public static Func<KeyValuePair<int, double>, double> _003C_003E9__46_1;

		public static Func<KeyValuePair<int, double>, double> _003C_003E9__46_2;

		public static Func<KeyValuePair<int, double>, double> _003C_003E9__46_3;

		public static Predicate<SimMarketEvent> _003C_003E9__50_1;

		public static Predicate<SimMarketEvent> _003C_003E9__50_2;

		public static Func<SimMarketEvent, bool> _003C_003E9__50_0;

		public static Func<SimFund, double> _003C_003E9__56_0;

		public static Func<SimStock, double> _003C_003E9__57_0;

		public static Func<SimStock, double> _003C_003E9__57_1;

		public static Func<SimStockSector, IEnumerable<SimStock>> _003C_003E9__58_0;

		public static Func<SimSectorEvent, bool> _003C_003E9__60_1;

		public static Func<SimSectorEvent, bool> _003C_003E9__60_2;

		public static Func<SimSectorEvent, bool> _003C_003E9__60_3;

		public static Func<SimStock, bool> _003C_003E9__61_0;

		public static Func<SimStockSector, bool> _003C_003E9__65_0;

		public static Func<SimStockSector, bool> _003C_003E9__65_1;

		public static Func<SimStockSector, bool> _003C_003E9__65_2;

		public static Func<SimStockSector, bool> _003C_003E9__65_3;

		public static Func<SimStockSector, bool> _003C_003E9__65_4;

		public static Func<SimStock, double> _003C_003E9__65_5;

		public static Func<SimSibling, int> _003C_003E9__66_0;

		public static Func<SimChild, float> _003C_003E9__66_1;

		public static Func<SimBond, bool> _003C_003E9__71_0;

		public static Func<SimBond, bool> _003C_003E9__71_1;

		public static Func<SimStockSector, bool> _003C_003E9__77_6;

		public static Func<SimStockSector, bool> _003C_003E9__77_7;

		public static Func<SimStockSector, bool> _003C_003E9__77_8;

		public static Func<SimStockSector, bool> _003C_003E9__77_9;

		public static Func<SimStockSector, bool> _003C_003E9__77_10;

		public static Func<SimStockSector, bool> _003C_003E9__77_0;

		public static Func<SimStockSector, bool> _003C_003E9__77_1;

		public static Func<SimStockSector, bool> _003C_003E9__77_2;

		public static Func<SimStockSector, bool> _003C_003E9__77_3;

		public static Func<SimStockSector, bool> _003C_003E9__77_4;

		public static Func<SimStockSector, bool> _003C_003E9__77_5;

		internal double _003CSetupInvestmentMarket_003Eb__46_0(KeyValuePair<int, double> x)
		{
			return 0.0;
		}

		internal double _003CSetupInvestmentMarket_003Eb__46_1(KeyValuePair<int, double> x)
		{
			return 0.0;
		}

		internal double _003CSetupInvestmentMarket_003Eb__46_2(KeyValuePair<int, double> x)
		{
			return 0.0;
		}

		internal double _003CSetupInvestmentMarket_003Eb__46_3(KeyValuePair<int, double> x)
		{
			return 0.0;
		}

		internal bool _003CSetupMarketLifecycle_003Eb__50_1(SimMarketEvent x)
		{
			return false;
		}

		internal bool _003CSetupMarketLifecycle_003Eb__50_2(SimMarketEvent x)
		{
			return false;
		}

		internal bool _003CSetupMarketLifecycle_003Eb__50_0(SimMarketEvent x)
		{
			return false;
		}

		internal double _003CSetFundPerformance_003Eb__56_0(SimFund x)
		{
			return 0.0;
		}

		internal double _003CGetSectorAnnualMovementType_003Eb__57_0(SimStock x)
		{
			return 0.0;
		}

		internal double _003CGetSectorAnnualMovementType_003Eb__57_1(SimStock x)
		{
			return 0.0;
		}

		internal IEnumerable<SimStock> _003CAllStockList_003Eb__58_0(SimStockSector x)
		{
			return null;
		}

		internal bool _003CSetupSectorLifecycle_003Eb__60_1(SimSectorEvent x)
		{
			return false;
		}

		internal bool _003CSetupSectorLifecycle_003Eb__60_2(SimSectorEvent x)
		{
			return false;
		}

		internal bool _003CSetupSectorLifecycle_003Eb__60_3(SimSectorEvent x)
		{
			return false;
		}

		internal bool _003CIncrementAge_003Eb__61_0(SimStock x)
		{
			return false;
		}

		internal bool _003CStockMarketHealth_003Eb__65_0(SimStockSector x)
		{
			return false;
		}

		internal bool _003CStockMarketHealth_003Eb__65_1(SimStockSector x)
		{
			return false;
		}

		internal bool _003CStockMarketHealth_003Eb__65_2(SimStockSector x)
		{
			return false;
		}

		internal bool _003CStockMarketHealth_003Eb__65_3(SimStockSector x)
		{
			return false;
		}

		internal bool _003CStockMarketHealth_003Eb__65_4(SimStockSector x)
		{
			return false;
		}

		internal double _003CStockMarketHealth_003Eb__65_5(SimStock x)
		{
			return 0.0;
		}

		internal int _003CGetEligibleAdvisorDict_003Eb__66_0(SimSibling x)
		{
			return 0;
		}

		internal float _003CGetEligibleAdvisorDict_003Eb__66_1(SimChild x)
		{
			return 0f;
		}

		internal bool _003CGenerateGovernmentBonds_003Eb__71_0(SimBond x)
		{
			return false;
		}

		internal bool _003CGenerateGovernmentBonds_003Eb__71_1(SimBond x)
		{
			return false;
		}

		internal bool _003CGetSectorForCompany_003Eb__77_6(SimStockSector x)
		{
			return false;
		}

		internal bool _003CGetSectorForCompany_003Eb__77_7(SimStockSector x)
		{
			return false;
		}

		internal bool _003CGetSectorForCompany_003Eb__77_8(SimStockSector x)
		{
			return false;
		}

		internal bool _003CGetSectorForCompany_003Eb__77_9(SimStockSector x)
		{
			return false;
		}

		internal bool _003CGetSectorForCompany_003Eb__77_10(SimStockSector x)
		{
			return false;
		}

		internal bool _003CGetSectorForCompany_003Eb__77_0(SimStockSector x)
		{
			return false;
		}

		internal bool _003CGetSectorForCompany_003Eb__77_1(SimStockSector x)
		{
			return false;
		}

		internal bool _003CGetSectorForCompany_003Eb__77_2(SimStockSector x)
		{
			return false;
		}

		internal bool _003CGetSectorForCompany_003Eb__77_3(SimStockSector x)
		{
			return false;
		}

		internal bool _003CGetSectorForCompany_003Eb__77_4(SimStockSector x)
		{
			return false;
		}

		internal bool _003CGetSectorForCompany_003Eb__77_5(SimStockSector x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass46_0
	{
		public SimStockSector sector;

		internal bool _003CSetupInvestmentMarket_003Eb__4(DataFinancialUniversalStock x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass46_1
	{
		public DataFinancialUniversalStock randomStockData;

		internal bool _003CSetupInvestmentMarket_003Eb__5(SimStock x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass46_2
	{
		public DataFinancialUniversalStock randomStockData;

		internal bool _003CSetupInvestmentMarket_003Eb__6(SimStock x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass46_3
	{
		public int i;

		public Func<SimStock, double> _003C_003E9__8;

		internal double _003CSetupInvestmentMarket_003Eb__7(SimStockSector x)
		{
			return 0.0;
		}

		internal double _003CSetupInvestmentMarket_003Eb__8(SimStock y)
		{
			return 0.0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass48_0
	{
		public DataFinancialCryptocurrency randomData;

		internal bool _003CSetupCrypto_003Eb__0(SimCrypto x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass51_0
	{
		public string name;

		internal bool _003CGetStockForName_003Eb__0(SimStock x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass52_0
	{
		public string ID;

		internal bool _003CGetBondForID_003Eb__0(SimBond x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass53_0
	{
		public string name;

		internal bool _003CGetFundForName_003Eb__0(SimFund x)
		{
			return false;
		}

		internal bool _003CGetFundForName_003Eb__1(SimFund x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass54_0
	{
		public string name;

		internal bool _003CGetCryptoForName_003Eb__0(SimCrypto x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass60_0
	{
		public STOCK_SECTOR index;

		internal bool _003CSetupSectorLifecycle_003Eb__0(SimStockSector x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass61_0
	{
		public SimCrypto crypto;

		internal bool _003CIncrementAge_003Eb__1(SimInvestment x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass61_1
	{
		public DataFinancialCryptocurrency randomData;

		internal bool _003CIncrementAge_003Eb__2(SimCrypto x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass65_0
	{
		public int i;

		internal double _003CStockMarketHealth_003Eb__6(SimStock x)
		{
			return 0.0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass66_0
	{
		public List<SimPerson> friendList;

		internal bool _003CGetEligibleAdvisorDict_003Eb__2(SimPerson x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass71_0
	{
		public DataCountry tempCountry;

		internal bool _003CGenerateGovernmentBonds_003Eb__2(SimBond x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass71_1
	{
		public DataCountry tempCountry;

		internal bool _003CGenerateGovernmentBonds_003Eb__3(SimBond x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass71_2
	{
		public DataCountry tempCountry;

		internal bool _003CGenerateGovernmentBonds_003Eb__4(SimBond x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass73_0
	{
		public string ticker;

		internal bool _003CDoesStockTickerExist_003Eb__0(SimStock x)
		{
			return false;
		}
	}

	private const int MaxValidAge = 148;

	private bool HasSetupInvestmentMarket__BackingField;

	private List<SimMarketEvent> _MarketEventsList;

	private List<SimStockSector> _SectorList;

	private Dictionary<int, double> _FundRateDict;

	private List<SimBond> _AvailableBonds;

	private List<SimBond> _ActiveBonds;

	private List<SimFund> _ActiveFunds;

	private List<SimFund> _IndexFunds;

	private List<SimCrypto> _CryptoList;

	private int HeroAgeAtLastRefreshHeadlines__BackingField;

	private List<string> _HeadlineArray;

	public bool HasSetupInvestmentMarket
	{
		get
		{
			return HasSetupInvestmentMarket__BackingField;
		}
		set
		{
			HasSetupInvestmentMarket__BackingField = value;
		}
	}

	public List<SimMarketEvent> MarketEventsList
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public List<SimStockSector> SectorList
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public Dictionary<int, double> FundRateDict
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public List<SimBond> AvailableBonds
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public List<SimBond> ActiveBonds
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public List<SimFund> ActiveFunds
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public List<SimFund> IndexFunds
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public List<SimCrypto> CryptoList
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int HeroAgeAtLastRefreshHeadlines
	{
		get
		{
			return HeroAgeAtLastRefreshHeadlines__BackingField;
		}
		set
		{
			HeroAgeAtLastRefreshHeadlines__BackingField = value;
		}
	}

	public List<string> HeadlineArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public void SetupInvestmentMarket()
	{
	}

	private bool IsAlreadySetup()
	{
		return false;
	}

	private void SetupCrypto()
	{
	}

	private void SetupFunds()
	{
	}

	private void SetupMarketLifecycle()
	{
	}

	public SimStock GetStockForName(string name)
	{
		return null;
	}

	public SimBond GetBondForID(string ID)
	{
		return null;
	}

	public SimFund GetFundForName(string name)
	{
		return null;
	}

	public SimCrypto GetCryptoForName(string name)
	{
		return null;
	}

	public void SetCryptoTrendiness()
	{
	}

	public void SetFundPerformance()
	{
	}

	public int GetSectorAnnualMovementType(SimStockSector sector)
	{
		return 0;
	}

	public List<SimStock> AllStockList()
	{
		return null;
	}

	public SimStock GetStockDataForPerformance(bool stockDoesGood)
	{
		return null;
	}

	private void SetupSectorLifecycle()
	{
	}

	public void IncrementAge()
	{
	}

	public void LiveUpdateAllStocks()
	{
	}

	public double BondEarlySellPrice(SimInvestment asset)
	{
		return 0.0;
	}

	public float BondMarketHealth()
	{
		return 0f;
	}

	public float StockMarketHealth()
	{
		return 0f;
	}

	public Dictionary<SimPerson, string> GetEligibleAdvisorDict()
	{
		return null;
	}

	public void RefreshBonds()
	{
	}

	private List<SimBond> GenerateCorporateBonds()
	{
		return null;
	}

	private void GenerateLocalBonds()
	{
	}

	private void CreateBondList(string entity, int level, DataCountry country = null, SimStock stock = null)
	{
	}

	private List<SimBond> GenerateGovernmentBonds()
	{
		return null;
	}

	public string GenerateTickerForName(string name)
	{
		return null;
	}

	public bool DoesStockTickerExist(string ticker)
	{
		return false;
	}

	private ArrayList PossibleMaturityArray()
	{
		return null;
	}

	public string GetStockDeltaString(string listItemTitle)
	{
		return null;
	}

	public SimPerson GetEligiblePersonForTip(bool isStockTip = true)
	{
		return null;
	}

	public static SimStockSector GetSectorForCompany(SimCompany company)
	{
		return null;
	}

	public static double GetBondCapForLevel(int level)
	{
		return 0.0;
	}

	public static double GetStockValueCap(bool pennyStock)
	{
		return 0.0;
	}

	public static double GetActiveFundsCap()
	{
		return 0.0;
	}

	public static double GetCryptoQuantityIncrement()
	{
		return 0.0;
	}

	public static int ClampValidHeroAge()
	{
		return 0;
	}

	private static int ActualHeroAge()
	{
		return 0;
	}

	private static bool IsHeroAgeInRange()
	{
		return false;
	}
}
