using System;
using System.Collections.Generic;

[Serializable]
public class SimBusiness
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimProduct, double> _003C_003E9__223_0;

		public static Func<SimProduct, bool> _003C_003E9__223_2;

		public static Func<SimProduct, double> _003C_003E9__223_1;

		public static Func<SimProduct, double> _003C_003E9__226_0;

		public static Func<SimProduct, double> _003C_003E9__226_1;

		public static Func<SimProduct, double> _003C_003E9__226_2;

		public static Func<SimProduct, bool> _003C_003E9__232_0;

		public static Func<SimEmployee, int> _003C_003E9__232_1;

		public static Func<KeyValuePair<SimProduct, double>, double> _003C_003E9__252_0;

		public static Func<SimProduct, float> _003C_003E9__254_0;

		internal double _003C_002Ector_003Eb__223_0(SimProduct x)
		{
			return 0.0;
		}

		internal bool _003C_002Ector_003Eb__223_2(SimProduct x)
		{
			return false;
		}

		internal double _003C_002Ector_003Eb__223_1(SimProduct x)
		{
			return 0.0;
		}

		internal double _003CCreateAnnualReport_003Eb__226_0(SimProduct x)
		{
			return 0.0;
		}

		internal double _003CCreateAnnualReport_003Eb__226_1(SimProduct x)
		{
			return 0.0;
		}

		internal double _003CCreateAnnualReport_003Eb__226_2(SimProduct x)
		{
			return 0.0;
		}

		internal bool _003CIncrementAge_003Eb__232_0(SimProduct x)
		{
			return false;
		}

		internal int _003CIncrementAge_003Eb__232_1(SimEmployee x)
		{
			return 0;
		}

		internal double _003CGetHighestRevenueProduct_003Eb__252_0(KeyValuePair<SimProduct, double> x)
		{
			return 0.0;
		}

		internal float _003CCalculateStartupCosts_003Eb__254_0(SimProduct x)
		{
			return 0f;
		}
	}

	private sealed class _003C_003Ec__DisplayClass232_0
	{
		public DataEmployeeType employeeType;

		internal bool _003CIncrementAge_003Eb__2(SimEmployee x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass232_1
	{
		public string employeeTitle;

		internal bool _003CIncrementAge_003Eb__3(string x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass232_2
	{
		public KeyValuePair<string, int> kvp;

		internal bool _003CIncrementAge_003Eb__4(SimEmployee x)
		{
			return false;
		}
	}

	private string Name__BackingField;

	private int CurrentFiscalYear__BackingField;

	private int IndustryEnum__BackingField;

	private List<SimProduct> PossibleProductList__BackingField;

	private List<SimProduct> ActiveProductsList__BackingField;

	private List<SimAnnualReport> AnnualReports__BackingField;

	private List<SimCompetitor> CompetitorList__BackingField;

	private float MiscAnnualCosts__BackingField;

	private int YearsInPosition__BackingField;

	private int HeroAgeWhenStartedBusiness__BackingField;

	private float HeroSalary__BackingField;

	private double NetIncome__BackingField;

	private double BankBalance__BackingField;

	private int CompensationPercentage__BackingField;

	private int NumFacilities__BackingField;

	private List<SimEmployee> KeyEmployeeList__BackingField;

	private List<SimEmployee> ExEmployeeList__BackingField;

	private List<string> UniqueEmployeeCombinationList__BackingField;

	private Dictionary<string, string> PreviousCEODict__BackingField;

	private int NumEmployees__BackingField;

	private float IndustryDemand__BackingField;

	private float IndustrySupply__BackingField;

	private double InitialInvestment__BackingField;

	private double ExternalInvestment__BackingField;

	private double TotalInvestment__BackingField;

	private bool IsStartup__BackingField;

	private bool IsAcquired__BackingField;

	private double AcquiredCompanyRevenue__BackingField;

	private double AcquiredCompanyNetIncome__BackingField;

	private double AcquiredCompanySellPrice__BackingField;

	private double JobOfferRevenue__BackingField;

	private double JobOfferCosts__BackingField;

	private double JobOfferNetIncome__BackingField;

	private double MaxInvestmentCap__BackingField;

	private double SellValueIndex__BackingField;

	private double TotalSellPrice__BackingField;

	private int AgeAtLastSellAttempt__BackingField;

	private int AgeAtLastGaveSelfBonus__BackingField;

	private int AgeAtLastVirality__BackingField;

	private int AgeAtLastSupplierTermination__BackingField;

	private int NumTimesLookedForCandidate__BackingField;

	private int NumTimesLostLawsuit__BackingField;

	private float Att_Performance__BackingField;

	private int WorkforceRating__BackingField;

	private int TotalNumberOfBonuses__BackingField;

	private double BonusesTotal__BackingField;

	private int TotalNumberOfBoardRaises__BackingField;

	private int AgeAtLastBoardBonus__BackingField;

	private int AgeAtLastBoardRaise__BackingField;

	private float CompanyPerformanceBonusGenerosity__BackingField;

	private int AgeAtBankruptcy__BackingField;

	private int AgeAtSoldCompany__BackingField;

	private double TotalSellProfit__BackingField;

	private int NumTimesVisitedConsultantThisYear__BackingField;

	private int NumTimesCookedBooks__BackingField;

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

	public int CurrentFiscalYear
	{
		get
		{
			return CurrentFiscalYear__BackingField;
		}
		set
		{
			CurrentFiscalYear__BackingField = value;
		}
	}

	public DataBusinessIndustry Industry
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int IndustryEnum
	{
		get
		{
			return IndustryEnum__BackingField;
		}
		set
		{
			IndustryEnum__BackingField = value;
		}
	}

	public List<SimProduct> PossibleProductList
	{
		get
		{
			return PossibleProductList__BackingField;
		}
		set
		{
			PossibleProductList__BackingField = value;
		}
	}

	public List<SimProduct> ActiveProductsList
	{
		get
		{
			return ActiveProductsList__BackingField;
		}
		set
		{
			ActiveProductsList__BackingField = value;
		}
	}

	public List<SimAnnualReport> AnnualReports
	{
		get
		{
			return AnnualReports__BackingField;
		}
		set
		{
			AnnualReports__BackingField = value;
		}
	}

	public List<SimCompetitor> CompetitorList
	{
		get
		{
			return CompetitorList__BackingField;
		}
		set
		{
			CompetitorList__BackingField = value;
		}
	}

	public float MiscAnnualCosts
	{
		get
		{
			return MiscAnnualCosts__BackingField;
		}
		set
		{
			MiscAnnualCosts__BackingField = value;
		}
	}

	public int YearsInPosition
	{
		get
		{
			return YearsInPosition__BackingField;
		}
		set
		{
			YearsInPosition__BackingField = value;
		}
	}

	public int HeroAgeWhenStartedBusiness
	{
		get
		{
			return HeroAgeWhenStartedBusiness__BackingField;
		}
		set
		{
			HeroAgeWhenStartedBusiness__BackingField = value;
		}
	}

	public float HeroSalary
	{
		get
		{
			return HeroSalary__BackingField;
		}
		set
		{
			HeroSalary__BackingField = value;
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

	public double BankBalance
	{
		get
		{
			return BankBalance__BackingField;
		}
		set
		{
			BankBalance__BackingField = value;
		}
	}

	public int CompensationPercentage
	{
		get
		{
			return CompensationPercentage__BackingField;
		}
		set
		{
			CompensationPercentage__BackingField = value;
		}
	}

	public int NumFacilities
	{
		get
		{
			return NumFacilities__BackingField;
		}
		set
		{
			NumFacilities__BackingField = value;
		}
	}

	public List<SimEmployee> KeyEmployeeList
	{
		get
		{
			return KeyEmployeeList__BackingField;
		}
		set
		{
			KeyEmployeeList__BackingField = value;
		}
	}

	public List<SimEmployee> ExEmployeeList
	{
		get
		{
			return ExEmployeeList__BackingField;
		}
		set
		{
			ExEmployeeList__BackingField = value;
		}
	}

	public List<string> UniqueEmployeeCombinationList
	{
		get
		{
			return UniqueEmployeeCombinationList__BackingField;
		}
		set
		{
			UniqueEmployeeCombinationList__BackingField = value;
		}
	}

	public Dictionary<string, string> PreviousCEODict
	{
		get
		{
			return PreviousCEODict__BackingField;
		}
		set
		{
			PreviousCEODict__BackingField = value;
		}
	}

	public int NumEmployees
	{
		get
		{
			return NumEmployees__BackingField;
		}
		set
		{
			NumEmployees__BackingField = value;
		}
	}

	public float IndustryDemand
	{
		get
		{
			return IndustryDemand__BackingField;
		}
		set
		{
			IndustryDemand__BackingField = value;
		}
	}

	public float IndustrySupply
	{
		get
		{
			return IndustrySupply__BackingField;
		}
		set
		{
			IndustrySupply__BackingField = value;
		}
	}

	public double InitialInvestment
	{
		get
		{
			return InitialInvestment__BackingField;
		}
		set
		{
			InitialInvestment__BackingField = value;
		}
	}

	public double ExternalInvestment
	{
		get
		{
			return ExternalInvestment__BackingField;
		}
		set
		{
			ExternalInvestment__BackingField = value;
		}
	}

	public double TotalInvestment
	{
		get
		{
			return TotalInvestment__BackingField;
		}
		set
		{
			TotalInvestment__BackingField = value;
		}
	}

	public bool IsStartup
	{
		get
		{
			return IsStartup__BackingField;
		}
		set
		{
			IsStartup__BackingField = value;
		}
	}

	public bool IsAcquired
	{
		get
		{
			return IsAcquired__BackingField;
		}
		set
		{
			IsAcquired__BackingField = value;
		}
	}

	public double AcquiredCompanyRevenue
	{
		get
		{
			return AcquiredCompanyRevenue__BackingField;
		}
		set
		{
			AcquiredCompanyRevenue__BackingField = value;
		}
	}

	public double AcquiredCompanyNetIncome
	{
		get
		{
			return AcquiredCompanyNetIncome__BackingField;
		}
		set
		{
			AcquiredCompanyNetIncome__BackingField = value;
		}
	}

	public double AcquiredCompanySellPrice
	{
		get
		{
			return AcquiredCompanySellPrice__BackingField;
		}
		set
		{
			AcquiredCompanySellPrice__BackingField = value;
		}
	}

	public double JobOfferRevenue
	{
		get
		{
			return JobOfferRevenue__BackingField;
		}
		set
		{
			JobOfferRevenue__BackingField = value;
		}
	}

	public double JobOfferCosts
	{
		get
		{
			return JobOfferCosts__BackingField;
		}
		set
		{
			JobOfferCosts__BackingField = value;
		}
	}

	public double JobOfferNetIncome
	{
		get
		{
			return JobOfferNetIncome__BackingField;
		}
		set
		{
			JobOfferNetIncome__BackingField = value;
		}
	}

	public double MaxInvestmentCap
	{
		get
		{
			return MaxInvestmentCap__BackingField;
		}
		set
		{
			MaxInvestmentCap__BackingField = value;
		}
	}

	public double SellValueIndex
	{
		get
		{
			return SellValueIndex__BackingField;
		}
		set
		{
			SellValueIndex__BackingField = value;
		}
	}

	public double TotalSellPrice
	{
		get
		{
			return TotalSellPrice__BackingField;
		}
		set
		{
			TotalSellPrice__BackingField = value;
		}
	}

	public int AgeAtLastSellAttempt
	{
		get
		{
			return AgeAtLastSellAttempt__BackingField;
		}
		set
		{
			AgeAtLastSellAttempt__BackingField = value;
		}
	}

	public int AgeAtLastGaveSelfBonus
	{
		get
		{
			return AgeAtLastGaveSelfBonus__BackingField;
		}
		set
		{
			AgeAtLastGaveSelfBonus__BackingField = value;
		}
	}

	public int AgeAtLastVirality
	{
		get
		{
			return AgeAtLastVirality__BackingField;
		}
		set
		{
			AgeAtLastVirality__BackingField = value;
		}
	}

	public int AgeAtLastSupplierTermination
	{
		get
		{
			return AgeAtLastSupplierTermination__BackingField;
		}
		set
		{
			AgeAtLastSupplierTermination__BackingField = value;
		}
	}

	public int NumTimesLookedForCandidate
	{
		get
		{
			return NumTimesLookedForCandidate__BackingField;
		}
		set
		{
			NumTimesLookedForCandidate__BackingField = value;
		}
	}

	public int NumTimesLostLawsuit
	{
		get
		{
			return NumTimesLostLawsuit__BackingField;
		}
		set
		{
			NumTimesLostLawsuit__BackingField = value;
		}
	}

	public float Att_Performance
	{
		get
		{
			return Att_Performance__BackingField;
		}
		set
		{
			Att_Performance__BackingField = value;
		}
	}

	public int WorkforceRating
	{
		get
		{
			return WorkforceRating__BackingField;
		}
		set
		{
			WorkforceRating__BackingField = value;
		}
	}

	public int TotalNumberOfBonuses
	{
		get
		{
			return TotalNumberOfBonuses__BackingField;
		}
		set
		{
			TotalNumberOfBonuses__BackingField = value;
		}
	}

	public double BonusesTotal
	{
		get
		{
			return BonusesTotal__BackingField;
		}
		set
		{
			BonusesTotal__BackingField = value;
		}
	}

	public int TotalNumberOfBoardRaises
	{
		get
		{
			return TotalNumberOfBoardRaises__BackingField;
		}
		set
		{
			TotalNumberOfBoardRaises__BackingField = value;
		}
	}

	public int AgeAtLastBoardBonus
	{
		get
		{
			return AgeAtLastBoardBonus__BackingField;
		}
		set
		{
			AgeAtLastBoardBonus__BackingField = value;
		}
	}

	public int AgeAtLastBoardRaise
	{
		get
		{
			return AgeAtLastBoardRaise__BackingField;
		}
		set
		{
			AgeAtLastBoardRaise__BackingField = value;
		}
	}

	public float CompanyPerformanceBonusGenerosity
	{
		get
		{
			return CompanyPerformanceBonusGenerosity__BackingField;
		}
		set
		{
			CompanyPerformanceBonusGenerosity__BackingField = value;
		}
	}

	public int AgeAtBankruptcy
	{
		get
		{
			return AgeAtBankruptcy__BackingField;
		}
		set
		{
			AgeAtBankruptcy__BackingField = value;
		}
	}

	public int AgeAtSoldCompany
	{
		get
		{
			return AgeAtSoldCompany__BackingField;
		}
		set
		{
			AgeAtSoldCompany__BackingField = value;
		}
	}

	public double TotalSellProfit
	{
		get
		{
			return TotalSellProfit__BackingField;
		}
		set
		{
			TotalSellProfit__BackingField = value;
		}
	}

	public int NumTimesVisitedConsultantThisYear
	{
		get
		{
			return NumTimesVisitedConsultantThisYear__BackingField;
		}
		set
		{
			NumTimesVisitedConsultantThisYear__BackingField = value;
		}
	}

	public int NumTimesCookedBooks
	{
		get
		{
			return NumTimesCookedBooks__BackingField;
		}
		set
		{
			NumTimesCookedBooks__BackingField = value;
		}
	}

	public SimBusiness(int businessTier, bool successfulBusiness, bool isJobOffer)
	{
	}

	public SimBusiness(int industryType)
	{
	}

	public int CalculateBaseCEOSalary()
	{
		return 0;
	}

	public void CreateAnnualReport()
	{
	}

	public double CalculateAmountSold(SimProduct product, bool simulateResult = false)
	{
		return 0.0;
	}

	public static float BusinessSuccessDenominator(float productDemandCurve, float productSupplyCurve, float industryDemandSupplyCurve)
	{
		return 0f;
	}

	public float ProductDemandCurve(SimProduct product, out float productSupplyCurve, out float industryDemandSupplyCurve)
	{
		productSupplyCurve = default;
		industryDemandSupplyCurve = default;
		return 0f;
	}

	private SimProduct CurrentViralProduct()
	{
		return null;
	}

	private void AdjustPerformance(double revenue, double netIncome, double prevRevenue, double prevNetIncome)
	{
	}

	public void IncrementAge()
	{
	}

	public void SetProductionAutomatically(SimProduct theProduct)
	{
	}

	public float GetProductLaunchCost()
	{
		return 0f;
	}

	public float GetEmployeeSalaryForCompensationPercentage(int percentage)
	{
		return 0f;
	}

	public string GetCompensationLevelStringForPercentage(int percentage)
	{
		return null;
	}

	public SimAnnualReport GetLastReport()
	{
		return null;
	}

	public double WorkingCapital()
	{
		return 0.0;
	}

	public double CalculateTotalProductCost()
	{
		return 0.0;
	}

	public float CalculateFacilityCost()
	{
		return 0f;
	}

	public float CalculateEmployeePayrollCost()
	{
		return 0f;
	}

	public double CalculateInventoryValue()
	{
		return 0.0;
	}

	public void AdjustBankBalance(double adjustAmount)
	{
	}

	public void ImpactPerformance(int impactScore)
	{
	}

	public void ImpactAllEmployeeMorale(int impactScore)
	{
	}

	public float GetWorkforceRating()
	{
		return 0f;
	}

	public void UpdateProductionForFacilityCapacityChange()
	{
	}

	public double OptimalSellPrice(bool newCompany = false)
	{
		return 0.0;
	}

	public bool WillReceiveBankruptcyWarning()
	{
		return false;
	}

	public bool WillGoBankrupt()
	{
		return false;
	}

	public float FacilityUnitCapacity(SimProduct aProduct)
	{
		return 0f;
	}

	public KeyValuePair<SimProduct, double> GetHighestRevenueProduct()
	{
		return default;
	}

	public int GetBusinessTier()
	{
		return 0;
	}

	public double CalculateStartupCosts()
	{
		return 0.0;
	}

	public static double MaxUnitCapacityForProduct(SimProduct product, int numProducts, int numEmployees, int numFacilities, int facilityEmployeeMax)
	{
		return 0.0;
	}

	public static string GetIndustryNameAndCapitalize(int industry, bool capitalize = true, bool translate = false)
	{
		return null;
	}

	public static string LawsuitReasonString(int lawsuitReason, bool willCapitalize = false)
	{
		return null;
	}
}
