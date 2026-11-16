using System;

[Serializable]
public class SimSpaceAsteroid : SimDiscovery
{
	private int SizeScore__BackingField;

	public int SizeScore
	{
		get
		{
			return SizeScore__BackingField;
		}
		set
		{
			SizeScore__BackingField = value;
		}
	}
}
