using System;
using System.Collections.Generic;

[Serializable]
public class AdvancedLogicBlock
{
	private string SetupRoute__BackingField;

	private AdvancedLogicCondition AvailabilityCondition__BackingField;

	private List<AdvancedLogicCase> Cases__BackingField;

	private AdvancedLogicThen ElseWhenNoMatches__BackingField;

	public string SetupRoute
	{
		get
		{
			return SetupRoute__BackingField;
		}
		set
		{
			SetupRoute__BackingField = value;
		}
	}

	public AdvancedLogicCondition AvailabilityCondition
	{
		get
		{
			return AvailabilityCondition__BackingField;
		}
		set
		{
			AvailabilityCondition__BackingField = value;
		}
	}

	public List<AdvancedLogicCase> Cases
	{
		get
		{
			return Cases__BackingField;
		}
		set
		{
			Cases__BackingField = value;
		}
	}

	public AdvancedLogicThen ElseWhenNoMatches
	{
		get
		{
			return ElseWhenNoMatches__BackingField;
		}
		set
		{
			ElseWhenNoMatches__BackingField = value;
		}
	}
}
