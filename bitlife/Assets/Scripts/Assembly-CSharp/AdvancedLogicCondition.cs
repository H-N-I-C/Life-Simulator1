using System;
using System.Collections.Generic;

[Serializable]
public class AdvancedLogicCondition
{
	private string Expression__BackingField;

	private string Filter__BackingField;

	private DesignerEntityDropdownPopulation EntityPopulation__BackingField;

	private List<DropdownCondition> DropdownConditions__BackingField;

	public string Expression
	{
		get
		{
			return Expression__BackingField;
		}
		set
		{
			Expression__BackingField = value;
		}
	}

	public string Filter
	{
		get
		{
			return Filter__BackingField;
		}
		set
		{
			Filter__BackingField = value;
		}
	}

	public DesignerEntityDropdownPopulation EntityPopulation
	{
		get
		{
			return EntityPopulation__BackingField;
		}
		set
		{
			EntityPopulation__BackingField = value;
		}
	}

	public List<DropdownCondition> DropdownConditions
	{
		get
		{
			return DropdownConditions__BackingField;
		}
		set
		{
			DropdownConditions__BackingField = value;
		}
	}
}
