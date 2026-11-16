using System;

[Serializable]
public class Apollo16AchievementData : IComparable
{
	private bool MEarned__BackingField;

	private string MAchievement__BackingField;

	private string MName__BackingField;

	private string MDescription__BackingField;

	public bool MEarned
	{
		get
		{
			return MEarned__BackingField;
		}
		set
		{
			MEarned__BackingField = value;
		}
	}

	public string MAchievement
	{
		get
		{
			return MAchievement__BackingField;
		}
		set
		{
			MAchievement__BackingField = value;
		}
	}

	public string MName
	{
		get
		{
			return MName__BackingField;
		}
		set
		{
			MName__BackingField = value;
		}
	}

	public string MDescription
	{
		get
		{
			return MDescription__BackingField;
		}
		set
		{
			MDescription__BackingField = value;
		}
	}

	public Apollo16AchievementData(string anAchievement, string aName, string aDescription)
	{
	}

	public void SetEarned()
	{
	}

	public int CompareTo(object otherObject)
	{
		return 0;
	}
}
