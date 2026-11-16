using System;

[Serializable]
public class SimMagazineOpportunity
{
	private string MagazineTitle__BackingField;

	private int PoseType__BackingField;

	private float Payment__BackingField;

	private int SuccessScore__BackingField;

	private int HeroAge__BackingField;

	public string MagazineTitle
	{
		get
		{
			return MagazineTitle__BackingField;
		}
		set
		{
			MagazineTitle__BackingField = value;
		}
	}

	public int PoseType
	{
		get
		{
			return PoseType__BackingField;
		}
		set
		{
			PoseType__BackingField = value;
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

	public SimMagazineOpportunity()
	{
	}

	public SimMagazineOpportunity(int thePoseType)
	{
	}

	public float GetPaymentAmount()
	{
		return 0f;
	}

	public string PoseTypeString()
	{
		return null;
	}

	public string AppearAsString()
	{
		return null;
	}

	public static float HeroMagazineAppealScore()
	{
		return 0f;
	}
}
