using System;

[Serializable]
public class AdvancedLogicCase
{
	private AdvancedLogicCondition If__BackingField;

	private AdvancedLogicThen Then__BackingField;

	public AdvancedLogicCondition If
	{
		get
		{
			return If__BackingField;
		}
		set
		{
			If__BackingField = value;
		}
	}

	public AdvancedLogicThen Then
	{
		get
		{
			return Then__BackingField;
		}
		set
		{
			Then__BackingField = value;
		}
	}
}
