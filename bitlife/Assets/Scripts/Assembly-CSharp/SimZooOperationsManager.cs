using System;

[Serializable]
public class SimZooOperationsManager : SimZooEmployee
{
	private int NumEventHostingsAvailableThisYear__BackingField;

	public int NumEventHostingsAvailableThisYear
	{
		get
		{
			return NumEventHostingsAvailableThisYear__BackingField;
		}
		set
		{
			NumEventHostingsAvailableThisYear__BackingField = value;
		}
	}

	public SimZooOperationsManager(bool initHere, bool starterZooEmployee)
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
