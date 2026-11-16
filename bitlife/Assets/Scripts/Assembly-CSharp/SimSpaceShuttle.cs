using System;

[Serializable]
public class SimSpaceShuttle
{
	private string Name__BackingField;

	private int Risk__BackingField;

	public string Name
	{
		get
		{
			return Name__BackingField;
		}
		set
		{
			Name__BackingField = value;
		}
	}

	public int Risk
	{
		get
		{
			return Risk__BackingField;
		}
		set
		{
			Risk__BackingField = value;
		}
	}

	public static SimSpaceShuttle GenerateShuttle()
	{
		return null;
	}
}
