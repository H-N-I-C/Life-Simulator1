using System;
using Game;

[Serializable]
public class SimVampireCouncilMember : SimCelebrityLover
{
	public SimVampireCouncilMember(bool initHere)
		: base(initHere: false)
	{
	}

	public override void IncrementAge()
	{
	}

	public override void Die()
	{
	}

	public new virtual string RelationshipStatusString()
	{
		return null;
	}

	public bool WillGiveHeroTimeOfDay()
	{
		return false;
	}
}
