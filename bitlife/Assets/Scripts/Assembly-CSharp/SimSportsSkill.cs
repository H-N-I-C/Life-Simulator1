using System;

[Serializable]
public class SimSportsSkill
{
	private string Skill__BackingField;

	private int AgeAtLastPractice__BackingField;

	private float CachedSkillAttributeBeforeSteroids__BackingField;

	private float Att_skill__BackingField;

	public string Skill
	{
		get
		{
			return Skill__BackingField;
		}
		set
		{
			Skill__BackingField = value;
		}
	}

	public int AgeAtLastPractice
	{
		get
		{
			return AgeAtLastPractice__BackingField;
		}
		set
		{
			AgeAtLastPractice__BackingField = value;
		}
	}

	public float CachedSkillAttributeBeforeSteroids
	{
		get
		{
			return CachedSkillAttributeBeforeSteroids__BackingField;
		}
		set
		{
			CachedSkillAttributeBeforeSteroids__BackingField = value;
		}
	}

	public float Att_skill
	{
		get
		{
			return Att_skill__BackingField;
		}
		set
		{
			Att_skill__BackingField = value;
		}
	}

	public void ImpactSkill(int impactScore)
	{
	}
}
