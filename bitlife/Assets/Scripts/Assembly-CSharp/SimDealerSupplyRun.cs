using System;

[Serializable]
public class SimDealerSupplyRun
{
	private SimPlace Location__BackingField;

	private string DrugType__BackingField;

	private int InitialLoadSize__BackingField;

	private double Payout__BackingField;

	private float Risk__BackingField;

	private SimDealerCartelMember PilotMember__BackingField;

	private int MidRunLoadSizeAdjustment__BackingField;

	[NonSerialized]
	public string PackageTypeString;

	public SimPlace Location
	{
		get
		{
			return Location__BackingField;
		}
		set
		{
			Location__BackingField = value;
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

	public int InitialLoadSize
	{
		get
		{
			return InitialLoadSize__BackingField;
		}
		set
		{
			InitialLoadSize__BackingField = value;
		}
	}

	public double Payout
	{
		get
		{
			return Payout__BackingField;
		}
		set
		{
			Payout__BackingField = value;
		}
	}

	public float Risk
	{
		get
		{
			return Risk__BackingField;
		}
		set
		{
			Risk__BackingField = value;
		}
	}

	public SimDealerCartelMember PilotMember
	{
		get
		{
			return PilotMember__BackingField;
		}
		set
		{
			PilotMember__BackingField = value;
		}
	}

	public int MidRunLoadSizeAdjustment
	{
		get
		{
			return MidRunLoadSizeAdjustment__BackingField;
		}
		set
		{
			MidRunLoadSizeAdjustment__BackingField = value;
		}
	}

	public SimDealerSupplyRun(SimPlace location, string drugType, int initialLoadSize, double payout, float risk)
	{
	}

	public int GetCurrentLoadSize()
	{
		return 0;
	}
}
