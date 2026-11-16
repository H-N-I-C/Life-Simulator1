using System;
using System.Collections.Generic;

[Serializable]
public class DropdownPopulation
{
	private List<DrawRule> DrawRules__BackingField;

	public List<DrawRule> DrawRules
	{
		get
		{
			return DrawRules__BackingField;
		}
		set
		{
			DrawRules__BackingField = value;
		}
	}
}
