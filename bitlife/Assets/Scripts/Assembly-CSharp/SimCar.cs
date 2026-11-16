using System;

[Serializable]
public class SimCar : IComparable
{
	private int Age__BackingField;

	private float MuseumCondition__BackingField;

	private float Condition__BackingField;

	private bool Stolen__BackingField;

	private bool ParentsBought__BackingField;

	private int YearsOwned__BackingField;

	private bool GivenAsIAPUnlock__BackingField;

	private string _Make;

	private string _Model;

	private int Color__BackingField;

	private float DealerPrice__BackingField;

	private SimCompany Dealer__BackingField;

	private SimLoan CarLoan__BackingField;

	private int AgeAtLastRepair__BackingField;

	private int AgeAtLastSellAttempt__BackingField;

	private bool RepossessionWarningInEffect__BackingField;

	private SimCarRepair ActiveRepair__BackingField;

	private bool NeedsToBeScrapped__BackingField;

	private int AgeAtLastMaintenance__BackingField;

	private int NumAnnualMaintenancesPerformed__BackingField;

	private SimCarSpecialFeature SpecialFeature__BackingField;

	private SimRaceCarDetails RaceCarDetails__BackingField;

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

	public bool Stolen
	{
		get
		{
			return Stolen__BackingField;
		}
		set
		{
			Stolen__BackingField = value;
		}
	}

	public bool ParentsBought
	{
		get
		{
			return ParentsBought__BackingField;
		}
		set
		{
			ParentsBought__BackingField = value;
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

	public DataCarModel Model
	{
		get
		{
			return null;
		}
		set
		{
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

	public SimCompany Dealer
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

	public SimLoan CarLoan
	{
		get
		{
			return CarLoan__BackingField;
		}
		set
		{
			CarLoan__BackingField = value;
		}
	}

	public int AgeAtLastRepair
	{
		get
		{
			return AgeAtLastRepair__BackingField;
		}
		set
		{
			AgeAtLastRepair__BackingField = value;
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

	public SimCarRepair ActiveRepair
	{
		get
		{
			return ActiveRepair__BackingField;
		}
		set
		{
			ActiveRepair__BackingField = value;
		}
	}

	public bool NeedsToBeScrapped
	{
		get
		{
			return NeedsToBeScrapped__BackingField;
		}
		set
		{
			NeedsToBeScrapped__BackingField = value;
		}
	}

	public int AgeAtLastMaintenance
	{
		get
		{
			return AgeAtLastMaintenance__BackingField;
		}
		set
		{
			AgeAtLastMaintenance__BackingField = value;
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

	public SimCarSpecialFeature SpecialFeature
	{
		get
		{
			return SpecialFeature__BackingField;
		}
		set
		{
			SpecialFeature__BackingField = value;
		}
	}

	public SimRaceCarDetails RaceCarDetails
	{
		get
		{
			return RaceCarDetails__BackingField;
		}
		set
		{
			RaceCarDetails__BackingField = value;
		}
	}

	public int CompareTo(object obj)
	{
		return 0;
	}

	private SimCar()
	{
	}

	public SimCar(DataCarModel theModel, bool isNew)
	{
	}

	public string GenericDescriptionString()
	{
		return null;
	}

	public string GetMakeAndModel()
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

	public void IncrementAge()
	{
	}

	public float CarValue()
	{
		return 0f;
	}

	public float CarEstimatedValue()
	{
		return 0f;
	}

	public float MonthlyExpense()
	{
		return 0f;
	}

	public void ImpactCondition(int impact)
	{
	}

	public void ApplyGoldenWrenchEffect()
	{
	}

	private int ColorForStockOrFormulaCar()
	{
		return 0;
	}

	public int CarLoanRejectionReason()
	{
		return 0;
	}

	public void OriginateLoanForCar()
	{
	}

	public bool IsRepairNeededAfterCheckForAnnualRepair()
	{
		return false;
	}

	public float RepairCostForAccidentSeriousness(int accidentSeriousnessScore)
	{
		return 0f;
	}

	public bool GetTheftAttemptOutcome()
	{
		return false;
	}
}
