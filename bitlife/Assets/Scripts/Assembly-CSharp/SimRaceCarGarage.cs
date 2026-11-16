using System;
using System.Collections.Generic;

[Serializable]
public class SimRaceCarGarage
{
	private string Address__BackingField;

	private bool GivenAsIAPUnlock__BackingField;

	private int NumYearsOwned__BackingField;

	private List<SimRaceCar> _raceCars;

	private int _numberOfBays;

	private float _purchasePrice;

	private SimLoan _mortgage;

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

	public int NumYearsOwned
	{
		get
		{
			return NumYearsOwned__BackingField;
		}
		set
		{
			NumYearsOwned__BackingField = value;
		}
	}

	public SimLoan Mortgage => null;

	public void IncrementAge()
	{
	}

	public int GetNumberOfBays()
	{
		return 0;
	}

	public void AddBay()
	{
	}

	public double GetPurchasePrice()
	{
		return 0.0;
	}

	public SimRaceCar AddCar(SimCar car)
	{
		return null;
	}

	public SimRaceCar AddCar(SimAuctionItem car)
	{
		return null;
	}

	public void AddCar(SimRaceCar raceCar)
	{
	}

	public void RemoveCar(SimRaceCar car, bool moveBackToCarList = true)
	{
	}

	public List<SimRaceCar> GetCars()
	{
		return null;
	}

	public int GetNumberOfOpenBays()
	{
		return 0;
	}

	public static int GetMaxNumberOfBays()
	{
		return 0;
	}

	public static int GetCostPerBay()
	{
		return 0;
	}

	public bool ContainsCar(SimCar car)
	{
		return false;
	}

	public bool ContainsCar(SimAuctionItem car)
	{
		return false;
	}

	public int GetNumberOfCars()
	{
		return 0;
	}

	public SimLoan ApplyForMortgage()
	{
		return null;
	}

	public SimLoan GetActiveMortgage()
	{
		return null;
	}

	public bool HasActiveMortgage()
	{
		return false;
	}

	public LOAN_REJECTION_REASON PreQualifyForMortgage()
	{
		return LOAN_REJECTION_REASON.NONE;
	}

	private double GetHeroBalance()
	{
		return 0.0;
	}

	public float GetAnnualCost()
	{
		return 0f;
	}

	public bool HasOnlyBrokenCarsToRaceForCategory(RACING_TRACK_CATEGORY category)
	{
		return false;
	}

	public List<SimRaceCar> GetCarsToRaceForCategory(RACING_TRACK_CATEGORY category)
	{
		return null;
	}
}
