using System;
using System.Collections.Generic;
using Game;

[Serializable]
public class SimDealerDrug
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataDealerDrugEffect, string> _003C_003E9__52_0;

		internal string _003C_002Ector_003Eb__52_0(DataDealerDrugEffect x)
		{
			return null;
		}
	}

	private string Type__BackingField;

	private List<SimDealerDrugCut> Cuts__BackingField;

	private double StreetValue__BackingField;

	private bool IsCut__BackingField;

	private double Cost__BackingField;

	private int MinSellPrice__BackingField;

	private int MaxSellPrice__BackingField;

	private double SoldPrice__BackingField;

	private double LastYearsProfit__BackingField;

	private double ThisYearsProfit__BackingField;

	private double TotalProfit__BackingField;

	protected float _demand;

	protected List<string> _effects;

	private int _dosesAvailable__BackingField;

	public string Type
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

	public List<SimDealerDrugCut> Cuts
	{
		get
		{
			return Cuts__BackingField;
		}
		set
		{
			Cuts__BackingField = value;
		}
	}

	public double StreetValue
	{
		get
		{
			return StreetValue__BackingField;
		}
		set
		{
			StreetValue__BackingField = value;
		}
	}

	public bool IsCut
	{
		get
		{
			return IsCut__BackingField;
		}
		set
		{
			IsCut__BackingField = value;
		}
	}

	public double Cost
	{
		get
		{
			return Cost__BackingField;
		}
		set
		{
			Cost__BackingField = value;
		}
	}

	public int MinSellPrice
	{
		get
		{
			return MinSellPrice__BackingField;
		}
		set
		{
			MinSellPrice__BackingField = value;
		}
	}

	public int MaxSellPrice
	{
		get
		{
			return MaxSellPrice__BackingField;
		}
		set
		{
			MaxSellPrice__BackingField = value;
		}
	}

	public double SoldPrice
	{
		get
		{
			return SoldPrice__BackingField;
		}
		set
		{
			SoldPrice__BackingField = value;
		}
	}

	public double LastYearsProfit
	{
		get
		{
			return LastYearsProfit__BackingField;
		}
		set
		{
			LastYearsProfit__BackingField = value;
		}
	}

	public double ThisYearsProfit
	{
		get
		{
			return ThisYearsProfit__BackingField;
		}
		set
		{
			ThisYearsProfit__BackingField = value;
		}
	}

	public double TotalProfit
	{
		get
		{
			return TotalProfit__BackingField;
		}
		set
		{
			TotalProfit__BackingField = value;
		}
	}

	protected int _dosesAvailable
	{
		get
		{
			return _dosesAvailable__BackingField;
		}
		set
		{
			_dosesAvailable__BackingField = value;
		}
	}

	public DataDealerCartelDrug Data => null;

	public SimDealerDrug(string type, double cost, int doses, bool isCut = false)
	{
	}

	public void IncrementAge()
	{
	}

	public int GetPossibleCutDosesFromPureDoses(int pureDoseaAvailable, int cutPercentage)
	{
		return 0;
	}

	public float GetRisk()
	{
		return 0f;
	}

	public float GetDemand()
	{
		return 0f;
	}

	public void ImpactDemand(int impactScore)
	{
	}

	public void AddCut(string ingredient, int percentage, string name, int doses, bool pullFromSourceDrugSupply = true)
	{
	}

	public void AddPremadeCut(SimDealerDrugCut drugCut)
	{
	}

	public virtual string GetEffectsListString()
	{
		return null;
	}

	public string GetProductName()
	{
		return null;
	}

	public float GetPurity()
	{
		return 0f;
	}

	public void OverrideDosesAvailable(int doses)
	{
	}

	public void AdjustAvailableDoses(int doses)
	{
	}

	public int DosesAvailable()
	{
		return 0;
	}

	public DataDealerDrugEffect GetRandomEffectData()
	{
		return null;
	}

	public int GetNumberOfRequiredPureDosesToCreateDesiredCutDoses(int desiredCutDoses, int cutIngredientPercentage)
	{
		return 0;
	}

	public int GetMaxNumberOfCutDosesThatCouldBeCreatedFromAvailablePureDoses(int cutIngredientPercentage)
	{
		return 0;
	}

	public static DataDealerDrugEffect GetDataForEffect(string effect)
	{
		return null;
	}

	public void CollectProfit(double value)
	{
	}
}
