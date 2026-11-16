using System;

[Serializable]
public class LifeLogIntraYear
{
	private int Age__BackingField;

	private int Month__BackingField;

	public int Age
	{
		get
		{
			return Age__BackingField;
		}
		set
		{
			Age__BackingField = value;
		}
	}

	public int Month
	{
		get
		{
			return Month__BackingField;
		}
		set
		{
			Month__BackingField = value;
		}
	}

	public string Description()
	{
		return null;
	}
}
