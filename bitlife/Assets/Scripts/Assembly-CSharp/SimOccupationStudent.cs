using System;

[Serializable]
public class SimOccupationStudent : SimOccupation
{
	private SimSchool School__BackingField;

	private int YearsCompleted__BackingField;

	private int YearsLeft__BackingField;

	private int SchoolType__BackingField;

	private int SchoolMajor__BackingField;

	private int SchoolStatus__BackingField;

	private int SchoolFundSource__BackingField;

	private float AnnualTuition__BackingField;

	private SimLoan StudentLoan__BackingField;

	private bool HasHighSchoolScholarship__BackingField;

	private int AgeAtLastHealthyNurseVisit__BackingField;

	private int NumTimesBeggedBeforeExpulsion__BackingField;

	private float Att_grades__BackingField;

	private float Att_popularity__BackingField;

	private bool GraduatedWithGD__BackingField;

	private int AgeLastAttendedSchoolDance__BackingField;

	private int AgeAtLastThrownParty__BackingField;

	private int TimesThrownAPartyThisYear__BackingField;

	private int AgeAtLastSchoolChange__BackingField;

	public SimSchool School
	{
		get
		{
			return School__BackingField;
		}
		set
		{
			School__BackingField = value;
		}
	}

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

	public int YearsLeft
	{
		get
		{
			return YearsLeft__BackingField;
		}
		set
		{
			YearsLeft__BackingField = value;
		}
	}

	public int SchoolType
	{
		get
		{
			return SchoolType__BackingField;
		}
		set
		{
			SchoolType__BackingField = value;
		}
	}

	public int SchoolMajor
	{
		get
		{
			return SchoolMajor__BackingField;
		}
		set
		{
			SchoolMajor__BackingField = value;
		}
	}

	public int SchoolStatus
	{
		get
		{
			return SchoolStatus__BackingField;
		}
		set
		{
			SchoolStatus__BackingField = value;
		}
	}

	public int SchoolFundSource
	{
		get
		{
			return SchoolFundSource__BackingField;
		}
		set
		{
			SchoolFundSource__BackingField = value;
		}
	}

	public float AnnualTuition
	{
		get
		{
			return AnnualTuition__BackingField;
		}
		set
		{
			AnnualTuition__BackingField = value;
		}
	}

	public SimLoan StudentLoan
	{
		get
		{
			return StudentLoan__BackingField;
		}
		set
		{
			StudentLoan__BackingField = value;
		}
	}

	public bool HasHighSchoolScholarship
	{
		get
		{
			return HasHighSchoolScholarship__BackingField;
		}
		set
		{
			HasHighSchoolScholarship__BackingField = value;
		}
	}

	public int AgeAtLastHealthyNurseVisit
	{
		get
		{
			return AgeAtLastHealthyNurseVisit__BackingField;
		}
		set
		{
			AgeAtLastHealthyNurseVisit__BackingField = value;
		}
	}

	public int NumTimesBeggedBeforeExpulsion
	{
		get
		{
			return NumTimesBeggedBeforeExpulsion__BackingField;
		}
		set
		{
			NumTimesBeggedBeforeExpulsion__BackingField = value;
		}
	}

	public float Att_grades
	{
		get
		{
			return Att_grades__BackingField;
		}
		set
		{
			Att_grades__BackingField = value;
		}
	}

	public float Att_popularity
	{
		get
		{
			return Att_popularity__BackingField;
		}
		set
		{
			Att_popularity__BackingField = value;
		}
	}

	public bool GraduatedWithGD
	{
		get
		{
			return GraduatedWithGD__BackingField;
		}
		set
		{
			GraduatedWithGD__BackingField = value;
		}
	}

	public int AgeLastAttendedSchoolDance
	{
		get
		{
			return AgeLastAttendedSchoolDance__BackingField;
		}
		set
		{
			AgeLastAttendedSchoolDance__BackingField = value;
		}
	}

	public int AgeAtLastThrownParty
	{
		get
		{
			return AgeAtLastThrownParty__BackingField;
		}
		set
		{
			AgeAtLastThrownParty__BackingField = value;
		}
	}

	public int TimesThrownAPartyThisYear
	{
		get
		{
			return TimesThrownAPartyThisYear__BackingField;
		}
		set
		{
			TimesThrownAPartyThisYear__BackingField = value;
		}
	}

	public int AgeAtLastSchoolChange
	{
		get
		{
			return AgeAtLastSchoolChange__BackingField;
		}
		set
		{
			AgeAtLastSchoolChange__BackingField = value;
		}
	}

	private SimOccupationStudent()
	{
	}

	public SimOccupationStudent(int theType, SimSchool thePreviousSchool, bool overridePublicSchool = false)
	{
	}

	public SimOccupationStudent(int theType, SimSchool thePreviousSchool, SCHOOL_FORMAT format)
	{
	}

	public void BackwardsCompatibilityDelayedSchoolInit()
	{
	}

	public string SchoolCompletedString()
	{
		return null;
	}

	public string SchoolTypeString()
	{
		return null;
	}

	public bool IsCollegeOrHigher()
	{
		return false;
	}

	public void BeginStudentLoanPayback()
	{
	}

	public void AddAnotherYearOfTuitionToStudentLoan()
	{
	}

	public float GetInitialGradesAttribute()
	{
		return 0f;
	}

	public float GetInitialPopularityAttribute()
	{
		return 0f;
	}

	public void ImpactGrades(int impactScore)
	{
	}

	public void ImpactPopularity(int impactScore)
	{
	}
}
