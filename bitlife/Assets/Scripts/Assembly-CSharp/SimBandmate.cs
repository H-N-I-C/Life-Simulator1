using System;

[Serializable]
public class SimBandmate : SimLover
{
	private int Role__BackingField;

	private string StageName__BackingField;

	public int Role
	{
		get
		{
			return Role__BackingField;
		}
		set
		{
			Role__BackingField = value;
		}
	}

	public string StageName
	{
		get
		{
			return StageName__BackingField;
		}
		set
		{
			StageName__BackingField = value;
		}
	}

	public SimBandmate(bool initHere)
		: base(initHere: false)
	{
	}

	public override void IncrementAge()
	{
	}

	public string GetRoleStringAndCapitalize(bool capitalize)
	{
		return null;
	}

	public string GetStageName()
	{
		return null;
	}
}
