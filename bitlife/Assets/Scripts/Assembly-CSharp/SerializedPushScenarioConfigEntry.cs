using System;

[Serializable]
public class SerializedPushScenarioConfigEntry : IPushScenarioConfigEntry
{
	private string scenarioCategory;

	private int priority;

	private int minDraws;

	private int maxDraws;

	private float drawProbability;

	private int ageMin;

	private int ageMax;

	public string ScenarioCategory
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int Priority
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public int MinDraws
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public int MaxDraws
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public float DrawProbability
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}

	public int AgeMin
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public int AgeMax
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public override string ToString()
	{
		return null;
	}
}
