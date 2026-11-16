using System;

[Serializable]
public class SimTalkShowOpportunity
{
	private string TalkShowName__BackingField;

	private int SuccessScore__BackingField;

	private int HeroAge__BackingField;

	public string TalkShowName
	{
		get
		{
			return TalkShowName__BackingField;
		}
		set
		{
			TalkShowName__BackingField = value;
		}
	}

	public int SuccessScore
	{
		get
		{
			return SuccessScore__BackingField;
		}
		set
		{
			SuccessScore__BackingField = value;
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

	private SimTalkShowOpportunity()
	{
	}

	public SimTalkShowOpportunity(int age)
	{
	}

	public static float HeroTalkShowAppealScore()
	{
		return 0f;
	}
}
