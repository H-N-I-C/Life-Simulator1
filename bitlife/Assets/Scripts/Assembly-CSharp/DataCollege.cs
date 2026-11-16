using System.Collections;

public class DataCollege
{
	private int SchoolType__BackingField;

	private ArrayList PrerequisiteArray__BackingField;

	private int Years__BackingField;

	private int Tuition__BackingField;

	private int DifficultyScore__BackingField;

	private int CommonnessScore__BackingField;

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

	public ArrayList PrerequisiteArray
	{
		get
		{
			return PrerequisiteArray__BackingField;
		}
		set
		{
			PrerequisiteArray__BackingField = value;
		}
	}

	public int Years
	{
		get
		{
			return Years__BackingField;
		}
		set
		{
			Years__BackingField = value;
		}
	}

	public int Tuition
	{
		get
		{
			return Tuition__BackingField;
		}
		set
		{
			Tuition__BackingField = value;
		}
	}

	public int DifficultyScore
	{
		get
		{
			return DifficultyScore__BackingField;
		}
		set
		{
			DifficultyScore__BackingField = value;
		}
	}

	public int CommonnessScore
	{
		get
		{
			return CommonnessScore__BackingField;
		}
		set
		{
			CommonnessScore__BackingField = value;
		}
	}

	public float TuitionAdjustedForThisLife()
	{
		return 0f;
	}

	public int RequiredEducationalAbilityScoreForAdmission()
	{
		return 0;
	}

	public bool PrerequisiteArrayIncludesMajor(int major)
	{
		return false;
	}
}
