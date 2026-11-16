using System;

[Serializable]
public class SimAstronautSkills
{
	private float Att_fitness__BackingField;

	private float Att_flight__BackingField;

	private float Att_technical__BackingField;

	private float Att_reputation__BackingField;

	private int HeroAgeAtLastTrainFitness__BackingField;

	private int HeroAgeAtLastTrainTechnical__BackingField;

	private int HeroAgeAtLastTrainFlight__BackingField;

	public float Att_fitness
	{
		get
		{
			return Att_fitness__BackingField;
		}
		set
		{
			Att_fitness__BackingField = value;
		}
	}

	public float Att_flight
	{
		get
		{
			return Att_flight__BackingField;
		}
		set
		{
			Att_flight__BackingField = value;
		}
	}

	public float Att_technical
	{
		get
		{
			return Att_technical__BackingField;
		}
		set
		{
			Att_technical__BackingField = value;
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

	public int HeroAgeAtLastTrainFitness
	{
		get
		{
			return HeroAgeAtLastTrainFitness__BackingField;
		}
		set
		{
			HeroAgeAtLastTrainFitness__BackingField = value;
		}
	}

	public int HeroAgeAtLastTrainTechnical
	{
		get
		{
			return HeroAgeAtLastTrainTechnical__BackingField;
		}
		set
		{
			HeroAgeAtLastTrainTechnical__BackingField = value;
		}
	}

	public int HeroAgeAtLastTrainFlight
	{
		get
		{
			return HeroAgeAtLastTrainFlight__BackingField;
		}
		set
		{
			HeroAgeAtLastTrainFlight__BackingField = value;
		}
	}

	public float GetOverallRating()
	{
		return 0f;
	}

	public void ImpactFitness(int impactScore)
	{
	}

	public void ImpactTechnical(int impactScore)
	{
	}

	public void ImpactFlight(int impactScore)
	{
	}

	public void ImpactReputation(int impactScore)
	{
	}

	public void ImpactOverallSkillsDecay()
	{
	}
}
