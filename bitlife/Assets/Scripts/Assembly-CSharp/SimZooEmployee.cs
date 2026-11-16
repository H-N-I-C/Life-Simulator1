using System;

[Serializable]
public class SimZooEmployee : SimLover
{
	private double Salary__BackingField;

	private double LastSalaryAdjustment__BackingField;

	private ZOO_EMPLOYEE_TYPE Type__BackingField;

	private int YearsEmployed__BackingField;

	private int HeroAgeAtLastInteractedWithHero__BackingField;

	private int HeroAgeAtLastSalaryAdjustment__BackingField;

	private float Att_competence__BackingField;

	public double Salary
	{
		get
		{
			return Salary__BackingField;
		}
		set
		{
			Salary__BackingField = value;
		}
	}

	public double LastSalaryAdjustment
	{
		get
		{
			return LastSalaryAdjustment__BackingField;
		}
		set
		{
			LastSalaryAdjustment__BackingField = value;
		}
	}

	public ZOO_EMPLOYEE_TYPE Type
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

	public int YearsEmployed
	{
		get
		{
			return YearsEmployed__BackingField;
		}
		set
		{
			YearsEmployed__BackingField = value;
		}
	}

	public int HeroAgeAtLastInteractedWithHero
	{
		get
		{
			return HeroAgeAtLastInteractedWithHero__BackingField;
		}
		set
		{
			HeroAgeAtLastInteractedWithHero__BackingField = value;
		}
	}

	public int HeroAgeAtLastSalaryAdjustment
	{
		get
		{
			return HeroAgeAtLastSalaryAdjustment__BackingField;
		}
		set
		{
			HeroAgeAtLastSalaryAdjustment__BackingField = value;
		}
	}

	public float Att_competence
	{
		get
		{
			return Att_competence__BackingField;
		}
		set
		{
			Att_competence__BackingField = value;
		}
	}

	public SimZooEmployee(bool initHere, bool starterZooEmployee = false)
		: base(initHere: false)
	{
	}

	public SimZooEmployee(SimPerson otherPerson)
		: base(initHere: false)
	{
	}

	public override void IncrementAge()
	{
	}

	public override void Die()
	{
	}

	public new virtual string RelationshipStatusString()
	{
		return null;
	}

	public float GetCompetence()
	{
		return 0f;
	}

	public void ImpactCompetence(int impactScore)
	{
	}

	public static ZOO_EMPLOYEE_TYPE EmployeeTypeForString(string employeeString)
	{
		return ZOO_EMPLOYEE_TYPE.NONE;
	}

	public static string StringForEmployeeType(ZOO_EMPLOYEE_TYPE type)
	{
		return null;
	}

	public static double SalaryLimitForEmployeeType(ZOO_EMPLOYEE_TYPE type, bool minSalary)
	{
		return 0.0;
	}
}
