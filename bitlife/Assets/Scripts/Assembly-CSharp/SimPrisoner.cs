using System;

[Serializable]
public class SimPrisoner : SimPerson, IComparable
{
	private string Nickname__BackingField;

	private string CrimeString__BackingField;

	private int GangIndexNI__BackingField;

	private int TimesBulliedHero__BackingField;

	private bool VanquishedFromBullying__BackingField;

	private int HeroAgeAtLastBulliedByHero__BackingField;

	private float Att_size__BackingField;

	private float Att_respect__BackingField;

	public string Nickname
	{
		get
		{
			return Nickname__BackingField;
		}
		set
		{
			Nickname__BackingField = value;
		}
	}

	public string CrimeString
	{
		get
		{
			return CrimeString__BackingField;
		}
		set
		{
			CrimeString__BackingField = value;
		}
	}

	public int GangIndexNI
	{
		get
		{
			return GangIndexNI__BackingField;
		}
		set
		{
			GangIndexNI__BackingField = value;
		}
	}

	public int TimesBulliedHero
	{
		get
		{
			return TimesBulliedHero__BackingField;
		}
		set
		{
			TimesBulliedHero__BackingField = value;
		}
	}

	public bool VanquishedFromBullying
	{
		get
		{
			return VanquishedFromBullying__BackingField;
		}
		set
		{
			VanquishedFromBullying__BackingField = value;
		}
	}

	public int HeroAgeAtLastBulliedByHero
	{
		get
		{
			return HeroAgeAtLastBulliedByHero__BackingField;
		}
		set
		{
			HeroAgeAtLastBulliedByHero__BackingField = value;
		}
	}

	public float Att_size
	{
		get
		{
			return Att_size__BackingField;
		}
		set
		{
			Att_size__BackingField = value;
		}
	}

	public float Att_respect
	{
		get
		{
			return Att_respect__BackingField;
		}
		set
		{
			Att_respect__BackingField = value;
		}
	}

	private SimPrisoner()
	{
	}

	public SimPrisoner(bool initHere)
	{
	}

	public bool IsPassive()
	{
		return false;
	}

	public bool IsInGang()
	{
		return false;
	}

	public bool IsInSameGangAsHero()
	{
		return false;
	}

	public int CompareTo(object otherObject)
	{
		return 0;
	}
}
