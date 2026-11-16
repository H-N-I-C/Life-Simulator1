using System;

[Serializable]
public class SimModel : SimLover
{
	private float Att_reputation__BackingField;

	private float Att_profile__BackingField;

	private MODEL_CAREER_TYPE ModelType__BackingField;

	private int YearsAtAgency__BackingField;

	private int HeroAgeAtLastSabotage__BackingField;

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

	public float Att_profile
	{
		get
		{
			return Att_profile__BackingField;
		}
		set
		{
			Att_profile__BackingField = value;
		}
	}

	public MODEL_CAREER_TYPE ModelType
	{
		get
		{
			return ModelType__BackingField;
		}
		set
		{
			ModelType__BackingField = value;
		}
	}

	public int YearsAtAgency
	{
		get
		{
			return YearsAtAgency__BackingField;
		}
		set
		{
			YearsAtAgency__BackingField = value;
		}
	}

	public int HeroAgeAtLastSabotage
	{
		get
		{
			return HeroAgeAtLastSabotage__BackingField;
		}
		set
		{
			HeroAgeAtLastSabotage__BackingField = value;
		}
	}

	public SimModel(bool initHere)
		: base(initHere: false)
	{
	}
}
