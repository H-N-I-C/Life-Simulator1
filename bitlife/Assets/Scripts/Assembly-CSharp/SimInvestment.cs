using System;

[Serializable]
public class SimInvestment
{
	private int AssetType__BackingField;

	private double Quantity__BackingField;

	private double TotalUntaxedInvestment__BackingField;

	private double AverageCost__BackingField;

	private string Name__BackingField;

	private int YearsOwned__BackingField;

	private double YearsLeftOnBond__BackingField;

	private double BondInvestment__BackingField;

	private string BondID__BackingField;

	private bool BondMatured__BackingField;

	private double FundInvestment__BackingField;

	public int AssetType
	{
		get
		{
			return AssetType__BackingField;
		}
		set
		{
			AssetType__BackingField = value;
		}
	}

	public double Quantity
	{
		get
		{
			return Quantity__BackingField;
		}
		set
		{
			Quantity__BackingField = value;
		}
	}

	public double TotalUntaxedInvestment
	{
		get
		{
			return TotalUntaxedInvestment__BackingField;
		}
		set
		{
			TotalUntaxedInvestment__BackingField = value;
		}
	}

	public double AverageCost
	{
		get
		{
			return AverageCost__BackingField;
		}
		set
		{
			AverageCost__BackingField = value;
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

	public double YearsLeftOnBond
	{
		get
		{
			return YearsLeftOnBond__BackingField;
		}
		set
		{
			YearsLeftOnBond__BackingField = value;
		}
	}

	public double BondInvestment
	{
		get
		{
			return BondInvestment__BackingField;
		}
		set
		{
			BondInvestment__BackingField = value;
		}
	}

	public string BondID
	{
		get
		{
			return BondID__BackingField;
		}
		set
		{
			BondID__BackingField = value;
		}
	}

	public bool BondMatured
	{
		get
		{
			return BondMatured__BackingField;
		}
		set
		{
			BondMatured__BackingField = value;
		}
	}

	public double FundInvestment
	{
		get
		{
			return FundInvestment__BackingField;
		}
		set
		{
			FundInvestment__BackingField = value;
		}
	}

	public double AnnualBondAmount(SimBond bond)
	{
		return 0.0;
	}

	public void IncrementAge()
	{
	}

	public void AdjustAverageCost(double quantity, double price)
	{
	}

	public double GetAnnualStartingValue()
	{
		return 0.0;
	}

	public void BuyAsset(double quantity, double price)
	{
	}

	public void SellAsset(double quantity, double price, SimBond bond = null)
	{
	}

	public double TotalCost()
	{
		return 0.0;
	}

	public double TotalReturn()
	{
		return 0.0;
	}

	public double TotalValue()
	{
		return 0.0;
	}

	public double AssetMarketValue()
	{
		return 0.0;
	}

	public string AssetSingularDescriptionString(bool report = false)
	{
		return null;
	}

	public string AssetPluralDescriptionString()
	{
		return null;
	}

	public string NameForAssetType()
	{
		return null;
	}

	private bool _003CIncrementAge_003Eb__45_0(SimBond x)
	{
		return false;
	}
}
