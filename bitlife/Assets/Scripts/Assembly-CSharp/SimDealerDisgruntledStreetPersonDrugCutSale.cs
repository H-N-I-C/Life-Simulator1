using System;

[Serializable]
public class SimDealerDisgruntledStreetPersonDrugCutSale
{
	private string CutName__BackingField;

	private string DrugType__BackingField;

	private double PurchasePrice__BackingField;

	private int HeroAgeSaleOccured__BackingField;

	public string CutName
	{
		get
		{
			return CutName__BackingField;
		}
		set
		{
			CutName__BackingField = value;
		}
	}

	public string DrugType
	{
		get
		{
			return DrugType__BackingField;
		}
		set
		{
			DrugType__BackingField = value;
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

	public int HeroAgeSaleOccured
	{
		get
		{
			return HeroAgeSaleOccured__BackingField;
		}
		set
		{
			HeroAgeSaleOccured__BackingField = value;
		}
	}

	public SimDealerDisgruntledStreetPersonDrugCutSale(SimDealerDrugCut cut, double purchasePrice)
	{
	}
}
