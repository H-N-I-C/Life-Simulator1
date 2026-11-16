using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SimPast
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimOccupationStudent, int> _003C_003E9__195_1;

		public static Func<SimOccupationStudent, bool> _003C_003E9__195_2;

		public static Predicate<SimOccupationStudent> _003C_003E9__195_3;

		public static Func<SimOccupationStudent, bool> _003C_003E9__195_4;

		public static Func<SimOccupationStudent, bool> _003C_003E9__195_5;

		public static Func<SimOccupationStudent, bool> _003C_003E9__195_6;

		public static Func<SimOccupationStudent, bool> _003C_003E9__195_7;

		public static Func<SimOccupationStudent, int> _003C_003E9__195_0;

		internal int _003CHighestDegreesCompletedList_003Eb__195_1(SimOccupationStudent x)
		{
			return 0;
		}

		internal bool _003CHighestDegreesCompletedList_003Eb__195_2(SimOccupationStudent x)
		{
			return false;
		}

		internal bool _003CHighestDegreesCompletedList_003Eb__195_3(SimOccupationStudent x)
		{
			return false;
		}

		internal bool _003CHighestDegreesCompletedList_003Eb__195_4(SimOccupationStudent x)
		{
			return false;
		}

		internal bool _003CHighestDegreesCompletedList_003Eb__195_5(SimOccupationStudent x)
		{
			return false;
		}

		internal bool _003CHighestDegreesCompletedList_003Eb__195_6(SimOccupationStudent x)
		{
			return false;
		}

		internal bool _003CHighestDegreesCompletedList_003Eb__195_7(SimOccupationStudent x)
		{
			return false;
		}

		internal int _003CHighestDegreesCompletedList_003Eb__195_0(SimOccupationStudent x)
		{
			return 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass195_0
	{
		public List<SimOccupationStudent> highestDegreesList;

		internal bool _003CHighestDegreesCompletedList_003Eb__8(SimOccupationStudent x)
		{
			return false;
		}
	}

	private ArrayList _ExLoverArray__BackingField;

	private ArrayList _ExChildArray__BackingField;

	private ArrayList _ExPetArray__BackingField;

	private ArrayList _ExAgentArray__BackingField;

	private ArrayList _ExDirectorArray__BackingField;

	private ArrayList _ExFinancialAdvisorArray;

	private ArrayList _StudentHistoryArray__BackingField;

	private ArrayList _EmployeeHistoryArray__BackingField;

	private ArrayList _ExPropertyManagerArray__BackingField;

	private ArrayList _ExPropertyTenantArray__BackingField;

	private ArrayList _ExCommunardArray__BackingField;

	private ArrayList _MilitaryHistoryArray__BackingField;

	private ArrayList _PrisonerHistoryArray__BackingField;

	private ArrayList _SpecialHistoryArray__BackingField;

	private ArrayList _PartTimeJobHistoryArray__BackingField;

	private ArrayList _PartTimeFreelanceHistoryArray__BackingField;

	private ArrayList _OpenCrimeArray__BackingField;

	private ArrayList _ArrestHistoryArray__BackingField;

	private ArrayList _DiseaseHistoryArray__BackingField;

	private ArrayList _AddictionHistoryArray__BackingField;

	private ArrayList _EmigrationHistoryArray__BackingField;

	private ArrayList _FameHistoryArray__BackingField;

	private ArrayList _BooksReadArray__BackingField;

	private ArrayList _DrugsTried__BackingField;

	private SimRoyal ExRoyal__BackingField;

	private List<SimSpyAgency> _SpyAgencies;

	private List<SimZoo> _zooList;

	private bool _EverOwnedHome__BackingField;

	private float IntelligenceAtBirth__BackingField;

	private float AppearanceAtBirth__BackingField;

	private ArrayList _ExLoverArray
	{
		get
		{
			return _ExLoverArray__BackingField;
		}
		set
		{
			_ExLoverArray__BackingField = value;
		}
	}

	public ArrayList ExLoverArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _ExChildArray
	{
		get
		{
			return _ExChildArray__BackingField;
		}
		set
		{
			_ExChildArray__BackingField = value;
		}
	}

	public ArrayList ExChildArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _ExPetArray
	{
		get
		{
			return _ExPetArray__BackingField;
		}
		set
		{
			_ExPetArray__BackingField = value;
		}
	}

	public ArrayList ExPetArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _ExAgentArray
	{
		get
		{
			return _ExAgentArray__BackingField;
		}
		set
		{
			_ExAgentArray__BackingField = value;
		}
	}

	public ArrayList ExAgentArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _ExDirectorArray
	{
		get
		{
			return _ExDirectorArray__BackingField;
		}
		set
		{
			_ExDirectorArray__BackingField = value;
		}
	}

	public ArrayList ExDirectorArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList ExFinancialAdvisorArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _StudentHistoryArray
	{
		get
		{
			return _StudentHistoryArray__BackingField;
		}
		set
		{
			_StudentHistoryArray__BackingField = value;
		}
	}

	public ArrayList StudentHistoryArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _EmployeeHistoryArray
	{
		get
		{
			return _EmployeeHistoryArray__BackingField;
		}
		set
		{
			_EmployeeHistoryArray__BackingField = value;
		}
	}

	public ArrayList EmployeeHistoryArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _ExPropertyManagerArray
	{
		get
		{
			return _ExPropertyManagerArray__BackingField;
		}
		set
		{
			_ExPropertyManagerArray__BackingField = value;
		}
	}

	public ArrayList ExPropertyManagerArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _ExPropertyTenantArray
	{
		get
		{
			return _ExPropertyTenantArray__BackingField;
		}
		set
		{
			_ExPropertyTenantArray__BackingField = value;
		}
	}

	public ArrayList ExPropertyTenantArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _ExCommunardArray
	{
		get
		{
			return _ExCommunardArray__BackingField;
		}
		set
		{
			_ExCommunardArray__BackingField = value;
		}
	}

	public ArrayList ExCommunardArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _MilitaryHistoryArray
	{
		get
		{
			return _MilitaryHistoryArray__BackingField;
		}
		set
		{
			_MilitaryHistoryArray__BackingField = value;
		}
	}

	public ArrayList MilitaryHistoryArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _PrisonerHistoryArray
	{
		get
		{
			return _PrisonerHistoryArray__BackingField;
		}
		set
		{
			_PrisonerHistoryArray__BackingField = value;
		}
	}

	public ArrayList PrisonerHistoryArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _SpecialHistoryArray
	{
		get
		{
			return _SpecialHistoryArray__BackingField;
		}
		set
		{
			_SpecialHistoryArray__BackingField = value;
		}
	}

	public ArrayList SpecialHistoryArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _PartTimeJobHistoryArray
	{
		get
		{
			return _PartTimeJobHistoryArray__BackingField;
		}
		set
		{
			_PartTimeJobHistoryArray__BackingField = value;
		}
	}

	public ArrayList PartTimeJobHistoryArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _PartTimeFreelanceHistoryArray
	{
		get
		{
			return _PartTimeFreelanceHistoryArray__BackingField;
		}
		set
		{
			_PartTimeFreelanceHistoryArray__BackingField = value;
		}
	}

	public ArrayList PartTimeFreelanceHistoryArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _OpenCrimeArray
	{
		get
		{
			return _OpenCrimeArray__BackingField;
		}
		set
		{
			_OpenCrimeArray__BackingField = value;
		}
	}

	public ArrayList OpenCrimeArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _ArrestHistoryArray
	{
		get
		{
			return _ArrestHistoryArray__BackingField;
		}
		set
		{
			_ArrestHistoryArray__BackingField = value;
		}
	}

	public ArrayList ArrestHistoryArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _DiseaseHistoryArray
	{
		get
		{
			return _DiseaseHistoryArray__BackingField;
		}
		set
		{
			_DiseaseHistoryArray__BackingField = value;
		}
	}

	public ArrayList DiseaseHistoryArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _AddictionHistoryArray
	{
		get
		{
			return _AddictionHistoryArray__BackingField;
		}
		set
		{
			_AddictionHistoryArray__BackingField = value;
		}
	}

	public ArrayList AddictionHistoryArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _EmigrationHistoryArray
	{
		get
		{
			return _EmigrationHistoryArray__BackingField;
		}
		set
		{
			_EmigrationHistoryArray__BackingField = value;
		}
	}

	public ArrayList EmigrationHistoryArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _FameHistoryArray
	{
		get
		{
			return _FameHistoryArray__BackingField;
		}
		set
		{
			_FameHistoryArray__BackingField = value;
		}
	}

	public ArrayList FameHistoryArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _BooksReadArray
	{
		get
		{
			return _BooksReadArray__BackingField;
		}
		set
		{
			_BooksReadArray__BackingField = value;
		}
	}

	public ArrayList BooksReadArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _DrugsTried
	{
		get
		{
			return _DrugsTried__BackingField;
		}
		set
		{
			_DrugsTried__BackingField = value;
		}
	}

	public ArrayList DrugsTried
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimRoyal ExRoyal
	{
		get
		{
			return ExRoyal__BackingField;
		}
		set
		{
			ExRoyal__BackingField = value;
		}
	}

	public List<SimSpyAgency> SpyAgenciesList
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public List<SimZoo> ZooList
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private bool _EverOwnedHome
	{
		get
		{
			return _EverOwnedHome__BackingField;
		}
		set
		{
			_EverOwnedHome__BackingField = value;
		}
	}

	public bool EverOwnedHome
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public float IntelligenceAtBirth
	{
		get
		{
			return IntelligenceAtBirth__BackingField;
		}
		set
		{
			IntelligenceAtBirth__BackingField = value;
		}
	}

	public float AppearanceAtBirth
	{
		get
		{
			return AppearanceAtBirth__BackingField;
		}
		set
		{
			AppearanceAtBirth__BackingField = value;
		}
	}

	private SimPast()
	{
	}

	public SimPast(bool initHere)
	{
	}

	public SimOccupationStudent HighestSchoolCompleted()
	{
		return null;
	}

	public List<SimOccupationStudent> HighestDegreesCompletedList(bool ensureTwoDegrees = false)
	{
		return null;
	}

	public bool HasCompletedSchoolType(int schoolType)
	{
		return false;
	}

	public int NumCompletedSchoolTypeMin(int schoolType)
	{
		return 0;
	}

	public int NumCompletedSchoolTypeExact(int schoolType)
	{
		return 0;
	}

	public bool HasCompletedPostUniversitySchool()
	{
		return false;
	}

	public int NumCompletedPostUniversitySchool()
	{
		return 0;
	}

	public int GetUndergraduateMajor()
	{
		return 0;
	}

	public List<int> GetUndergraduateMajorList()
	{
		return null;
	}

	public bool HasADoctorate()
	{
		return false;
	}

	public bool HasStemDegree()
	{
		return false;
	}

	public bool HasEverParticipatedInSchoolActivityNameAndHasToBeInCollege(string name, bool inCollege)
	{
		return false;
	}

	public SimSchoolActivity GetMostRecentSchoolActivityForNameAndHasToBeInCollege(string name, bool inCollege)
	{
		return null;
	}

	public int HighestMilitaryEnlistedRankAchieved()
	{
		return 0;
	}

	public int HighestMilitaryOfficerRankAchieved()
	{
		return 0;
	}

	public bool HasEverBeenDishonorablyDischarged()
	{
		return false;
	}

	public bool HasRetiredFromMilitary()
	{
		return false;
	}

	public int MilitaryYearsServed()
	{
		return 0;
	}

	public bool HasCriminalRecordInPlaceOfResidence()
	{
		return false;
	}

	public bool HasCriminalRecordInAnyCountry()
	{
		return false;
	}

	public bool HasCriminalRecordInPlace(SimPlace place)
	{
		return false;
	}

	public ArrayList GetOpenCrimesInPlaceOfResidence()
	{
		return null;
	}

	public ArrayList GetOpenCrimesInPlace(SimPlace place)
	{
		return null;
	}

	public bool IsOnTheRunFromPrisonInPlaceOfResidence()
	{
		return false;
	}

	public bool ShouldGetCapturedWhenInteractingWithPoliceInPlace(SimPlace place)
	{
		return false;
	}

	public void CloseAnyOpenCrimesInPlaceOfResidence()
	{
	}

	public int PrisonYearsServed()
	{
		return 0;
	}

	public int PrisonEscapes()
	{
		return 0;
	}

	public void ClearCriminalRecord()
	{
	}

	public int WorkingExperienceYears()
	{
		return 0;
	}

	public int WorkingExperienceYearsInCareer(DataCareer career)
	{
		return 0;
	}

	public int WorkingExperienceYearsAtCompany(SimCompany company)
	{
		return 0;
	}

	public bool HasAchievedCorporateManagement()
	{
		return false;
	}

	public int PartTimeWorkingExperienceYears()
	{
		return 0;
	}

	public int SpecialExperienceYears()
	{
		return 0;
	}

	public int NumTimesSufferedFromDiseaseType(DataDiseaseType diseaseType)
	{
		return 0;
	}

	public int YearsSinceLastSufferedFromDiseaseType(DataDiseaseType diseaseType)
	{
		return 0;
	}

	public bool HasSufferedFromAddiction()
	{
		return false;
	}

	public bool IsImmigrationLegalInPlace(SimPlace place)
	{
		return false;
	}

	public SimPlace LastLegalPlaceOfResidence()
	{
		return null;
	}

	public ArrayList GetAllPlacesLived()
	{
		return null;
	}

	public bool HasEverBeenExiled()
	{
		return false;
	}

	public bool IsInWitnessProtectionProgram()
	{
		return false;
	}

	public int NumMarriages()
	{
		return 0;
	}

	public bool HasShelteredAPetInPlaceOfResidence()
	{
		return false;
	}
}
