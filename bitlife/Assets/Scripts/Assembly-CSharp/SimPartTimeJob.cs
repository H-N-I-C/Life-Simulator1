using System;

[Serializable]
public class SimPartTimeJob : IComparable
{
	private int YearsCompleted__BackingField;

	private string _partTimeJobName;

	private SimCompany Company__BackingField;

	private float HourlyRate__BackingField;

	private int WeeklyHours__BackingField;

	private int AgeAtLastRaise__BackingField;

	private int AgeAtLastRaiseRequest__BackingField;

	private int AgeAtLastHoursRequest__BackingField;

	private int AgeAtLastWorkHarder__BackingField;

	private int TerminationType__BackingField;

	public int YearsCompleted
	{
		get
		{
			return YearsCompleted__BackingField;
		}
		set
		{
			YearsCompleted__BackingField = value;
		}
	}

	public DataPartTimeJob Job
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimCompany Company
	{
		get
		{
			return Company__BackingField;
		}
		set
		{
			Company__BackingField = value;
		}
	}

	public float HourlyRate
	{
		get
		{
			return HourlyRate__BackingField;
		}
		set
		{
			HourlyRate__BackingField = value;
		}
	}

	public int WeeklyHours
	{
		get
		{
			return WeeklyHours__BackingField;
		}
		set
		{
			WeeklyHours__BackingField = value;
		}
	}

	public int AgeAtLastRaise
	{
		get
		{
			return AgeAtLastRaise__BackingField;
		}
		set
		{
			AgeAtLastRaise__BackingField = value;
		}
	}

	public int AgeAtLastRaiseRequest
	{
		get
		{
			return AgeAtLastRaiseRequest__BackingField;
		}
		set
		{
			AgeAtLastRaiseRequest__BackingField = value;
		}
	}

	public int AgeAtLastHoursRequest
	{
		get
		{
			return AgeAtLastHoursRequest__BackingField;
		}
		set
		{
			AgeAtLastHoursRequest__BackingField = value;
		}
	}

	public int AgeAtLastWorkHarder
	{
		get
		{
			return AgeAtLastWorkHarder__BackingField;
		}
		set
		{
			AgeAtLastWorkHarder__BackingField = value;
		}
	}

	public int TerminationType
	{
		get
		{
			return TerminationType__BackingField;
		}
		set
		{
			TerminationType__BackingField = value;
		}
	}

	public int HeroQualification()
	{
		return 0;
	}

	public float AnnualizedIncome()
	{
		return 0f;
	}

	public int CompareTo(object obj)
	{
		return 0;
	}
}
