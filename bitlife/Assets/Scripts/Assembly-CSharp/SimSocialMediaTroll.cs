using System;

[Serializable]
public class SimSocialMediaTroll
{
	private string Username__BackingField;

	private SimPerson Person__BackingField;

	private int NumTimesProvokedHero__BackingField;

	private int NumTimesHeroProvoked__BackingField;

	private bool MetHeroInPerson__BackingField;

	private bool AttackedHero__BackingField;

	private bool HeroAttacked__BackingField;

	public string Username
	{
		get
		{
			return Username__BackingField;
		}
		set
		{
			Username__BackingField = value;
		}
	}

	public SimPerson Person
	{
		get
		{
			return Person__BackingField;
		}
		set
		{
			Person__BackingField = value;
		}
	}

	public int NumTimesProvokedHero
	{
		get
		{
			return NumTimesProvokedHero__BackingField;
		}
		set
		{
			NumTimesProvokedHero__BackingField = value;
		}
	}

	public int NumTimesHeroProvoked
	{
		get
		{
			return NumTimesHeroProvoked__BackingField;
		}
		set
		{
			NumTimesHeroProvoked__BackingField = value;
		}
	}

	public bool MetHeroInPerson
	{
		get
		{
			return MetHeroInPerson__BackingField;
		}
		set
		{
			MetHeroInPerson__BackingField = value;
		}
	}

	public bool AttackedHero
	{
		get
		{
			return AttackedHero__BackingField;
		}
		set
		{
			AttackedHero__BackingField = value;
		}
	}

	public bool HeroAttacked
	{
		get
		{
			return HeroAttacked__BackingField;
		}
		set
		{
			HeroAttacked__BackingField = value;
		}
	}

	public string GetUsername()
	{
		return null;
	}
}
