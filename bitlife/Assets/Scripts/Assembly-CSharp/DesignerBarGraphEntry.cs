using System;

[Serializable]
public class DesignerBarGraphEntry
{
	private int Row__BackingField;

	private string Key__BackingField;

	private string Value__BackingField;

	private string BarGraphStyle__BackingField;

	public int Row
	{
		get
		{
			return Row__BackingField;
		}
		set
		{
			Row__BackingField = value;
		}
	}

	public string Key
	{
		get
		{
			return Key__BackingField;
		}
		set
		{
			Key__BackingField = value;
		}
	}

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

	public string BarGraphStyle
	{
		get
		{
			return BarGraphStyle__BackingField;
		}
		set
		{
			BarGraphStyle__BackingField = value;
		}
	}
}
