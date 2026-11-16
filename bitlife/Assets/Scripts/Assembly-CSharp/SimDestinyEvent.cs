using System;

[Serializable]
public class SimDestinyEvent
{
	private int Type__BackingField;

	private int Age__BackingField;

	public int Type
	{
		get
		{
			return Type__BackingField;
		}
		set
		{
			Type__BackingField = value;
		}
	}

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

	public string GetEventString()
	{
		return null;
	}
}
