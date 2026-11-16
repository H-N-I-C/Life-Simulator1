using System;
using System.Collections;

[Serializable]
public class SimAgent : SimLover
{
	private int YearsOfExperience__BackingField;

	private float CommissionPercent__BackingField;

	private ArrayList _FamousClientArray__BackingField;

	private float EarningsFromHero__BackingField;

	private int HeroAgeAtStartRepresenting__BackingField;

	private int HeroAgeAtStopRepresenting__BackingField;

	private int HeroAgeAtLastFoundJob__BackingField;

	private int HeroAgeAtLastFindJobFailure__BackingField;

	private float Att_reputation__BackingField;

	private float Att_competence__BackingField;

	public int YearsOfExperience
	{
		get
		{
			return YearsOfExperience__BackingField;
		}
		set
		{
			YearsOfExperience__BackingField = value;
		}
	}

	public float CommissionPercent
	{
		get
		{
			return CommissionPercent__BackingField;
		}
		set
		{
			CommissionPercent__BackingField = value;
		}
	}

	private ArrayList _FamousClientArray
	{
		get
		{
			return _FamousClientArray__BackingField;
		}
		set
		{
			_FamousClientArray__BackingField = value;
		}
	}

	public ArrayList FamousClientArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public float EarningsFromHero
	{
		get
		{
			return EarningsFromHero__BackingField;
		}
		set
		{
			EarningsFromHero__BackingField = value;
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

	public int HeroAgeAtLastFoundJob
	{
		get
		{
			return HeroAgeAtLastFoundJob__BackingField;
		}
		set
		{
			HeroAgeAtLastFoundJob__BackingField = value;
		}
	}

	public int HeroAgeAtLastFindJobFailure
	{
		get
		{
			return HeroAgeAtLastFindJobFailure__BackingField;
		}
		set
		{
			HeroAgeAtLastFindJobFailure__BackingField = value;
		}
	}

	public float Att_reputation
	{
		get
		{
			return Att_reputation__BackingField;
		}
		set
		{
			Att_reputation__BackingField = value;
		}
	}

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

	public SimAgent(bool initHere)
		: base(initHere: false)
	{
	}

	public void SetupForTier(int tier)
	{
	}

	public override void Die()
	{
	}

	public override void IncrementAge()
	{
	}

	public bool WillGiveHeroTimeOfDay()
	{
		return false;
	}

	public void TerminateAgent()
	{
	}

	public void ImpactReputation(int impactScore)
	{
	}
}
