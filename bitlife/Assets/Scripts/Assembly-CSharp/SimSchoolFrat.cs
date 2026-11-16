using System;

[Serializable]
public class SimSchoolFrat
{
	private int _FratIndex;

	private int Status__BackingField;

	private int YearsInFrat__BackingField;

	private int AgeAtLastRejection__BackingField;

	public DataSchoolFrat FratData
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int Status
	{
		get
		{
			return Status__BackingField;
		}
		set
		{
			Status__BackingField = value;
		}
	}

	public int YearsInFrat
	{
		get
		{
			return YearsInFrat__BackingField;
		}
		set
		{
			YearsInFrat__BackingField = value;
		}
	}

	public int AgeAtLastRejection
	{
		get
		{
			return AgeAtLastRejection__BackingField;
		}
		set
		{
			AgeAtLastRejection__BackingField = value;
		}
	}

	public SimSchoolFrat(DataSchoolFrat theFratData)
	{
	}

	public void ImpactRelationshipWithOtherClassmatesOnJoin()
	{
	}

	public void ImpactRelationshipWithOtherClassmatesOnLeave()
	{
	}
}
