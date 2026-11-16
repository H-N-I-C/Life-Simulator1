using System;
using System.Collections.Generic;

[Serializable]
public class DropdownCondition
{
	private string TagID__BackingField;

	private Dictionary<string, string> Match__BackingField;

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

	public Dictionary<string, string> Match
	{
		get
		{
			return Match__BackingField;
		}
		set
		{
			Match__BackingField = value;
		}
	}
}
