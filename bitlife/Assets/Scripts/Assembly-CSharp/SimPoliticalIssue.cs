using System;

[Serializable]
public class SimPoliticalIssue
{
	private int Issue__BackingField;

	private int Hours__BackingField;

	private float Att_demand__BackingField;

	private float Att_approval__BackingField;

	public int Issue
	{
		get
		{
			return Issue__BackingField;
		}
		set
		{
			Issue__BackingField = value;
		}
	}

	public int Hours
	{
		get
		{
			return Hours__BackingField;
		}
		set
		{
			Hours__BackingField = value;
		}
	}

	public float Att_demand
	{
		get
		{
			return Att_demand__BackingField;
		}
		set
		{
			Att_demand__BackingField = value;
		}
	}

	public float Att_approval
	{
		get
		{
			return Att_approval__BackingField;
		}
		set
		{
			Att_approval__BackingField = value;
		}
	}

	public void ImpactApproval(int impactScore)
	{
	}

	public void ImpactDemand(int impactScore)
	{
	}
}
