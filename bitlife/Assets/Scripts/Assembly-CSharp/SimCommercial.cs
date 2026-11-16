using System;

[Serializable]
public class SimCommercial
{
	private SimCompany Company__BackingField;

	private float Payment__BackingField;

	private int SuccessScore__BackingField;

	private int HeroAge__BackingField;

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

	public float Payment
	{
		get
		{
			return Payment__BackingField;
		}
		set
		{
			Payment__BackingField = value;
		}
	}

	public int SuccessScore
	{
		get
		{
			return SuccessScore__BackingField;
		}
		set
		{
			SuccessScore__BackingField = value;
		}
	}

	public int HeroAge
	{
		get
		{
			return HeroAge__BackingField;
		}
		set
		{
			HeroAge__BackingField = value;
		}
	}

	public SimCommercial(SimCompany theCompany)
	{
	}

	public float GetPaymentAmount()
	{
		return 0f;
	}

	public static float HeroCommercialAppealScore()
	{
		return 0f;
	}
}
