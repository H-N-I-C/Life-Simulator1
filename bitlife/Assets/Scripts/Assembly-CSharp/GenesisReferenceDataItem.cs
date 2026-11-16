using System.Collections.Generic;

public class GenesisReferenceDataItem
{
	private string TagID__BackingField;

	private string Value__BackingField;

	private string CategorizationA__BackingField;

	private string CategorizationB__BackingField;

	private string CategorizationC__BackingField;

	private Dictionary<string, string> Data__BackingField;

	public string TagID
	{
		get
		{
			return TagID__BackingField;
		}
		set
		{
			TagID__BackingField = value;
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

	public string CategorizationA
	{
		get
		{
			return CategorizationA__BackingField;
		}
		set
		{
			CategorizationA__BackingField = value;
		}
	}

	public string CategorizationB
	{
		get
		{
			return CategorizationB__BackingField;
		}
		set
		{
			CategorizationB__BackingField = value;
		}
	}

	public string CategorizationC
	{
		get
		{
			return CategorizationC__BackingField;
		}
		set
		{
			CategorizationC__BackingField = value;
		}
	}

	public Dictionary<string, string> Data
	{
		get
		{
			return Data__BackingField;
		}
		set
		{
			Data__BackingField = value;
		}
	}
}
