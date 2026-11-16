using System;

[Serializable]
public class SimRaceCarMechanic
{
	private SimPersonName Name__BackingField;

	private int Gender__BackingField;

	private float Reputation__BackingField;

	public SimPersonName Name
	{
		get
		{
			return Name__BackingField;
		}
		set
		{
			Name__BackingField = value;
		}
	}

	public int Gender
	{
		get
		{
			return Gender__BackingField;
		}
		set
		{
			Gender__BackingField = value;
		}
	}

	public float Reputation
	{
		get
		{
			return Reputation__BackingField;
		}
		set
		{
			Reputation__BackingField = value;
		}
	}

	public SimRaceCarMechanic(DataCountry country, bool requireHighQuality)
	{
	}
}
