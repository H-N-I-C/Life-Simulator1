using System;

[Serializable]
public class SimFightClubCoach : SimLover
{
	private float Quality__BackingField;

	private int YearsOfExperience__BackingField;

	private int YearsWithHero__BackingField;

	private double StreetCredCostToHire__BackingField;

	private bool _updatedThisYear;

	public float Quality
	{
		get
		{
			return Quality__BackingField;
		}
		set
		{
			Quality__BackingField = value;
		}
	}

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

	public int YearsWithHero
	{
		get
		{
			return YearsWithHero__BackingField;
		}
		set
		{
			YearsWithHero__BackingField = value;
		}
	}

	public double StreetCredCostToHire
	{
		get
		{
			return StreetCredCostToHire__BackingField;
		}
		set
		{
			StreetCredCostToHire__BackingField = value;
		}
	}

	public SimFightClubCoach(bool initHere, bool isAutoReplacementCoach = false, int maxQuality = 101)
		: base(initHere: false)
	{
	}

	public override void IncrementAge()
	{
	}

	public override void OnLifeIncrementAgeEnded()
	{
	}

	public override void Die()
	{
	}

	public override string GetRelationshipString()
	{
		return null;
	}

	public string GetJobTitle()
	{
		return null;
	}

	public override Scenario TryGetPostDeathScenario()
	{
		return null;
	}
}
