using System;

[Serializable]
public class SimAstronaut : SimLover
{
	private float Att_professionalism__BackingField;

	public bool IsFullAstronaut;

	public bool IsInstructor;

	public float Att_grades;

	public float Att_popularity;

	public int HeroAgeAtLastInsultHero;

	public string LastInsultToHero;

	public bool EverBulliedHero;

	public int HeroAgeAtLastAssaultHero;

	public int HeroAgeAtLastInsulted;

	public int Rank;

	public float Att_professionalism
	{
		get
		{
			return Att_professionalism__BackingField;
		}
		set
		{
			Att_professionalism__BackingField = value;
		}
	}

	public SimAstronaut(bool initHere)
		: base(initHere: false)
	{
	}

	public override void IncrementAge()
	{
	}

	public string GetTypeStringAndCapitalize(bool isSupervisor)
	{
		return null;
	}

	public bool WillGiveHeroTimeOfDay()
	{
		return false;
	}

	public void ImpactPopularity(int impactScore)
	{
	}

	public string RelationshipStatusString(bool shortTitle = false)
	{
		return null;
	}

	public string GetRankString()
	{
		return null;
	}
}
