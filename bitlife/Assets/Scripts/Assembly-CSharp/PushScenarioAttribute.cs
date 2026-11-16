using System;

public class PushScenarioAttribute : Attribute
{
	private string Category__BackingField;

	private string SubCategory__BackingField;

	private string ID__BackingField;

	public string Category
	{
		get
		{
			return Category__BackingField;
		}
		private set
		{
			Category__BackingField = value;
		}
	}

	public string SubCategory
	{
		get
		{
			return SubCategory__BackingField;
		}
		private set
		{
			SubCategory__BackingField = value;
		}
	}

	public string ID
	{
		get
		{
			return ID__BackingField;
		}
		private set
		{
			ID__BackingField = value;
		}
	}

	public PushScenarioAttribute(string category, string pushScenarioID)
	{
	}

	public PushScenarioAttribute(string pushScenarioCategory, string pushScenarioSubCategory, string pushScenarioID)
	{
	}
}
