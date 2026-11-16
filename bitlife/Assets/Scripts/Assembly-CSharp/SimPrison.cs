using System;
using System.Collections;

[Serializable]
public class SimPrison
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, string> _003C_003E9__42_0;

		internal string _003CRefreshAvailablePrisonJobs_003Eb__42_0(string str)
		{
			return null;
		}
	}

	public static int kNumFellowPrisoners;

	public static int kNumFellowPrisonersJuvenile;

	private SimCompany Company__BackingField;

	private ArrayList AvailablePrisonJobNames__BackingField;

	private string _PrisonJob__BackingField;

	private int YearsWorkedCurrentJob__BackingField;

	private ArrayList _FellowPrisonerArray__BackingField;

	private int AgeAtLastFellowPrisonerRefresh__BackingField;

	private SimPrisonGang Gang__BackingField;

	private int YearsOfGuardProtectionLeft__BackingField;

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

	public ArrayList AvailablePrisonJobNames
	{
		get
		{
			return AvailablePrisonJobNames__BackingField;
		}
		set
		{
			AvailablePrisonJobNames__BackingField = value;
		}
	}

	public string _PrisonJob
	{
		get
		{
			return _PrisonJob__BackingField;
		}
		set
		{
			_PrisonJob__BackingField = value;
		}
	}

	public DataPrisonJob PrisonJob
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int YearsWorkedCurrentJob
	{
		get
		{
			return YearsWorkedCurrentJob__BackingField;
		}
		set
		{
			YearsWorkedCurrentJob__BackingField = value;
		}
	}

	private ArrayList _FellowPrisonerArray
	{
		get
		{
			return _FellowPrisonerArray__BackingField;
		}
		set
		{
			_FellowPrisonerArray__BackingField = value;
		}
	}

	public ArrayList FellowPrisonerArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int AgeAtLastFellowPrisonerRefresh
	{
		get
		{
			return AgeAtLastFellowPrisonerRefresh__BackingField;
		}
		set
		{
			AgeAtLastFellowPrisonerRefresh__BackingField = value;
		}
	}

	public SimPrisonGang Gang
	{
		get
		{
			return Gang__BackingField;
		}
		set
		{
			Gang__BackingField = value;
		}
	}

	public int YearsOfGuardProtectionLeft
	{
		get
		{
			return YearsOfGuardProtectionLeft__BackingField;
		}
		set
		{
			YearsOfGuardProtectionLeft__BackingField = value;
		}
	}

	public int SecurityLevel()
	{
		return 0;
	}

	public string SecurityString()
	{
		return null;
	}

	public void RefreshAvailablePrisonJobs()
	{
	}

	public void RefreshFellowPrisoners()
	{
	}

	public SimPrisoner GetAPrisonBully()
	{
		return null;
	}

	public SimPrisoner GetARandomPrisonerForEncounter()
	{
		return null;
	}

	public bool CurrentlyUnderGuardProtection()
	{
		return false;
	}
}
