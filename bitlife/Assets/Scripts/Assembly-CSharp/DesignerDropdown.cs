using System;

[Serializable]
public class DesignerDropdown
{
	private string Title__BackingField;

	private string TagId__BackingField;

	private DropdownPopulation Population__BackingField;

	private DesignerEntityDropdownPopulation EntityPopulation__BackingField;

	private string WhenToApplyCosts__BackingField;

	private bool SortAlphabetically__BackingField;

	public string Title
	{
		get
		{
			return Title__BackingField;
		}
		set
		{
			Title__BackingField = value;
		}
	}

	public string TagId
	{
		get
		{
			return TagId__BackingField;
		}
		set
		{
			TagId__BackingField = value;
		}
	}

	public DropdownPopulation Population
	{
		get
		{
			return Population__BackingField;
		}
		set
		{
			Population__BackingField = value;
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

	public string WhenToApplyCosts
	{
		get
		{
			return WhenToApplyCosts__BackingField;
		}
		set
		{
			WhenToApplyCosts__BackingField = value;
		}
	}

	public bool SortAlphabetically
	{
		get
		{
			return SortAlphabetically__BackingField;
		}
		set
		{
			SortAlphabetically__BackingField = value;
		}
	}
}
