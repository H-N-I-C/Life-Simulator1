using System;

[Serializable]
public class SimZooVeterinarian : SimZooEmployee
{
	public SimZooVeterinarian(bool initHere, bool starterZooEmployee)
		: base(initHere: false)
	{
	}

	public override void IncrementAge()
	{
	}

	public override string RelationshipStatusString()
	{
		return null;
	}
}
