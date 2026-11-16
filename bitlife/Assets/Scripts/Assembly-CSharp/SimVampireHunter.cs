using System;

[Serializable]
public class SimVampireHunter : SimLover
{
	public SimVampireHunter(bool initHere)
		: base(initHere: false)
	{
	}

	public SimVampireHunter(SimPerson otherPerson)
		: base(initHere: false)
	{
	}

	public override void IncrementAge()
	{
	}

	public override void Die()
	{
	}

	public override string OccupationString()
	{
		return null;
	}
}
