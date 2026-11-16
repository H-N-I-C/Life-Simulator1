using System;
using System.Collections.Generic;

[Serializable]
public class SimProduct
{
	private string ProductLookupValue__BackingField;

	private int IndustryEnum__BackingField;

	private string DisplayName__BackingField;

	private int Age__BackingField;

	private string CustomName__BackingField;

	private Dictionary<int, int> Ledger__BackingField;

	private SimSupplier CurrentSupplier__BackingField;

	private SimSupplier OriginalSupplier__BackingField;

	private double CurrentInventory__BackingField;

	private float MarketingCostPerUnit__BackingField;

	private float PricePerUnit__BackingField;

	private double UnitsSold__BackingField;

	private int Demand__BackingField;

	private int Competition__BackingField;

	private bool DemandTrendingUp__BackingField;

	private bool CompetitionTrendingUp__BackingField;

	private double UnitsToProduce__BackingField;

	private int ForecastedDemand__BackingField;

	private int ForecastedCompetition__BackingField;

	private string DemandVariationString__BackingField;

	private int DemandVariationType__BackingField;

	private string CompetitionVariationString__BackingField;

	private int CompetitionVariationType__BackingField;

	private int CurrentYearDemandPrediction__BackingField;

	private int CurrentYearCompetitionPrediction__BackingField;

	private string AnnualProfileString__BackingField;

	private int CustomerRating__BackingField;

	private double FacilityUnitCapacity__BackingField;

	private int NumSuppliersFoundAtAge__BackingField;

	private int HeroAgeAtLastSelectedSupplier__BackingField;

	private bool IsViral__BackingField;

	public DataBusinessProduct Product
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public string ProductLookupValue
	{
		get
		{
			return ProductLookupValue__BackingField;
		}
		set
		{
			ProductLookupValue__BackingField = value;
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

	public string DisplayName
	{
		get
		{
			return DisplayName__BackingField;
		}
		set
		{
			DisplayName__BackingField = value;
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

	public string CustomName
	{
		get
		{
			return CustomName__BackingField;
		}
		set
		{
			CustomName__BackingField = value;
		}
	}

	public Dictionary<int, int> Ledger
	{
		get
		{
			return Ledger__BackingField;
		}
		set
		{
			Ledger__BackingField = value;
		}
	}

	public SimSupplier CurrentSupplier
	{
		get
		{
			return CurrentSupplier__BackingField;
		}
		set
		{
			CurrentSupplier__BackingField = value;
		}
	}

	public SimSupplier OriginalSupplier
	{
		get
		{
			return OriginalSupplier__BackingField;
		}
		set
		{
			OriginalSupplier__BackingField = value;
		}
	}

	public double CurrentInventory
	{
		get
		{
			return CurrentInventory__BackingField;
		}
		set
		{
			CurrentInventory__BackingField = value;
		}
	}

	public float MarketingCostPerUnit
	{
		get
		{
			return MarketingCostPerUnit__BackingField;
		}
		set
		{
			MarketingCostPerUnit__BackingField = value;
		}
	}

	public float PricePerUnit
	{
		get
		{
			return PricePerUnit__BackingField;
		}
		set
		{
			PricePerUnit__BackingField = value;
		}
	}

	public double UnitsSold
	{
		get
		{
			return UnitsSold__BackingField;
		}
		set
		{
			UnitsSold__BackingField = value;
		}
	}

	public int Demand
	{
		get
		{
			return Demand__BackingField;
		}
		set
		{
			Demand__BackingField = value;
		}
	}

	public int Competition
	{
		get
		{
			return Competition__BackingField;
		}
		set
		{
			Competition__BackingField = value;
		}
	}

	public bool DemandTrendingUp
	{
		get
		{
			return DemandTrendingUp__BackingField;
		}
		set
		{
			DemandTrendingUp__BackingField = value;
		}
	}

	public bool CompetitionTrendingUp
	{
		get
		{
			return CompetitionTrendingUp__BackingField;
		}
		set
		{
			CompetitionTrendingUp__BackingField = value;
		}
	}

	public double UnitsToProduce
	{
		get
		{
			return UnitsToProduce__BackingField;
		}
		set
		{
			UnitsToProduce__BackingField = value;
		}
	}

	public int ForecastedDemand
	{
		get
		{
			return ForecastedDemand__BackingField;
		}
		set
		{
			ForecastedDemand__BackingField = value;
		}
	}

	public int ForecastedCompetition
	{
		get
		{
			return ForecastedCompetition__BackingField;
		}
		set
		{
			ForecastedCompetition__BackingField = value;
		}
	}

	public string DemandVariationString
	{
		get
		{
			return DemandVariationString__BackingField;
		}
		set
		{
			DemandVariationString__BackingField = value;
		}
	}

	public int DemandVariationType
	{
		get
		{
			return DemandVariationType__BackingField;
		}
		set
		{
			DemandVariationType__BackingField = value;
		}
	}

	public string CompetitionVariationString
	{
		get
		{
			return CompetitionVariationString__BackingField;
		}
		set
		{
			CompetitionVariationString__BackingField = value;
		}
	}

	public int CompetitionVariationType
	{
		get
		{
			return CompetitionVariationType__BackingField;
		}
		set
		{
			CompetitionVariationType__BackingField = value;
		}
	}

	public int CurrentYearDemandPrediction
	{
		get
		{
			return CurrentYearDemandPrediction__BackingField;
		}
		set
		{
			CurrentYearDemandPrediction__BackingField = value;
		}
	}

	public int CurrentYearCompetitionPrediction
	{
		get
		{
			return CurrentYearCompetitionPrediction__BackingField;
		}
		set
		{
			CurrentYearCompetitionPrediction__BackingField = value;
		}
	}

	public string AnnualProfileString
	{
		get
		{
			return AnnualProfileString__BackingField;
		}
		set
		{
			AnnualProfileString__BackingField = value;
		}
	}

	private int CustomerRating
	{
		get
		{
			return CustomerRating__BackingField;
		}
		set
		{
			CustomerRating__BackingField = value;
		}
	}

	public double FacilityUnitCapacity
	{
		get
		{
			return FacilityUnitCapacity__BackingField;
		}
		set
		{
			FacilityUnitCapacity__BackingField = value;
		}
	}

	public int NumSuppliersFoundAtAge
	{
		get
		{
			return NumSuppliersFoundAtAge__BackingField;
		}
		set
		{
			NumSuppliersFoundAtAge__BackingField = value;
		}
	}

	public int HeroAgeAtLastSelectedSupplier
	{
		get
		{
			return HeroAgeAtLastSelectedSupplier__BackingField;
		}
		set
		{
			HeroAgeAtLastSelectedSupplier__BackingField = value;
		}
	}

	public bool IsViral
	{
		get
		{
			return IsViral__BackingField;
		}
		set
		{
			IsViral__BackingField = value;
		}
	}

	public SimProduct(DataBusinessProduct product, bool nameOverride)
	{
	}

	public void IncrementAge()
	{
	}

	private float UpdateCustomerRating()
	{
		return 0f;
	}

	public float GetCustomerRating()
	{
		return 0f;
	}

	public double TotalProductCost()
	{
		return 0.0;
	}
}
