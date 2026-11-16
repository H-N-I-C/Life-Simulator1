using System;

[Serializable]
public class SimMafiaTarget
{
	private SimCompany Company__BackingField;

	private SimPerson Person__BackingField;

	private int PersonGenderRequirement__BackingField;

	private string RoleString__BackingField;

	private double TotalCashExtorted__BackingField;

	private int AgeAtLastExtortionProcess__BackingField;

	private float Att_money__BackingField;

	private float Att_fear__BackingField;

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

	public SimPerson Person
	{
		get
		{
			return Person__BackingField;
		}
		set
		{
			Person__BackingField = value;
		}
	}

	public int PersonGenderRequirement
	{
		get
		{
			return PersonGenderRequirement__BackingField;
		}
		set
		{
			PersonGenderRequirement__BackingField = value;
		}
	}

	public string RoleString
	{
		get
		{
			return RoleString__BackingField;
		}
		set
		{
			RoleString__BackingField = value;
		}
	}

	public double TotalCashExtorted
	{
		get
		{
			return TotalCashExtorted__BackingField;
		}
		set
		{
			TotalCashExtorted__BackingField = value;
		}
	}

	public int AgeAtLastExtortionProcess
	{
		get
		{
			return AgeAtLastExtortionProcess__BackingField;
		}
		set
		{
			AgeAtLastExtortionProcess__BackingField = value;
		}
	}

	public float Att_money
	{
		get
		{
			return Att_money__BackingField;
		}
		set
		{
			Att_money__BackingField = value;
		}
	}

	public float Att_fear
	{
		get
		{
			return Att_fear__BackingField;
		}
		set
		{
			Att_fear__BackingField = value;
		}
	}

	public float GetMinExtortionAmount()
	{
		return 0f;
	}

	public float GetMaxExtortionAmount()
	{
		return 0f;
	}

	public float GetTargetPersonFear()
	{
		return 0f;
	}

	public void ImpactFear(int impactScore)
	{
	}
}
