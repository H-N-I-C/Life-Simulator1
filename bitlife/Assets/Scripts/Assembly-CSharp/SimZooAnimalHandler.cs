using System;

[Serializable]
public class SimZooAnimalHandler : SimZooEmployee
{
	private string HabitatSpecialization__BackingField;

	private int NumAnimalShowsAvailableThisYear__BackingField;

	private int NumGroomingsAvailableThisYear__BackingField;

	public string HabitatSpecialization
	{
		get
		{
			return HabitatSpecialization__BackingField;
		}
		set
		{
			HabitatSpecialization__BackingField = value;
		}
	}

	public int NumAnimalShowsAvailableThisYear
	{
		get
		{
			return NumAnimalShowsAvailableThisYear__BackingField;
		}
		set
		{
			NumAnimalShowsAvailableThisYear__BackingField = value;
		}
	}

	public int NumGroomingsAvailableThisYear
	{
		get
		{
			return NumGroomingsAvailableThisYear__BackingField;
		}
		set
		{
			NumGroomingsAvailableThisYear__BackingField = value;
		}
	}

	public SimZooAnimalHandler(string habitatSpecialization, bool initHere, bool starterZooEmployee)
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

	public string HabitatSpecializationString()
	{
		return null;
	}
}
