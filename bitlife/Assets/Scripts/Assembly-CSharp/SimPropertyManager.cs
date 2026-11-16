using System;

[Serializable]
public class SimPropertyManager : SimPerson
{
	private float Att_competence__BackingField;

	private int HeroAgeAtStartRepresenting__BackingField;

	private int HeroAgeAtStopRepresenting__BackingField;

	public float Att_competence
	{
		get
		{
			return Att_competence__BackingField;
		}
		set
		{
			Att_competence__BackingField = value;
		}
	}

	public int HeroAgeAtStartRepresenting
	{
		get
		{
			return HeroAgeAtStartRepresenting__BackingField;
		}
		set
		{
			HeroAgeAtStartRepresenting__BackingField = value;
		}
	}

	public int HeroAgeAtStopRepresenting
	{
		get
		{
			return HeroAgeAtStopRepresenting__BackingField;
		}
		set
		{
			HeroAgeAtStopRepresenting__BackingField = value;
		}
	}

	public SimPropertyManager(bool initHere)
	{
	}

	public void TerminatePropertyManager()
	{
	}

	public bool WillGiveHeroTimeOfDay()
	{
		return false;
	}
}
