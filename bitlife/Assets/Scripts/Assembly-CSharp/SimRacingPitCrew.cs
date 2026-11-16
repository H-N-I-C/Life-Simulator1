using System;

[Serializable]
public class SimRacingPitCrew
{
	private string Name__BackingField;

	private double AnnualCost__BackingField;

	private float Competence__BackingField;

	private int YearsInService__BackingField;

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

	public double AnnualCost
	{
		get
		{
			return AnnualCost__BackingField;
		}
		set
		{
			AnnualCost__BackingField = value;
		}
	}

	public float Competence
	{
		get
		{
			return Competence__BackingField;
		}
		set
		{
			Competence__BackingField = value;
		}
	}

	public int YearsInService
	{
		get
		{
			return YearsInService__BackingField;
		}
		set
		{
			YearsInService__BackingField = value;
		}
	}

	public SimRacingPitCrew(string name, RACING_PIT_CREW_COMPETENCE_LEVEL competenceLevel)
	{
	}
}
