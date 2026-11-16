using System;
using System.Collections;

[Serializable]
public class SimAthleteSkills
{
	private int Sport__BackingField;

	private ArrayList _SkillArray__BackingField;

	private int AgeAtLastBalanceTraining__BackingField;

	private int AgeAtLastEnduranceTraining__BackingField;

	private int AgeAtLastFlexibilityTraining__BackingField;

	private int AgeAtLastStrengthTraining__BackingField;

	private float Att_fitness__BackingField;

	private float Att_respect__BackingField;

	public int Sport
	{
		get
		{
			return Sport__BackingField;
		}
		set
		{
			Sport__BackingField = value;
		}
	}

	private ArrayList _SkillArray
	{
		get
		{
			return _SkillArray__BackingField;
		}
		set
		{
			_SkillArray__BackingField = value;
		}
	}

	public ArrayList SkillArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int AgeAtLastBalanceTraining
	{
		get
		{
			return AgeAtLastBalanceTraining__BackingField;
		}
		set
		{
			AgeAtLastBalanceTraining__BackingField = value;
		}
	}

	public int AgeAtLastEnduranceTraining
	{
		get
		{
			return AgeAtLastEnduranceTraining__BackingField;
		}
		set
		{
			AgeAtLastEnduranceTraining__BackingField = value;
		}
	}

	public int AgeAtLastFlexibilityTraining
	{
		get
		{
			return AgeAtLastFlexibilityTraining__BackingField;
		}
		set
		{
			AgeAtLastFlexibilityTraining__BackingField = value;
		}
	}

	public int AgeAtLastStrengthTraining
	{
		get
		{
			return AgeAtLastStrengthTraining__BackingField;
		}
		set
		{
			AgeAtLastStrengthTraining__BackingField = value;
		}
	}

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

	public float Att_respect
	{
		get
		{
			return Att_respect__BackingField;
		}
		set
		{
			Att_respect__BackingField = value;
		}
	}

	public SimAthleteSkills(DataSportsPosition thePosition)
	{
	}

	public float GetGreatnessAttribute()
	{
		return 0f;
	}

	public float GetSkillsAttribute()
	{
		return 0f;
	}

	public void ImpactFitness(int impactScore)
	{
	}

	public void ImpactRespect(int impactScore)
	{
	}
}
