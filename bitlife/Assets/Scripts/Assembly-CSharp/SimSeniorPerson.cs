using System;

[Serializable]
public class SimSeniorPerson : SimLover
{
	public SimSeniorPerson()
		: base(initHere: false)
	{
	}

	public SimSeniorPerson(bool initHere)
		: base(initHere: false)
	{
	}

	public SimSeniorPerson(SimPerson otherPerson)
		: base(initHere: false)
	{
	}

	public string SeniorRelationshipStatusForDisplay()
	{
		return null;
	}
}
