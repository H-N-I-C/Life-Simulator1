using System;

[Serializable]
public class DesignerTableValuePartEntry
{
	private string Value__BackingField;

	private string IsVisible__BackingField;

	public string Value
	{
		get
		{
			return Value__BackingField;
		}
		set
		{
			Value__BackingField = value;
		}
	}

	public string IsVisible
	{
		get
		{
			return IsVisible__BackingField;
		}
		set
		{
			IsVisible__BackingField = value;
		}
	}
}
