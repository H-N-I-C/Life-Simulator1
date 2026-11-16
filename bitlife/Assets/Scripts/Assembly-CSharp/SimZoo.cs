using System;
using System.Collections.Generic;
using Game;

[Serializable]
public class SimZoo
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimZooAttraction, bool> _003C_003E9__185_0;

		public static Func<SimZooEmployee, float> _003C_003E9__188_0;

		public static Func<SimZooHabitat, float> _003C_003E9__196_0;

		public static Func<SimZooAttraction, float> _003C_003E9__197_0;

		public static Func<SimZooEmployee, float> _003C_003E9__198_0;

		public static Func<SimZooAnimal, bool> _003C_003E9__200_0;

		public static Func<KeyValuePair<SimZooAnimal, SimZooHabitat>, string> _003C_003E9__228_0;

		public static Func<KeyValuePair<SimZooAnimal, SimZooHabitat>, SimZooAnimal> _003C_003E9__228_1;

		public static Func<KeyValuePair<SimZooAnimal, SimZooHabitat>, SimZooHabitat> _003C_003E9__228_2;

		public static Func<KeyValuePair<SimZooAnimal, SimZooHabitat>, SimZooAnimal> _003C_003E9__228_4;

		public static Func<KeyValuePair<SimZooAnimal, SimZooHabitat>, SimZooHabitat> _003C_003E9__228_5;

		public static Func<SimZooAnimal, bool> _003C_003E9__246_0;

		public static Comparison<SimZooAttraction> _003C_003E9__252_0;

		public static Func<SimZooEmployee, bool> _003C_003E9__256_0;

		public static Comparison<SimZooAnimalHandler> _003C_003E9__259_0;

		public static Func<SimZooAnimal, float> _003C_003E9__277_0;

		public static Func<SimZooGiftShopItem, SimZooAnimal> _003C_003E9__281_1;

		internal bool _003CGetTrafficValue_003Eb__185_0(SimZooAttraction t)
		{
			return false;
		}

		internal float _003CGetHeroPerformance_003Eb__188_0(SimZooEmployee t)
		{
			return 0f;
		}

		internal float _003CGetAverageHabitatLivingCondition_003Eb__196_0(SimZooHabitat x)
		{
			return 0f;
		}

		internal float _003CGetAverageAttractionEngagement_003Eb__197_0(SimZooAttraction x)
		{
			return 0f;
		}

		internal float _003CGetAverageEmployeeCompetence_003Eb__198_0(SimZooEmployee x)
		{
			return 0f;
		}

		internal bool _003CGetTotalNumberOfSickAnimals_003Eb__200_0(SimZooAnimal t)
		{
			return false;
		}

		internal string _003CGetMostPopularAnimal_003Eb__228_0(KeyValuePair<SimZooAnimal, SimZooHabitat> t)
		{
			return null;
		}

		internal SimZooAnimal _003CGetMostPopularAnimal_003Eb__228_1(KeyValuePair<SimZooAnimal, SimZooHabitat> kvp)
		{
			return null;
		}

		internal SimZooHabitat _003CGetMostPopularAnimal_003Eb__228_2(KeyValuePair<SimZooAnimal, SimZooHabitat> kvp)
		{
			return null;
		}

		internal SimZooAnimal _003CGetMostPopularAnimal_003Eb__228_4(KeyValuePair<SimZooAnimal, SimZooHabitat> kvp)
		{
			return null;
		}

		internal SimZooHabitat _003CGetMostPopularAnimal_003Eb__228_5(KeyValuePair<SimZooAnimal, SimZooHabitat> kvp)
		{
			return null;
		}

		internal bool _003CGetExistingRareAnimalFromCompetingZoo_003Eb__246_0(SimZooAnimal x)
		{
			return false;
		}

		internal int _003CGetAttractionsListForTicketedType_003Eb__252_0(SimZooAttraction x, SimZooAttraction y)
		{
			return 0;
		}

		internal bool _003CGetRandomEmployee_003Eb__256_0(SimZooEmployee t)
		{
			return false;
		}

		internal int _003CGetAllAnimalHandlers_003Eb__259_0(SimZooAnimalHandler x, SimZooAnimalHandler y)
		{
			return 0;
		}

		internal float _003CGetAverageAnimalsHealth_003Eb__277_0(SimZooAnimal x)
		{
			return 0f;
		}

		internal SimZooAnimal _003CHasGiftShopOptions_003Eb__281_1(SimZooGiftShopItem x)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass182_0
	{
		public SimZooHabitat habitat;

		internal bool _003C_002Ector_003Eb__0(SimZooHabitat t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass182_1
	{
		public SimZooAnimal animal;

		public Func<SimZooAnimal, bool> _003C_003E9__1;

		internal bool _003C_002Ector_003Eb__1(SimZooAnimal t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass183_0
	{
		public string competitorName;

		internal bool _003CFinishSetup_003Eb__0(SimZooCompetitor t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass208_0
	{
		public SimZooAttraction attraction;

		internal bool _003CInstallAttraction_003Eb__0(SimZooAttraction x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass211_0
	{
		public string breed;

		public GENDER gender;

		public Func<SimZooAnimal, bool> _003C_003E9__0;

		public Func<SimZooAnimal, bool> _003C_003E9__1;

		internal bool _003CGetAnimalsOfBreedAndGender_003Eb__0(SimZooAnimal t)
		{
			return false;
		}

		internal bool _003CGetAnimalsOfBreedAndGender_003Eb__1(SimZooAnimal t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass251_0
	{
		public SimZooAttractionCommentCard card;

		internal bool _003CGetNextUnviewedAttractionCommentCard_003Eb__0(SimZooAttraction x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass257_0
	{
		public SimZooEmployee employee;

		internal bool _003CEmployeeExists_003Eb__0(SimZooEmployee x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass281_0
	{
		public DataZooGiftShopItem item;

		internal bool _003CHasGiftShopOptions_003Eb__0(SimZooGiftShopItem t)
		{
			return false;
		}
	}

	private string Name__BackingField;

	private ZOO_SIZE PlotSize__BackingField;

	private int Age__BackingField;

	private int YearsOwned__BackingField;

	private double BankBalance__BackingField;

	private string Address__BackingField;

	private double YearlyStipend__BackingField;

	private int StipendLength__BackingField;

	private double AdmissionFee__BackingField;

	private List<SimZooHabitat> HabitatsList__BackingField;

	private List<SimZooAttraction> AttractionsList__BackingField;

	private List<SimZooEmployee> EmployeesList__BackingField;

	private List<SimZooGiftShopItem> GiftShopItemsList__BackingField;

	private List<SimZooCompetitor> CompetitorZoosList__BackingField;

	private SimZooSupplier Supplier__BackingField;

	private double PurchasePrice__BackingField;

	private int NumBreedAttemptsAvailableThisYear__BackingField;

	private int NumSuppliersAvailableThisYear__BackingField;

	private int NumAnimalHandlerApplicantsAvailableThisYear__BackingField;

	private int NumOperationManagerApplicantsAvailableThisYear__BackingField;

	private int NumVeterinarianApplicantsAvailableThisYear__BackingField;

	private bool HasGivenAllAnimalsHealthBuffThisYearViaVet__BackingField;

	private double LastYearProfits__BackingField;

	private int NumYearsZooIsProfitable__BackingField;

	private double LastYearTicketSales__BackingField;

	private double LastYearGrossRevenue__BackingField;

	private double LastYearExpenses__BackingField;

	private double TotalTicketsSoldThisYear__BackingField;

	private int StaffMinionsAssignedToSecurity__BackingField;

	private int StaffMinionsAssignedToCustodial__BackingField;

	private int StaffMinionsAssignedToCustomerRelations__BackingField;

	private int YearFirstRareAnimalShouldBeAvailable__BackingField;

	private bool HasAlreadyAcquiredFirstRareAnimal__BackingField;

	private List<SimZooAttractionCommentCard> ThisYearsAttractionCommentCardList__BackingField;

	private List<SimZooRequestedAnimal> RequestedZooAnimals__BackingField;

	private List<SimZooAnimal> CompetitorZooAnimalsUpForTradeOrSale__BackingField;

	private List<SimZooAnimal> AgeUpPregnancyPossibleFemaleAnimals__BackingField;

	private double AccumulatedStipendAmount__BackingField;

	private double _startingAnnualExpenses;

	private double _staffMinionSalary;

	private float _baselineHeroPerformanceScore;

	private SimLoan _Mortgage__BackingField;

	[NonSerialized]
	private List<Scenario> _zooIncrementAgeScenarioQueueArray;

	private bool HasReceivedBankruptcyWarning__BackingField;

	private int AgeAtLastSellAttempt__BackingField;

	private double TotalInvestmentValue__BackingField;

	private int NumYearsZooHasNegativeBankBalance__BackingField;

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

	public ZOO_SIZE PlotSize
	{
		get
		{
			return PlotSize__BackingField;
		}
		set
		{
			PlotSize__BackingField = value;
		}
	}

	public int Age
	{
		get
		{
			return Age__BackingField;
		}
		set
		{
			Age__BackingField = value;
		}
	}

	public int YearsOwned
	{
		get
		{
			return YearsOwned__BackingField;
		}
		set
		{
			YearsOwned__BackingField = value;
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

	public string Address
	{
		get
		{
			return Address__BackingField;
		}
		set
		{
			Address__BackingField = value;
		}
	}

	public double YearlyStipend
	{
		get
		{
			return YearlyStipend__BackingField;
		}
		set
		{
			YearlyStipend__BackingField = value;
		}
	}

	public int StipendLength
	{
		get
		{
			return StipendLength__BackingField;
		}
		set
		{
			StipendLength__BackingField = value;
		}
	}

	public double AdmissionFee
	{
		get
		{
			return AdmissionFee__BackingField;
		}
		set
		{
			AdmissionFee__BackingField = value;
		}
	}

	public List<SimZooHabitat> HabitatsList
	{
		get
		{
			return HabitatsList__BackingField;
		}
		set
		{
			HabitatsList__BackingField = value;
		}
	}

	public List<SimZooAttraction> AttractionsList
	{
		get
		{
			return AttractionsList__BackingField;
		}
		set
		{
			AttractionsList__BackingField = value;
		}
	}

	public List<SimZooEmployee> EmployeesList
	{
		get
		{
			return EmployeesList__BackingField;
		}
		set
		{
			EmployeesList__BackingField = value;
		}
	}

	public List<SimZooGiftShopItem> GiftShopItemsList
	{
		get
		{
			return GiftShopItemsList__BackingField;
		}
		set
		{
			GiftShopItemsList__BackingField = value;
		}
	}

	public List<SimZooCompetitor> CompetitorZoosList
	{
		get
		{
			return CompetitorZoosList__BackingField;
		}
		set
		{
			CompetitorZoosList__BackingField = value;
		}
	}

	public SimZooSupplier Supplier
	{
		get
		{
			return Supplier__BackingField;
		}
		set
		{
			Supplier__BackingField = value;
		}
	}

	public double PurchasePrice
	{
		get
		{
			return PurchasePrice__BackingField;
		}
		set
		{
			PurchasePrice__BackingField = value;
		}
	}

	public int NumBreedAttemptsAvailableThisYear
	{
		get
		{
			return NumBreedAttemptsAvailableThisYear__BackingField;
		}
		set
		{
			NumBreedAttemptsAvailableThisYear__BackingField = value;
		}
	}

	public int NumSuppliersAvailableThisYear
	{
		get
		{
			return NumSuppliersAvailableThisYear__BackingField;
		}
		set
		{
			NumSuppliersAvailableThisYear__BackingField = value;
		}
	}

	public int NumAnimalHandlerApplicantsAvailableThisYear
	{
		get
		{
			return NumAnimalHandlerApplicantsAvailableThisYear__BackingField;
		}
		set
		{
			NumAnimalHandlerApplicantsAvailableThisYear__BackingField = value;
		}
	}

	public int NumOperationManagerApplicantsAvailableThisYear
	{
		get
		{
			return NumOperationManagerApplicantsAvailableThisYear__BackingField;
		}
		set
		{
			NumOperationManagerApplicantsAvailableThisYear__BackingField = value;
		}
	}

	public int NumVeterinarianApplicantsAvailableThisYear
	{
		get
		{
			return NumVeterinarianApplicantsAvailableThisYear__BackingField;
		}
		set
		{
			NumVeterinarianApplicantsAvailableThisYear__BackingField = value;
		}
	}

	public bool HasGivenAllAnimalsHealthBuffThisYearViaVet
	{
		get
		{
			return HasGivenAllAnimalsHealthBuffThisYearViaVet__BackingField;
		}
		set
		{
			HasGivenAllAnimalsHealthBuffThisYearViaVet__BackingField = value;
		}
	}

	public double LastYearProfits
	{
		get
		{
			return LastYearProfits__BackingField;
		}
		set
		{
			LastYearProfits__BackingField = value;
		}
	}

	public int NumYearsZooIsProfitable
	{
		get
		{
			return NumYearsZooIsProfitable__BackingField;
		}
		set
		{
			NumYearsZooIsProfitable__BackingField = value;
		}
	}

	public double LastYearTicketSales
	{
		get
		{
			return LastYearTicketSales__BackingField;
		}
		set
		{
			LastYearTicketSales__BackingField = value;
		}
	}

	public double LastYearGrossRevenue
	{
		get
		{
			return LastYearGrossRevenue__BackingField;
		}
		set
		{
			LastYearGrossRevenue__BackingField = value;
		}
	}

	public double LastYearExpenses
	{
		get
		{
			return LastYearExpenses__BackingField;
		}
		set
		{
			LastYearExpenses__BackingField = value;
		}
	}

	public double TotalTicketsSoldThisYear
	{
		get
		{
			return TotalTicketsSoldThisYear__BackingField;
		}
		set
		{
			TotalTicketsSoldThisYear__BackingField = value;
		}
	}

	public int StaffMinionsAssignedToSecurity
	{
		get
		{
			return StaffMinionsAssignedToSecurity__BackingField;
		}
		set
		{
			StaffMinionsAssignedToSecurity__BackingField = value;
		}
	}

	public int StaffMinionsAssignedToCustodial
	{
		get
		{
			return StaffMinionsAssignedToCustodial__BackingField;
		}
		set
		{
			StaffMinionsAssignedToCustodial__BackingField = value;
		}
	}

	public int StaffMinionsAssignedToCustomerRelations
	{
		get
		{
			return StaffMinionsAssignedToCustomerRelations__BackingField;
		}
		set
		{
			StaffMinionsAssignedToCustomerRelations__BackingField = value;
		}
	}

	public int YearFirstRareAnimalShouldBeAvailable
	{
		get
		{
			return YearFirstRareAnimalShouldBeAvailable__BackingField;
		}
		set
		{
			YearFirstRareAnimalShouldBeAvailable__BackingField = value;
		}
	}

	public bool HasAlreadyAcquiredFirstRareAnimal
	{
		get
		{
			return HasAlreadyAcquiredFirstRareAnimal__BackingField;
		}
		set
		{
			HasAlreadyAcquiredFirstRareAnimal__BackingField = value;
		}
	}

	public List<SimZooAttractionCommentCard> ThisYearsAttractionCommentCardList
	{
		get
		{
			return ThisYearsAttractionCommentCardList__BackingField;
		}
		set
		{
			ThisYearsAttractionCommentCardList__BackingField = value;
		}
	}

	public List<SimZooRequestedAnimal> RequestedZooAnimals
	{
		get
		{
			return RequestedZooAnimals__BackingField;
		}
		set
		{
			RequestedZooAnimals__BackingField = value;
		}
	}

	public List<SimZooAnimal> CompetitorZooAnimalsUpForTradeOrSale
	{
		get
		{
			return CompetitorZooAnimalsUpForTradeOrSale__BackingField;
		}
		set
		{
			CompetitorZooAnimalsUpForTradeOrSale__BackingField = value;
		}
	}

	public List<SimZooAnimal> AgeUpPregnancyPossibleFemaleAnimals
	{
		get
		{
			return AgeUpPregnancyPossibleFemaleAnimals__BackingField;
		}
		set
		{
			AgeUpPregnancyPossibleFemaleAnimals__BackingField = value;
		}
	}

	public double AccumulatedStipendAmount
	{
		get
		{
			return AccumulatedStipendAmount__BackingField;
		}
		set
		{
			AccumulatedStipendAmount__BackingField = value;
		}
	}

	private SimLoan _Mortgage
	{
		get
		{
			return _Mortgage__BackingField;
		}
		set
		{
			_Mortgage__BackingField = value;
		}
	}

	public List<Scenario> ZooIncrementAgeScenarioQueueArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimLoan Mortgage
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public bool HasReceivedBankruptcyWarning
	{
		get
		{
			return HasReceivedBankruptcyWarning__BackingField;
		}
		set
		{
			HasReceivedBankruptcyWarning__BackingField = value;
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

	public double TotalInvestmentValue
	{
		get
		{
			return TotalInvestmentValue__BackingField;
		}
		set
		{
			TotalInvestmentValue__BackingField = value;
		}
	}

	public int NumYearsZooHasNegativeBankBalance
	{
		get
		{
			return NumYearsZooHasNegativeBankBalance__BackingField;
		}
		set
		{
			NumYearsZooHasNegativeBankBalance__BackingField = value;
		}
	}

	public SimZoo(ZOO_SIZE size = ZOO_SIZE.SMALL)
	{
	}

	public void FinishSetup()
	{
	}

	public void IncrementAge()
	{
	}

	public float GetTrafficValue()
	{
		return 0f;
	}

	private void CheckForDeadEmployees()
	{
	}

	public float GetAppeal()
	{
		return 0f;
	}

	public float GetHeroPerformance()
	{
		return 0f;
	}

	public double GetValue(bool includeBankBalance = true, bool extractAccumulatedStipends = false)
	{
		return 0.0;
	}

	public double GetAnnualProfit()
	{
		return 0.0;
	}

	public double GetAnnualTaxes(double income)
	{
		return 0.0;
	}

	public double GetAnnualRevenue()
	{
		return 0.0;
	}

	public double GetAnnualAttractionRevenue()
	{
		return 0.0;
	}

	public double GetAnnualGiftShopRevenue()
	{
		return 0.0;
	}

	public double GetAnnualAdmissionFeeRevenue()
	{
		return 0.0;
	}

	public float GetAverageHabitatLivingCondition()
	{
		return 0f;
	}

	public float GetAverageAttractionEngagement()
	{
		return 0f;
	}

	public float GetAverageEmployeeCompetence()
	{
		return 0f;
	}

	public int GetTotalNumberOfAnimals()
	{
		return 0;
	}

	public int GetTotalNumberOfSickAnimals()
	{
		return 0;
	}

	public void RemoveAnimal(SimZooAnimal animal)
	{
	}

	public void AddHabitat(SimZooHabitat habitat)
	{
	}

	public void RemoveHabitat(SimZooHabitat habitat)
	{
	}

	public void AddGiftShopItem(SimZooGiftShopItem item)
	{
	}

	public void RemoveGiftShopItem(SimZooGiftShopItem item)
	{
	}

	public void AddEmployee(SimZooEmployee employee)
	{
	}

	public void RemoveEmployee(SimZooEmployee employee)
	{
	}

	public void InstallAttraction(SimZooAttraction attraction)
	{
	}

	private void RecordRentalManagement()
	{
	}

	public void UninstallAttraction(SimZooAttraction attraction)
	{
	}

	public List<SimZooAnimal> GetAnimalsOfBreedAndGender(string breed = "", GENDER gender = GENDER.BOTH)
	{
		return null;
	}

	public bool CompatibleAnimalExistsInHabitatsExcludingHabitat(SimZooHabitat excludeHabitat)
	{
		return false;
	}

	public List<SimZooAnimal> GetAnimalsForHabitatTypeExcludingHabitat(string habitatType, SimZooHabitat excludeHabitat)
	{
		return null;
	}

	public bool AnimalsExistToBreedWithAnimal(SimZooAnimal animal)
	{
		return false;
	}

	public bool OnlyPregnantAnimalsExistThatCouldBreedWithAnimal(SimZooAnimal animal)
	{
		return false;
	}

	public List<SimZooAnimal> GetAnimalsToBreedWithAnimal(SimZooAnimal animal, bool ignorePregnancies = false)
	{
		return null;
	}

	public bool CompatibleHabitatVacancyExistsForAnimal(SimZooAnimal animal)
	{
		return false;
	}

	public SimZooHabitat GetHabitatForAnimal(SimZooAnimal animal)
	{
		return null;
	}

	public List<SimZooHabitat> GetCompatibleHabitatVacancyListForAnimal(SimZooAnimal animal, SimZooHabitat excludeHabitat = null)
	{
		return null;
	}

	public string GetEmptyHabitatSlotsString()
	{
		return null;
	}

	public SimZooCompetitor GetCompetitorZooForAnimal(SimZooAnimal animal)
	{
		return null;
	}

	public SimZooCompetitor GetCompetitorZooForName(string name)
	{
		return null;
	}

	public string GetPlotSizeString(ZOO_SIZE plotSize)
	{
		return null;
	}

	public int GetOperationsManagersStaffSize()
	{
		return 0;
	}

	public double GetUpgradeCost(ZOO_SIZE plotSize)
	{
		return 0.0;
	}

	public double GetAdmissionFeeValue()
	{
		return 0.0;
	}

	public double GetTotalInvestmentValue()
	{
		return 0.0;
	}

	public SimZooAnimal GetMostPopularAnimal()
	{
		return null;
	}

	public double GetAnnualExpenses()
	{
		return 0.0;
	}

	public double GetAnnualAttractionCosts()
	{
		return 0.0;
	}

	public double GetAnnualAnimalUpkeepCosts()
	{
		return 0.0;
	}

	public double GetAnnualHabitatUpkeepCosts()
	{
		return 0.0;
	}

	public double GetAnnualSupplierCost()
	{
		return 0.0;
	}

	public double GetAnnualEmployeeCosts()
	{
		return 0.0;
	}

	public double GetMinimumAdmissionFee()
	{
		return 0.0;
	}

	public double GetMaximumAdmissionFee()
	{
		return 0.0;
	}

	public int GetMaximumNumberHabitats(ZOO_SIZE plotSize)
	{
		return 0;
	}

	public int GetMaximumNumberAttractions(ZOO_SIZE plotSize)
	{
		return 0;
	}

	public int GetMaximumNumberGiftShopItems(ZOO_SIZE plotSize)
	{
		return 0;
	}

	public void OriginateMortgage(double openingValue)
	{
	}

	public LOAN_REJECTION_REASON MortgageRejectionReason(double cost = 0.0)
	{
		return LOAN_REJECTION_REASON.NONE;
	}

	private bool ProcessLoan(SimLoan loan)
	{
		return false;
	}

	public void ShutDownZoo()
	{
	}

	public bool CanExpandZoo()
	{
		return false;
	}

	public void GenerateCompetitorZooAnimalsUpForTradeList()
	{
	}

	private SimZooAnimal GetExistingRareAnimalFromCompetingZoo()
	{
		return null;
	}

	public void GenerateCompetitorZooRequestedAnimalsList()
	{
	}

	public bool AttractionAlreadyInstalled(SimZooAttraction attraction)
	{
		return false;
	}

	public List<SimZooAttraction> GetTicketedAttractionsList()
	{
		return null;
	}

	public List<SimZooAttraction> GetFreeAttractionsList()
	{
		return null;
	}

	public SimZooAttractionCommentCard GetNextUnviewedAttractionCommentCard()
	{
		return null;
	}

	private List<SimZooAttraction> GetAttractionsListForTicketedType(bool ticketed)
	{
		return null;
	}

	private void GenerateNewAttractionCommentCards()
	{
	}

	private double GetStartingPurchasePrice()
	{
		return 0.0;
	}

	public static float GetAnimalOwnerAppealScore(SimZooAnimal animal)
	{
		return 0f;
	}

	public SimZooEmployee GetRandomEmployee()
	{
		return null;
	}

	public bool EmployeeExists(SimZooEmployee employee)
	{
		return false;
	}

	public SimZooEmployee GetEmployeeWithType(ZOO_EMPLOYEE_TYPE type, string habitatSpecialization = null)
	{
		return null;
	}

	public List<SimZooAnimalHandler> GetAllAnimalHandlers(bool sortAlpha = false)
	{
		return null;
	}

	public SimZooAnimalHandler GetAnimalHandlerWithSpecialization(string habitatSpecialization)
	{
		return null;
	}

	public SimZooOperationsManager GetOperationsManager()
	{
		return null;
	}

	public SimZooVeterinarian GetVeterinarian()
	{
		return null;
	}

	public bool FullyStaffAnimalHandlers()
	{
		return false;
	}

	public bool AnimalHandlerEmployeeHasRelevantExpertise(SimZooAnimalHandler handler)
	{
		return false;
	}

	public int GetNumberOfAnimalsHandlerHasExpertiseIn(SimZooAnimalHandler handler)
	{
		return 0;
	}

	public SimZooAnimal GetRandomExpertiseAnimalForHandler(SimZooAnimalHandler handler)
	{
		return null;
	}

	public bool SickAnimalExists()
	{
		return false;
	}

	public int GetNumberOfSickAnimals()
	{
		return 0;
	}

	public SimZooAnimal GetRandomSickAnimal()
	{
		return null;
	}

	public static void CancelAllZooScenarios()
	{
	}

	public float GetZooCustodialScore()
	{
		return 0f;
	}

	public float GetCustomerRelationsTicketSalesBumpPercentage()
	{
		return 0f;
	}

	public float GetChanceOfSecurityBreach()
	{
		return 0f;
	}

	private void CreateAgeUpPregnancyPossibleFemaleAnimals()
	{
	}

	public List<SimZooAnimal> GetAllZooAnimals()
	{
		return null;
	}

	public void PositivelyImpactHealthBuffOfAllAnimals()
	{
	}

	public float GetAverageAnimalsHealth()
	{
		return 0f;
	}

	public float GetAverageAnimalsPopularity()
	{
		return 0f;
	}

	public bool GetAnimalExistsInZoo(SimZooAnimal animal)
	{
		return false;
	}

	public SimZooAnimal GetRandomAnimal()
	{
		return null;
	}

	public bool HasGiftShopOptions()
	{
		return false;
	}

	public void ImpactBasePerformance(int impactScore)
	{
	}

	private float _003CGetMostPopularAnimal_003Eb__228_3(KeyValuePair<SimZooAnimal, SimZooHabitat> kvp)
	{
		return 0f;
	}
}
