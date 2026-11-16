using System;

[Serializable]
public class AssetAdviceDict
{
	private int AgeAtLastRejected__BackingField;

	private int AgeAtLastGaveAdvice__BackingField;

	private string AdviceString__BackingField;

	private string RejectionString__BackingField;

	public int AgeAtLastRejected
	{
		get
		{
			return AgeAtLastRejected__BackingField;
		}
		set
		{
			AgeAtLastRejected__BackingField = value;
		}
	}

	public int AgeAtLastGaveAdvice
	{
		get
		{
			return AgeAtLastGaveAdvice__BackingField;
		}
		set
		{
			AgeAtLastGaveAdvice__BackingField = value;
		}
	}

	public string AdviceString
	{
		get
		{
			return AdviceString__BackingField;
		}
		set
		{
			AdviceString__BackingField = value;
		}
	}

	public string RejectionString
	{
		get
		{
			return RejectionString__BackingField;
		}
		set
		{
			RejectionString__BackingField = value;
		}
	}
}
