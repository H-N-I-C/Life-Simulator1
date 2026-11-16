using System;

[Serializable]
public class SimMusicSoloContract
{
	private SimCompany RecordCompany__BackingField;

	private int Years__BackingField;

	private float RoyaltyPercent__BackingField;

	private int Genre__BackingField;

	private float Advance__BackingField;

	private float AdvanceBalance__BackingField;

	private int HeroAgeAtSigning__BackingField;

	public SimCompany RecordCompany
	{
		get
		{
			return RecordCompany__BackingField;
		}
		set
		{
			RecordCompany__BackingField = value;
		}
	}

	public int Years
	{
		get
		{
			return Years__BackingField;
		}
		set
		{
			Years__BackingField = value;
		}
	}

	public float RoyaltyPercent
	{
		get
		{
			return RoyaltyPercent__BackingField;
		}
		set
		{
			RoyaltyPercent__BackingField = value;
		}
	}

	public int Genre
	{
		get
		{
			return Genre__BackingField;
		}
		set
		{
			Genre__BackingField = value;
		}
	}

	public float Advance
	{
		get
		{
			return Advance__BackingField;
		}
		set
		{
			Advance__BackingField = value;
		}
	}

	public float AdvanceBalance
	{
		get
		{
			return AdvanceBalance__BackingField;
		}
		set
		{
			AdvanceBalance__BackingField = value;
		}
	}

	public int HeroAgeAtSigning
	{
		get
		{
			return HeroAgeAtSigning__BackingField;
		}
		set
		{
			HeroAgeAtSigning__BackingField = value;
		}
	}

	public double GetAdjustedIncomeFromAmount(double amount)
	{
		return 0.0;
	}

	public int GetAdvanceBalance()
	{
		return 0;
	}

	public bool IsAdvancePaidOff()
	{
		return false;
	}
}
