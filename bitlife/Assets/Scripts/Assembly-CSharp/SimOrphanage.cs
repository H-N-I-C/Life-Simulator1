using System;

[Serializable]
public class SimOrphanage
{
	private SimCompany Company__BackingField;

	private int AgeAtOrphaning__BackingField;

	private int YearsInOrphanage__BackingField;

	private float Att_love__BackingField;

	public SimCompany Company
	{
		get
		{
			return Company__BackingField;
		}
		set
		{
			Company__BackingField = value;
		}
	}

	public int AgeAtOrphaning
	{
		get
		{
			return AgeAtOrphaning__BackingField;
		}
		set
		{
			AgeAtOrphaning__BackingField = value;
		}
	}

	public int YearsInOrphanage
	{
		get
		{
			return YearsInOrphanage__BackingField;
		}
		set
		{
			YearsInOrphanage__BackingField = value;
		}
	}

	public float Att_love
	{
		get
		{
			return Att_love__BackingField;
		}
		set
		{
			Att_love__BackingField = value;
		}
	}

	private SimOrphanage()
	{
	}

	public SimOrphanage(bool initHere)
	{
	}

	public void IncrementYearsAtOrphanage()
	{
	}

	public void ImpactLove(int impactScore)
	{
	}
}
