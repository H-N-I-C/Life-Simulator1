using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SimHouse : IComparable
{
	private string ColorfulName__BackingField;

	private int Age__BackingField;

	private int YearsOwned__BackingField;

	private float Condition__BackingField;

	private bool GivenAsIAPUnlock__BackingField;

	private SimPlace Place__BackingField;

	private string _TypeName;

	private string ChosenTypeName__BackingField;

	private string ChosenDescriptor__BackingField;

	private int Bedrooms__BackingField;

	private int Bathrooms__BackingField;

	private float ValueAdj__BackingField;

	private string StreetAddress__BackingField;

	private SimLoan Mortgage__BackingField;

	private int PartiesThrown__BackingField;

	private int AgeAtLastHouseParty__BackingField;

	private int AgeAtLastSellAttempt__BackingField;

	private bool RepossessionWarningInEffect__BackingField;

	private int GenerationalAppreciationYears__BackingField;

	private ArrayList _GhostArray__BackingField;

	private float PurchasePrice__BackingField;

	private bool SpouseApprovedToBuy__BackingField;

	private bool SpouseRejectedToBuy__BackingField;

	private bool SpouseApprovedToSell__BackingField;

	private bool SpouseRejectedToSell__BackingField;

	private List<SimPropertyRemodel> _AddedFeatures;

	private SimTenant Squatter__BackingField;

	private List<string> KeyFeatures__BackingField;

	private List<string> Locations__BackingField;

	private int NumDeadTenants__BackingField;

	private float Att_hauntedness__BackingField;

	private bool WasAcquisitionObserved__BackingField;

	private SimVampireCoffin VampireCoffin__BackingField;

	public string ColorfulName
	{
		get
		{
			return ColorfulName__BackingField;
		}
		set
		{
			ColorfulName__BackingField = value;
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

	public float Condition
	{
		get
		{
			return Condition__BackingField;
		}
		set
		{
			Condition__BackingField = value;
		}
	}

	public bool GivenAsIAPUnlock
	{
		get
		{
			return GivenAsIAPUnlock__BackingField;
		}
		set
		{
			GivenAsIAPUnlock__BackingField = value;
		}
	}

	public SimPlace Place
	{
		get
		{
			return Place__BackingField;
		}
		set
		{
			Place__BackingField = value;
		}
	}

	public DataHouseType Type
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public string ChosenTypeName
	{
		get
		{
			return ChosenTypeName__BackingField;
		}
		set
		{
			ChosenTypeName__BackingField = value;
		}
	}

	public string ChosenDescriptor
	{
		get
		{
			return ChosenDescriptor__BackingField;
		}
		set
		{
			ChosenDescriptor__BackingField = value;
		}
	}

	public int Bedrooms
	{
		get
		{
			return Bedrooms__BackingField;
		}
		set
		{
			Bedrooms__BackingField = value;
		}
	}

	public int Bathrooms
	{
		get
		{
			return Bathrooms__BackingField;
		}
		set
		{
			Bathrooms__BackingField = value;
		}
	}

	public float ValueAdj
	{
		get
		{
			return ValueAdj__BackingField;
		}
		set
		{
			ValueAdj__BackingField = value;
		}
	}

	public string StreetAddress
	{
		get
		{
			return StreetAddress__BackingField;
		}
		set
		{
			StreetAddress__BackingField = value;
		}
	}

	public SimLoan Mortgage
	{
		get
		{
			return Mortgage__BackingField;
		}
		set
		{
			Mortgage__BackingField = value;
		}
	}

	public int PartiesThrown
	{
		get
		{
			return PartiesThrown__BackingField;
		}
		set
		{
			PartiesThrown__BackingField = value;
		}
	}

	public int AgeAtLastHouseParty
	{
		get
		{
			return AgeAtLastHouseParty__BackingField;
		}
		set
		{
			AgeAtLastHouseParty__BackingField = value;
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

	public bool RepossessionWarningInEffect
	{
		get
		{
			return RepossessionWarningInEffect__BackingField;
		}
		set
		{
			RepossessionWarningInEffect__BackingField = value;
		}
	}

	public int GenerationalAppreciationYears
	{
		get
		{
			return GenerationalAppreciationYears__BackingField;
		}
		set
		{
			GenerationalAppreciationYears__BackingField = value;
		}
	}

	private ArrayList _GhostArray
	{
		get
		{
			return _GhostArray__BackingField;
		}
		set
		{
			_GhostArray__BackingField = value;
		}
	}

	public ArrayList GhostArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public float PurchasePrice
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

	public bool SpouseApprovedToBuy
	{
		get
		{
			return SpouseApprovedToBuy__BackingField;
		}
		set
		{
			SpouseApprovedToBuy__BackingField = value;
		}
	}

	public bool SpouseRejectedToBuy
	{
		get
		{
			return SpouseRejectedToBuy__BackingField;
		}
		set
		{
			SpouseRejectedToBuy__BackingField = value;
		}
	}

	public bool SpouseApprovedToSell
	{
		get
		{
			return SpouseApprovedToSell__BackingField;
		}
		set
		{
			SpouseApprovedToSell__BackingField = value;
		}
	}

	public bool SpouseRejectedToSell
	{
		get
		{
			return SpouseRejectedToSell__BackingField;
		}
		set
		{
			SpouseRejectedToSell__BackingField = value;
		}
	}

	public List<SimPropertyRemodel> AddedFeatures
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimTenant Squatter
	{
		get
		{
			return Squatter__BackingField;
		}
		set
		{
			Squatter__BackingField = value;
		}
	}

	public List<string> KeyFeatures
	{
		get
		{
			return KeyFeatures__BackingField;
		}
		set
		{
			KeyFeatures__BackingField = value;
		}
	}

	public List<string> Locations
	{
		get
		{
			return Locations__BackingField;
		}
		set
		{
			Locations__BackingField = value;
		}
	}

	public int NumDeadTenants
	{
		get
		{
			return NumDeadTenants__BackingField;
		}
		set
		{
			NumDeadTenants__BackingField = value;
		}
	}

	public float Att_hauntedness
	{
		get
		{
			return Att_hauntedness__BackingField;
		}
		set
		{
			Att_hauntedness__BackingField = value;
		}
	}

	public bool WasAcquisitionObserved
	{
		get
		{
			return WasAcquisitionObserved__BackingField;
		}
		set
		{
			WasAcquisitionObserved__BackingField = value;
		}
	}

	public SimVampireCoffin VampireCoffin
	{
		get
		{
			return VampireCoffin__BackingField;
		}
		set
		{
			VampireCoffin__BackingField = value;
		}
	}

	public int CompareTo(object obj)
	{
		return 0;
	}

	private SimHouse()
	{
	}

	public SimHouse(DataHouseType theType)
	{
	}

	public string Description()
	{
		return null;
	}

	public string HouseDescriptionWithSpecsAndDescriptorAndAddressIsCapitalized(bool specs, bool descriptor, bool address, bool capitalized)
	{
		return null;
	}

	public void PopulateGhostsIfApplicable()
	{
	}

	public bool IsHaunted()
	{
		return false;
	}

	public void IncrementAge()
	{
	}

	public SimTenant GetTenantForHome()
	{
		return null;
	}

	public int MaxAmenityValue()
	{
		return 0;
	}

	public float PropertyValue()
	{
		return 0f;
	}

	public float PropertyEstimatedValue()
	{
		return 0f;
	}

	public float MonthlyExpense()
	{
		return 0f;
	}

	private float AppreciationRateAfterYears(int years)
	{
		return 0f;
	}

	public int MortgageRejectionReason()
	{
		return 0;
	}

	public void OriginateMortgage()
	{
	}

	public double GetFeatureCost(DataLandlordHomeRemodelFeature feature)
	{
		return 0.0;
	}

	public void ImpactCondition(int impactScore)
	{
	}
}
