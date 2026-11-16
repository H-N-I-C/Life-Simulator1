using System;

[Serializable]
public class SimBookOpportunity
{
	private string PublisherName__BackingField;

	private float Cost__BackingField;

	private string BookTitle__BackingField;

	private float Proceeds__BackingField;

	private int HeroAge__BackingField;

	public string PublisherName
	{
		get
		{
			return PublisherName__BackingField;
		}
		set
		{
			PublisherName__BackingField = value;
		}
	}

	public float Cost
	{
		get
		{
			return Cost__BackingField;
		}
		set
		{
			Cost__BackingField = value;
		}
	}

	public string BookTitle
	{
		get
		{
			return BookTitle__BackingField;
		}
		set
		{
			BookTitle__BackingField = value;
		}
	}

	public float Proceeds
	{
		get
		{
			return Proceeds__BackingField;
		}
		set
		{
			Proceeds__BackingField = value;
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

	private SimBookOpportunity()
	{
	}

	public SimBookOpportunity(bool initHere)
	{
	}

	public float GetCostAmount()
	{
		return 0f;
	}

	public float GetProceedsAmount()
	{
		return 0f;
	}

	public string OutcomeString()
	{
		return null;
	}

	public static float HeroBookAppealScore()
	{
		return 0f;
	}
}
