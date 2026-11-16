using System;
using System.Collections.Generic;

[Serializable]
public class SimLandlordPortfolio
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimPropertyLeaseContract, bool> _003C_003E9__17_0;

		public static Func<SimPropertyLeaseContract, double> _003C_003E9__23_0;

		internal bool _003CIncrementAge_003Eb__17_0(SimPropertyLeaseContract x)
		{
			return false;
		}

		internal double _003CAnnualRentalIncome_003Eb__23_0(SimPropertyLeaseContract x)
		{
			return 0.0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass19_0
	{
		public SimHouse house;

		internal bool _003CDoesHomeHaveTenant_003Eb__0(SimPropertyLeaseContract x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass20_0
	{
		public SimTenant tenant;

		internal bool _003CGetContractForTenant_003Eb__0(SimPropertyLeaseContract x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass21_0
	{
		public SimTenant tenant;

		internal bool _003CGetPreviousContractForTenant_003Eb__0(SimPropertyLeaseContract x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass22_0
	{
		public SimHouse house;

		internal bool _003CGetTenantForHouse_003Eb__0(SimPropertyLeaseContract x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass29_0
	{
		public List<string> listOfNaturalDisasters;

		internal bool _003CIsCountryEligibleForNaturalDisasters_003Eb__0(string t)
		{
			return false;
		}
	}

	private List<SimPropertyLeaseContract> RentedProperties__BackingField;

	private List<SimPropertyLeaseContract> PreviousLeaseContracts__BackingField;

	private double SlumlordProfit__BackingField;

	private int NumTimesKeptDeposit__BackingField;

	public List<SimPropertyLeaseContract> RentedProperties
	{
		get
		{
			return RentedProperties__BackingField;
		}
		set
		{
			RentedProperties__BackingField = value;
		}
	}

	public List<SimPropertyLeaseContract> PreviousLeaseContracts
	{
		get
		{
			return PreviousLeaseContracts__BackingField;
		}
		set
		{
			PreviousLeaseContracts__BackingField = value;
		}
	}

	public double SlumlordProfit
	{
		get
		{
			return SlumlordProfit__BackingField;
		}
		set
		{
			SlumlordProfit__BackingField = value;
		}
	}

	public int NumTimesKeptDeposit
	{
		get
		{
			return NumTimesKeptDeposit__BackingField;
		}
		set
		{
			NumTimesKeptDeposit__BackingField = value;
		}
	}

	public void IncrementAge()
	{
	}

	private bool CheckForIncrementAgeScenarios(SimPropertyLeaseContract aContract, bool dumbScenarioAlreadyInQueue)
	{
		return false;
	}

	public bool DoesHomeHaveTenant(SimHouse house)
	{
		return false;
	}

	public SimPropertyLeaseContract GetContractForTenant(SimTenant tenant)
	{
		return null;
	}

	public SimPropertyLeaseContract GetPreviousContractForTenant(SimTenant tenant)
	{
		return null;
	}

	public SimTenant GetTenantForHouse(SimHouse house)
	{
		return null;
	}

	public double AnnualRentalIncome()
	{
		return 0.0;
	}

	public double PortfolioValue()
	{
		return 0.0;
	}

	public void AddTenant(SimPropertyLeaseContract contract)
	{
	}

	public void EvictTenant(SimPropertyLeaseContract contract)
	{
	}

	public void CancelAllLandlordScenarios()
	{
	}

	public void CheckDeadTenants()
	{
	}

	public static bool IsCountryEligibleForNaturalDisasters(DataCountry data, string houseType)
	{
		return false;
	}
}
