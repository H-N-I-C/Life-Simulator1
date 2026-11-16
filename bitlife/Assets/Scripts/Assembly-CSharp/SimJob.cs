using System;
using System.Collections.Generic;

[Serializable]
public class SimJob : IComparable
{
	[NonSerialized]
	private DataCareer _Career;

	private string CareerName__BackingField;

	private int SeniorityTrackPositionZI__BackingField;

	private SimCompany Company__BackingField;

	private bool AlreadyRejected__BackingField;

	private bool AlreadyPassed__BackingField;

	[NonSerialized]
	public DataCountry _Country;

	private string CountryCode__BackingField;

	private int OvertimeHours__BackingField;

	private int AgeAtLastGivenOvertime__BackingField;

	private int AgeLastRequestedOvertime__BackingField;

	public DataCareer Career
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public string CareerName
	{
		get
		{
			return CareerName__BackingField;
		}
		set
		{
			CareerName__BackingField = value;
		}
	}

	public int SeniorityTrackPositionZI
	{
		get
		{
			return SeniorityTrackPositionZI__BackingField;
		}
		set
		{
			SeniorityTrackPositionZI__BackingField = value;
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

	public bool AlreadyRejected
	{
		get
		{
			return AlreadyRejected__BackingField;
		}
		set
		{
			AlreadyRejected__BackingField = value;
		}
	}

	public bool AlreadyPassed
	{
		get
		{
			return AlreadyPassed__BackingField;
		}
		set
		{
			AlreadyPassed__BackingField = value;
		}
	}

	public DataCountry Country
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public string CountryCode
	{
		get
		{
			return CountryCode__BackingField;
		}
		set
		{
			CountryCode__BackingField = value;
		}
	}

	public int OvertimeHours
	{
		get
		{
			return OvertimeHours__BackingField;
		}
		set
		{
			OvertimeHours__BackingField = value;
		}
	}

	public int AgeAtLastGivenOvertime
	{
		get
		{
			return AgeAtLastGivenOvertime__BackingField;
		}
		set
		{
			AgeAtLastGivenOvertime__BackingField = value;
		}
	}

	public int AgeLastRequestedOvertime
	{
		get
		{
			return AgeLastRequestedOvertime__BackingField;
		}
		set
		{
			AgeLastRequestedOvertime__BackingField = value;
		}
	}

	private SimJob()
	{
	}

	public SimJob(DataCountry theCountry)
	{
	}

	public string Description()
	{
		return null;
	}

	public int CompareTo(object otherObject)
	{
		return 0;
	}

	public string JobTitle()
	{
		return null;
	}

	public int Salary()
	{
		return 0;
	}

	public bool IsTheSameAsJob(SimJob anotherJob)
	{
		return false;
	}

	public bool DoesJobMatchCareerAndPositionInGoldenResume(SimJob anotherJob)
	{
		return false;
	}

	public List<JOB_QUALIFICATION> HeroQualificationForEducation()
	{
		return null;
	}

	public int HeroQualification()
	{
		return 0;
	}

	public bool HeroQualificationForGoldenDiploma()
	{
		return false;
	}
}
