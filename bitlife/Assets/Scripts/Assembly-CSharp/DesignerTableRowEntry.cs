using System;
using System.Collections.Generic;

[Serializable]
public class DesignerTableRowEntry
{
	private int Row__BackingField;

	private string Key__BackingField;

	private string Value__BackingField;

	private string EmptyValue__BackingField;

	private string IsVisible__BackingField;

	private string Separator__BackingField;

	private List<DesignerTableValuePartEntry> ValueParts__BackingField;

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

	public string EmptyValue
	{
		get
		{
			return EmptyValue__BackingField;
		}
		set
		{
			EmptyValue__BackingField = value;
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

	public string Separator
	{
		get
		{
			return Separator__BackingField;
		}
		set
		{
			Separator__BackingField = value;
		}
	}

	public List<DesignerTableValuePartEntry> ValueParts
	{
		get
		{
			return ValueParts__BackingField;
		}
		set
		{
			ValueParts__BackingField = value;
		}
	}
}
