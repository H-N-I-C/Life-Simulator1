using System;

[Serializable]
public class SimPropertyLeaseContract
{
	private int PetPolicy__BackingField;

	private double RentAmount__BackingField;

	private double Deposit__BackingField;

	private double PropertyDamages__BackingField;

	private SimTenant Tenant__BackingField;

	private SimHouse Property__BackingField;

	private float PropertyConditionAtLeaseStart__BackingField;

	private int HeroAgeAtLastAddNewAdult__BackingField;

	private int HeroAgeAtLastInspection__BackingField;

	private int NumTimesInspectedThisYear__BackingField;

	public int PetPolicy
	{
		get
		{
			return PetPolicy__BackingField;
		}
		set
		{
			PetPolicy__BackingField = value;
		}
	}

	public double RentAmount
	{
		get
		{
			return RentAmount__BackingField;
		}
		set
		{
			RentAmount__BackingField = value;
		}
	}

	public double Deposit
	{
		get
		{
			return Deposit__BackingField;
		}
		set
		{
			Deposit__BackingField = value;
		}
	}

	public double PropertyDamages
	{
		get
		{
			return PropertyDamages__BackingField;
		}
		set
		{
			PropertyDamages__BackingField = value;
		}
	}

	public SimTenant Tenant
	{
		get
		{
			return Tenant__BackingField;
		}
		set
		{
			Tenant__BackingField = value;
		}
	}

	public SimHouse Property
	{
		get
		{
			return Property__BackingField;
		}
		set
		{
			Property__BackingField = value;
		}
	}

	public float PropertyConditionAtLeaseStart
	{
		get
		{
			return PropertyConditionAtLeaseStart__BackingField;
		}
		set
		{
			PropertyConditionAtLeaseStart__BackingField = value;
		}
	}

	public int HeroAgeAtLastAddNewAdult
	{
		get
		{
			return HeroAgeAtLastAddNewAdult__BackingField;
		}
		set
		{
			HeroAgeAtLastAddNewAdult__BackingField = value;
		}
	}

	public int HeroAgeAtLastInspection
	{
		get
		{
			return HeroAgeAtLastInspection__BackingField;
		}
		set
		{
			HeroAgeAtLastInspection__BackingField = value;
		}
	}

	public int NumTimesInspectedThisYear
	{
		get
		{
			return NumTimesInspectedThisYear__BackingField;
		}
		set
		{
			NumTimesInspectedThisYear__BackingField = value;
		}
	}

	public SimPropertyLeaseContract(SimHouse property, SimTenant tenant, double deposit, double rentAmount, int petPolicy)
	{
	}

	public void IncrementAge()
	{
	}

	public static string GetPetPolicyStringFromEnum(int petPolicy)
	{
		return null;
	}

	public static int GetPetPolicyEnumForString(string petPolicyString)
	{
		return 0;
	}

	public void CheckDeadTenants()
	{
	}
}
