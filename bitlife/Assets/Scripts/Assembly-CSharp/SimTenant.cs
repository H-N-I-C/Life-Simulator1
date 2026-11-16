using System;
using System.Collections.Generic;

[Serializable]
public class SimTenant : SimLover
{
	private sealed class _003C_003Ec__DisplayClass64_0
	{
		public SimTenantPet pet;

		internal bool _003CPetString_003Eb__0(SimTenantPet x)
		{
			return false;
		}
	}

	private string EmploymentString__BackingField;

	private int NumTimesLateOnRent__BackingField;

	private double RentAmountOwed__BackingField;

	private double CurrentLateRent__BackingField;

	private int CreditScore__BackingField;

	private List<SimPerson> PersonList__BackingField;

	private List<SimTenantPet> PetList__BackingField;

	private int NumTimesEvicted__BackingField;

	private float Att_satisfaction__BackingField;

	private int NumMaintenanceRequestsIgnored__BackingField;

	private int YearsRented__BackingField;

	private int HeroAgeAtLastPullInteractionWithTenant__BackingField;

	private int HeroAgeAtTenantStartedSquatting__BackingField;

	private int HeroAgeAtTenantContractStart__BackingField;

	private int HeroAgeAtLastLateRent__BackingField;

	public string EmploymentString
	{
		get
		{
			return EmploymentString__BackingField;
		}
		set
		{
			EmploymentString__BackingField = value;
		}
	}

	public int NumTimesLateOnRent
	{
		get
		{
			return NumTimesLateOnRent__BackingField;
		}
		set
		{
			NumTimesLateOnRent__BackingField = value;
		}
	}

	public double RentAmountOwed
	{
		get
		{
			return RentAmountOwed__BackingField;
		}
		set
		{
			RentAmountOwed__BackingField = value;
		}
	}

	public double CurrentLateRent
	{
		get
		{
			return CurrentLateRent__BackingField;
		}
		set
		{
			CurrentLateRent__BackingField = value;
		}
	}

	public int CreditScore
	{
		get
		{
			return CreditScore__BackingField;
		}
		set
		{
			CreditScore__BackingField = value;
		}
	}

	public List<SimPerson> PersonList
	{
		get
		{
			return PersonList__BackingField;
		}
		set
		{
			PersonList__BackingField = value;
		}
	}

	public List<SimTenantPet> PetList
	{
		get
		{
			return PetList__BackingField;
		}
		set
		{
			PetList__BackingField = value;
		}
	}

	public int NumTimesEvicted
	{
		get
		{
			return NumTimesEvicted__BackingField;
		}
		set
		{
			NumTimesEvicted__BackingField = value;
		}
	}

	public float Att_satisfaction
	{
		get
		{
			return Att_satisfaction__BackingField;
		}
		set
		{
			Att_satisfaction__BackingField = value;
		}
	}

	public int NumMaintenanceRequestsIgnored
	{
		get
		{
			return NumMaintenanceRequestsIgnored__BackingField;
		}
		set
		{
			NumMaintenanceRequestsIgnored__BackingField = value;
		}
	}

	public int YearsRented
	{
		get
		{
			return YearsRented__BackingField;
		}
		set
		{
			YearsRented__BackingField = value;
		}
	}

	public int HeroAgeAtLastPullInteractionWithTenant
	{
		get
		{
			return HeroAgeAtLastPullInteractionWithTenant__BackingField;
		}
		set
		{
			HeroAgeAtLastPullInteractionWithTenant__BackingField = value;
		}
	}

	public int HeroAgeAtTenantStartedSquatting
	{
		get
		{
			return HeroAgeAtTenantStartedSquatting__BackingField;
		}
		set
		{
			HeroAgeAtTenantStartedSquatting__BackingField = value;
		}
	}

	public int HeroAgeAtTenantContractStart
	{
		get
		{
			return HeroAgeAtTenantContractStart__BackingField;
		}
		set
		{
			HeroAgeAtTenantContractStart__BackingField = value;
		}
	}

	public int HeroAgeAtLastLateRent
	{
		get
		{
			return HeroAgeAtLastLateRent__BackingField;
		}
		set
		{
			HeroAgeAtLastLateRent__BackingField = value;
		}
	}

	public SimTenant(bool initHere)
		: base(initHere: false)
	{
	}

	public override void IncrementAge()
	{
	}

	public bool WillGiveHeroTimeOfDay()
	{
		return false;
	}

	public string HouseholdInhabitantsString()
	{
		return null;
	}

	public string PetString()
	{
		return null;
	}

	public bool CanAffordRent(double rentPerMonth)
	{
		return false;
	}

	public void ImpactSatisfaction(int impactScore)
	{
	}

	public bool IsSquatting()
	{
		return false;
	}

	public static string LawsuitReasonString(int lawsuitReason, bool willCapitalize = false)
	{
		return null;
	}

	public new string RelationshipStatusString()
	{
		return null;
	}
}
