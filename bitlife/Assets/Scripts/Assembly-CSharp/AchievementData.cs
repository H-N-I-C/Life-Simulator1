using System;

[Serializable]
public class AchievementData : IComparable
{
	private bool Earned__BackingField;

	private string Achievement__BackingField;

	private string Name__BackingField;

	private string Description__BackingField;

	public bool Earned
	{
		get
		{
			return Earned__BackingField;
		}
		set
		{
			Earned__BackingField = value;
		}
	}

	public string Achievement
	{
		get
		{
			return Achievement__BackingField;
		}
		set
		{
			Achievement__BackingField = value;
		}
	}

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

	public string Description
	{
		get
		{
			return Description__BackingField;
		}
		set
		{
			Description__BackingField = value;
		}
	}

	public AchievementData(string anAchievement, string aName, string aDescription)
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
