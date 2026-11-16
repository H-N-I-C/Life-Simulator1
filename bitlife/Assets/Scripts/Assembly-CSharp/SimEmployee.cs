using System;

[Serializable]
public class SimEmployee : SimPerson
{
	private int IndustryEnum__BackingField;

	private string PositionString__BackingField;

	private float InitialImpression__BackingField;

	private int YearsInCompany__BackingField;

	private int NumTimesYelledAt__BackingField;

	private int NumTimesWarned__BackingField;

	private int HeroAgeAtLastInteracted__BackingField;

	private int HeroAgeAtLastBonus__BackingField;

	private int HeroAgeAtLastRecognition__BackingField;

	private float Att_morale__BackingField;

	private float Att_competence__BackingField;

	public DataEmployeeType EmployeeType
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int IndustryEnum
	{
		get
		{
			return IndustryEnum__BackingField;
		}
		set
		{
			IndustryEnum__BackingField = value;
		}
	}

	public string PositionString
	{
		get
		{
			return PositionString__BackingField;
		}
		set
		{
			PositionString__BackingField = value;
		}
	}

	public float InitialImpression
	{
		get
		{
			return InitialImpression__BackingField;
		}
		set
		{
			InitialImpression__BackingField = value;
		}
	}

	public int YearsInCompany
	{
		get
		{
			return YearsInCompany__BackingField;
		}
		set
		{
			YearsInCompany__BackingField = value;
		}
	}

	public int NumTimesYelledAt
	{
		get
		{
			return NumTimesYelledAt__BackingField;
		}
		set
		{
			NumTimesYelledAt__BackingField = value;
		}
	}

	public int NumTimesWarned
	{
		get
		{
			return NumTimesWarned__BackingField;
		}
		set
		{
			NumTimesWarned__BackingField = value;
		}
	}

	public int HeroAgeAtLastInteracted
	{
		get
		{
			return HeroAgeAtLastInteracted__BackingField;
		}
		set
		{
			HeroAgeAtLastInteracted__BackingField = value;
		}
	}

	public int HeroAgeAtLastBonus
	{
		get
		{
			return HeroAgeAtLastBonus__BackingField;
		}
		set
		{
			HeroAgeAtLastBonus__BackingField = value;
		}
	}

	public int HeroAgeAtLastRecognition
	{
		get
		{
			return HeroAgeAtLastRecognition__BackingField;
		}
		set
		{
			HeroAgeAtLastRecognition__BackingField = value;
		}
	}

	public float Att_morale
	{
		get
		{
			return Att_morale__BackingField;
		}
		set
		{
			Att_morale__BackingField = value;
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

	public SimEmployee(bool init)
	{
	}

	public override void IncrementAge()
	{
	}

	public void ImpactCompetence(int impactScore)
	{
	}

	public void ImpactMorale(int impactScore)
	{
	}
}
