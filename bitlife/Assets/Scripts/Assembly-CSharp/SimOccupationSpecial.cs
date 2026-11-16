using System;

[Serializable]
public class SimOccupationSpecial : SimOccupation
{
	private int YearsCompleted__BackingField;

	private SimSpecialJob Job__BackingField;

	private int TerminationType__BackingField;

	private int AgeAtLastReturnFromPrison__BackingField;

	private int AgeAtTermination__BackingField;

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

	public SimSpecialJob Job
	{
		get
		{
			return Job__BackingField;
		}
		set
		{
			Job__BackingField = value;
		}
	}

	public int TerminationType
	{
		get
		{
			return TerminationType__BackingField;
		}
		set
		{
			TerminationType__BackingField = value;
		}
	}

	public int AgeAtLastReturnFromPrison
	{
		get
		{
			return AgeAtLastReturnFromPrison__BackingField;
		}
		set
		{
			AgeAtLastReturnFromPrison__BackingField = value;
		}
	}

	public int AgeAtTermination
	{
		get
		{
			return AgeAtTermination__BackingField;
		}
		set
		{
			AgeAtTermination__BackingField = value;
		}
	}
}
