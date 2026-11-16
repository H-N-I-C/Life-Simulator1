using System;

[Serializable]
public class DrawRule
{
	private int NumDraws__BackingField;

	private bool IsConditional__BackingField;

	private DrawCondition Condition__BackingField;

	public int NumDraws
	{
		get
		{
			return NumDraws__BackingField;
		}
		set
		{
			NumDraws__BackingField = value;
		}
	}

	public bool IsConditional
	{
		get
		{
			return IsConditional__BackingField;
		}
		set
		{
			IsConditional__BackingField = value;
		}
	}

	public DrawCondition Condition
	{
		get
		{
			return Condition__BackingField;
		}
		set
		{
			Condition__BackingField = value;
		}
	}
}
