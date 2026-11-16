using System;

[Serializable]
public class ScenarioTimestamp
{
	private string ScenarioName__BackingField;

	private int HeroAge__BackingField;

	public string ScenarioName
	{
		get
		{
			return ScenarioName__BackingField;
		}
		set
		{
			ScenarioName__BackingField = value;
		}
	}

	public int HeroAge
	{
		get
		{
			return HeroAge__BackingField;
		}
		set
		{
			HeroAge__BackingField = value;
		}
	}

	public string Description()
	{
		return null;
	}
}
