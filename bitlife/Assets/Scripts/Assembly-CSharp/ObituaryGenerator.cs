using System;
using System.Collections;

[Serializable]
public class ObituaryGenerator
{
	private string ObituaryString__BackingField;

	private ArrayList ObituaryColorArray__BackingField;

	public string ObituaryString
	{
		get
		{
			return ObituaryString__BackingField;
		}
		set
		{
			ObituaryString__BackingField = value;
		}
	}

	public ArrayList ObituaryColorArray
	{
		get
		{
			return ObituaryColorArray__BackingField;
		}
		set
		{
			ObituaryColorArray__BackingField = value;
		}
	}

	public static void AddColorString(string s)
	{
	}

	public string GetObituaryString()
	{
		return null;
	}

	public string GenerateObituary()
	{
		return null;
	}
}
