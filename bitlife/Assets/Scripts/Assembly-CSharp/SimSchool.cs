using System;
using System.Collections;

[Serializable]
public class SimSchool
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimClassmate, string> _003C_003E9__82_0;

		public static Func<SimTeacher, string> _003C_003E9__87_0;

		public static Func<DataSchoolActivity, string> _003C_003E9__88_0;

		internal string _003CRefreshClassmates_003Eb__82_0(SimClassmate x)
		{
			return null;
		}

		internal string _003CRefreshTeachers_003Eb__87_0(SimTeacher x)
		{
			return null;
		}

		internal string _003CRefreshAvailableActivities_003Eb__88_0(DataSchoolActivity x)
		{
			return null;
		}
	}

	private const float PrivateSchoolCost = 10000f;

	public static int kNumClassmates;

	private SimCompany Company__BackingField;

	private int SchoolType__BackingField;

	private int SchoolFormat__BackingField;

	private ArrayList _ClassmateArray__BackingField;

	private int AgeAtLastClassRefresh__BackingField;

	private ArrayList _TeacherArray__BackingField;

	private int AgeAtLastTeacherRefresh__BackingField;

	private ArrayList _AvailableActivityIndexArray__BackingField;

	private ArrayList _HeroActivityArray__BackingField;

	private ArrayList _HeroCliqueArray__BackingField;

	private ArrayList _HeroFratArray__BackingField;

	private int TroublePoints__BackingField;

	private int TimesReportedClassmate__BackingField;

	private int TimesReportedTeacher__BackingField;

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

	public int SchoolFormat
	{
		get
		{
			return SchoolFormat__BackingField;
		}
		set
		{
			SchoolFormat__BackingField = value;
		}
	}

	private ArrayList _ClassmateArray
	{
		get
		{
			return _ClassmateArray__BackingField;
		}
		set
		{
			_ClassmateArray__BackingField = value;
		}
	}

	public ArrayList ClassmateArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int AgeAtLastClassRefresh
	{
		get
		{
			return AgeAtLastClassRefresh__BackingField;
		}
		set
		{
			AgeAtLastClassRefresh__BackingField = value;
		}
	}

	private ArrayList _TeacherArray
	{
		get
		{
			return _TeacherArray__BackingField;
		}
		set
		{
			_TeacherArray__BackingField = value;
		}
	}

	public ArrayList TeacherArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int AgeAtLastTeacherRefresh
	{
		get
		{
			return AgeAtLastTeacherRefresh__BackingField;
		}
		set
		{
			AgeAtLastTeacherRefresh__BackingField = value;
		}
	}

	private ArrayList _AvailableActivityIndexArray
	{
		get
		{
			return _AvailableActivityIndexArray__BackingField;
		}
		set
		{
			_AvailableActivityIndexArray__BackingField = value;
		}
	}

	public ArrayList AvailableActivityIndexArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _HeroActivityArray
	{
		get
		{
			return _HeroActivityArray__BackingField;
		}
		set
		{
			_HeroActivityArray__BackingField = value;
		}
	}

	public ArrayList HeroActivityArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _HeroCliqueArray
	{
		get
		{
			return _HeroCliqueArray__BackingField;
		}
		set
		{
			_HeroCliqueArray__BackingField = value;
		}
	}

	public ArrayList HeroCliqueArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _HeroFratArray
	{
		get
		{
			return _HeroFratArray__BackingField;
		}
		set
		{
			_HeroFratArray__BackingField = value;
		}
	}

	public ArrayList HeroFratArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int TroublePoints
	{
		get
		{
			return TroublePoints__BackingField;
		}
		set
		{
			TroublePoints__BackingField = value;
		}
	}

	public int TimesReportedClassmate
	{
		get
		{
			return TimesReportedClassmate__BackingField;
		}
		set
		{
			TimesReportedClassmate__BackingField = value;
		}
	}

	public int TimesReportedTeacher
	{
		get
		{
			return TimesReportedTeacher__BackingField;
		}
		set
		{
			TimesReportedTeacher__BackingField = value;
		}
	}

	public float SchoolCost => 0f;

	public SimSchool(int theType, SimSchool thePreviousSchool, bool overridePublicSchool)
	{
	}

	public SimSchool(int theType, SimSchool thePreviousSchool, SCHOOL_FORMAT format)
	{
	}

	private float CalculateChildSchoolCost()
	{
		return 0f;
	}

	public void IncrementYearsAtSchool()
	{
	}

	public void RefreshClassmates()
	{
	}

	public void UpdateClassmateActivities()
	{
	}

	public int GetNumClassmatesInActivity(DataSchoolActivity activityData)
	{
		return 0;
	}

	public int GetNumClassmatesInClique(DataSchoolClique cliqueData)
	{
		return 0;
	}

	public int GetNumClassmatesInFrat(DataSchoolFrat fratData)
	{
		return 0;
	}

	public void RefreshTeachers()
	{
	}

	public void RefreshAvailableActivities()
	{
	}

	public SimSchoolClique GetCurrentClique()
	{
		return null;
	}

	public SimSchoolFrat GetCurrentFrat()
	{
		return null;
	}

	public SimTeacher GetPrincipal()
	{
		return null;
	}

	public bool DoesPrincipalExist()
	{
		return false;
	}

	public string SchoolFormatString()
	{
		return null;
	}

	public string SchoolDescriptionString()
	{
		return null;
	}
}
