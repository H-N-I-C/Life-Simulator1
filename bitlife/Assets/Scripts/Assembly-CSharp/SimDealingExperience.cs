using System;

[Serializable]
public class SimDealingExperience
{
	private float Att_skill__BackingField;

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
