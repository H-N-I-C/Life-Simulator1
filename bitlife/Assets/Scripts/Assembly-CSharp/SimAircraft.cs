using System;

[Serializable]
public class SimAircraft : IComparable
{
	private int Age__BackingField;

	private float MuseumCondition__BackingField;

	private float Condition__BackingField;

	private int YearsOwned__BackingField;

	private int _DataIndex;

	private SimLoan Loan__BackingField;

	private int AgeAtLastRepair__BackingField;

	private int AgeAtLastSellAttempt__BackingField;

	private bool RepossessionWarningInEffect__BackingField;

	private SimAircraftRepair ActiveRepair__BackingField;

	private bool NeedsToBeScrapped__BackingField;

	private int AgeAtLastMaintenance__BackingField;

	private int NumAnnualMaintenancesPerformed__BackingField;

	private int AgeAtLastOuting__BackingField;

	private SimCarSpecialFeature SpecialFeature__BackingField;

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

	public DataAircraft Data
	{
		get
		{
			return null;
		}
		set
		{
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

	public SimAircraftRepair ActiveRepair
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

	public int AgeAtLastOuting
	{
		get
		{
			return AgeAtLastOuting__BackingField;
		}
		set
		{
			AgeAtLastOuting__BackingField = value;
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

	public int CompareTo(object otherObject)
	{
		return 0;
	}

	public SimAircraft(DataAircraft theData, bool isNew)
	{
	}

	public string GenericDescriptionString()
	{
		return null;
	}

	public void IncrementAge()
	{
	}

	public float CurrentValue()
	{
		return 0f;
	}

	public float EstimatedValue()
	{
		return 0f;
	}

	public float MonthlyExpense()
	{
		return 0f;
	}

	public float DealerPrice()
	{
		return 0f;
	}

	public float ScrapValue()
	{
		return 0f;
	}

	public int LoanRejectionReason()
	{
		return 0;
	}

	public void OriginateLoanForSalePrice(float price)
	{
	}

	public bool IsRepairNeededAfterCheckForAnnualRepair()
	{
		return false;
	}

	public void ApplyGoldenWrenchEffect()
	{
	}
}
