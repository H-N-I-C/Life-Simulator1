using System;
using System.Collections;

[Serializable]
public class SimAstronautTrainingProgram
{
	private int HeroAgeAtStartTrainingProgram__BackingField;

	private int StatusType__BackingField;

	private bool HeroHasFlightingTraining__BackingField;

	public ArrayList ClassmateArray;

	private float Att_popularity__BackingField;

	public int HeroAgeAtStartTrainingProgram
	{
		get
		{
			return HeroAgeAtStartTrainingProgram__BackingField;
		}
		set
		{
			HeroAgeAtStartTrainingProgram__BackingField = value;
		}
	}

	public int StatusType
	{
		get
		{
			return StatusType__BackingField;
		}
		set
		{
			StatusType__BackingField = value;
		}
	}

	public bool HeroHasFlightingTraining
	{
		get
		{
			return HeroHasFlightingTraining__BackingField;
		}
		set
		{
			HeroHasFlightingTraining__BackingField = value;
		}
	}

	public float Att_popularity
	{
		get
		{
			return Att_popularity__BackingField;
		}
		set
		{
			Att_popularity__BackingField = value;
		}
	}

	public bool IsProgramInProgress()
	{
		return false;
	}

	public int YearsToCompleteTraining()
	{
		return 0;
	}

	public bool IsProgramComplete()
	{
		return false;
	}

	public float AvgPopularityValue()
	{
		return 0f;
	}

	public void ImpactPopularity(int impactScore)
	{
	}
}
