using System;

[Serializable]
public class SimVampireProgeny : IComparable<SimVampireProgeny>
{
	private float QualityScore__BackingField;

	private SimPerson VampirePerson__BackingField;

	public float QualityScore
	{
		get
		{
			return QualityScore__BackingField;
		}
		set
		{
			QualityScore__BackingField = value;
		}
	}

	public SimPerson VampirePerson
	{
		get
		{
			return VampirePerson__BackingField;
		}
		set
		{
			VampirePerson__BackingField = value;
		}
	}

	public SimVampireProgeny(SimPerson person)
	{
	}

	public int CompareTo(SimVampireProgeny other)
	{
		return 0;
	}

	private static float CalculateProgenyScore(SimPerson person)
	{
		return 0f;
	}
}
