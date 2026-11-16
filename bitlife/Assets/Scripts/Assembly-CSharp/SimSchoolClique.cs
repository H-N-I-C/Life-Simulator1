using System;

[Serializable]
public class SimSchoolClique
{
	public string CliqueName;

	private int Status__BackingField;

	private int YearsInClique__BackingField;

	private int GenderOfLeader__BackingField;

	private int AgeAtLastRejection__BackingField;

	public DataSchoolClique CliqueData
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

	public int YearsInClique
	{
		get
		{
			return YearsInClique__BackingField;
		}
		set
		{
			YearsInClique__BackingField = value;
		}
	}

	public int GenderOfLeader
	{
		get
		{
			return GenderOfLeader__BackingField;
		}
		set
		{
			GenderOfLeader__BackingField = value;
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

	public SimSchoolClique(DataSchoolClique theCliqueData)
	{
	}

	public void ImpactRelationshipWithOtherClassmatesOnJoin()
	{
	}

	public void ImpactRelationshipWithOtherClassmatesOnLeave()
	{
	}
}
