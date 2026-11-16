using System;

[Serializable]
public class SimRecreationalVehicle
{
	private RECREATIONAL_VEHICLE_TYPE Type__BackingField;

	private string Make__BackingField;

	private string Model__BackingField;

	private int Age__BackingField;

	private int YearsOwned__BackingField;

	private int Color__BackingField;

	private float DealerPrice__BackingField;

	private string Dealer__BackingField;

	private int AgeAtLastRestore__BackingField;

	private int AgeAtLastSellAttempt__BackingField;

	private bool RepossessionWarningInEffect__BackingField;

	private float MuseumCondition__BackingField;

	private bool HasGottenFromParent__BackingField;

	private int NumAnnualMaintenancesPerformed__BackingField;

	private SimLoan Loan__BackingField;

	private float _condition;

	private int _performanceScore;

	public RECREATIONAL_VEHICLE_TYPE Type
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

	public string Make
	{
		get
		{
			return Make__BackingField;
		}
		set
		{
			Make__BackingField = value;
		}
	}

	public string Model
	{
		get
		{
			return Model__BackingField;
		}
		set
		{
			Model__BackingField = value;
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

	public int Color
	{
		get
		{
			return Color__BackingField;
		}
		set
		{
			Color__BackingField = value;
		}
	}

	public float DealerPrice
	{
		get
		{
			return DealerPrice__BackingField;
		}
		set
		{
			DealerPrice__BackingField = value;
		}
	}

	public string Dealer
	{
		get
		{
			return Dealer__BackingField;
		}
		set
		{
			Dealer__BackingField = value;
		}
	}

	public int AgeAtLastRestore
	{
		get
		{
			return AgeAtLastRestore__BackingField;
		}
		set
		{
			AgeAtLastRestore__BackingField = value;
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

	public float MuseumCondition
	{
		get
		{
			return MuseumCondition__BackingField;
		}
		set
		{
			MuseumCondition__BackingField = value;
		}
	}

	public bool HasGottenFromParent
	{
		get
		{
			return HasGottenFromParent__BackingField;
		}
		set
		{
			HasGottenFromParent__BackingField = value;
		}
	}

	public int NumAnnualMaintenancesPerformed
	{
		get
		{
			return NumAnnualMaintenancesPerformed__BackingField;
		}
		set
		{
			NumAnnualMaintenancesPerformed__BackingField = value;
		}
	}

	public SimLoan Loan
	{
		get
		{
			return Loan__BackingField;
		}
		set
		{
			Loan__BackingField = value;
		}
	}

	public SimRecreationalVehicle(RECREATIONAL_VEHICLE_TYPE vehicleType, string dealer = null)
	{
	}

	public SimRecreationalVehicle(string make, string model, string dealer)
	{
	}

	public void IncrementAge()
	{
	}

	public void ApplyGoldenWrenchEffect()
	{
	}

	public void SetCondition(float condition)
	{
	}

	public float GetCondition()
	{
		return 0f;
	}

	public void ImpactCondition(int impact)
	{
	}

	public string GenericDescriptionString()
	{
		return null;
	}

	public string MakeAndModelString()
	{
		return null;
	}

	public string TypeString()
	{
		return null;
	}

	public string ColorString()
	{
		return null;
	}

	public float VehicleValue()
	{
		return 0f;
	}

	public float VehicleEstimatedValue()
	{
		return 0f;
	}

	public float MonthlyExpense()
	{
		return 0f;
	}

	public int GetPerformanceScore()
	{
		return 0;
	}

	public int GetLoanRejectionReason()
	{
		return 0;
	}

	public void OriginateLoanForVehicle()
	{
	}

	public float VehicleRepairCost()
	{
		return 0f;
	}

	private string GetColor(int color)
	{
		return null;
	}

	private int LifespanYears()
	{
		return 0;
	}

	public int GetCoolScore()
	{
		return 0;
	}
}
