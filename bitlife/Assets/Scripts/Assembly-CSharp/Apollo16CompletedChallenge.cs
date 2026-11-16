using System;

[Serializable]
public class Apollo16CompletedChallenge
{
	private int ChallengeID__BackingField;

	private string CompletionDate__BackingField;

	private bool Submitted__BackingField;

	private int GlobalPosition__BackingField;

	public int ChallengeID
	{
		get
		{
			return ChallengeID__BackingField;
		}
		set
		{
			ChallengeID__BackingField = value;
		}
	}

	public string CompletionDate
	{
		get
		{
			return CompletionDate__BackingField;
		}
		set
		{
			CompletionDate__BackingField = value;
		}
	}

	public bool Submitted
	{
		get
		{
			return Submitted__BackingField;
		}
		set
		{
			Submitted__BackingField = value;
		}
	}

	public int GlobalPosition
	{
		get
		{
			return GlobalPosition__BackingField;
		}
		set
		{
			GlobalPosition__BackingField = value;
		}
	}
}
