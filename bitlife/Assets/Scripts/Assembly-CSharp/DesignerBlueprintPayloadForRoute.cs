using System;
using System.Collections.Generic;

[Serializable]
public class DesignerBlueprintPayloadForRoute
{
	private string BluePrintID__BackingField;

	private Dictionary<string, string> Payload__BackingField;

	public string BluePrintID
	{
		get
		{
			return BluePrintID__BackingField;
		}
		set
		{
			BluePrintID__BackingField = value;
		}
	}

	public Dictionary<string, string> Payload
	{
		get
		{
			return Payload__BackingField;
		}
		set
		{
			Payload__BackingField = value;
		}
	}
}
