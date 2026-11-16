using System;
using System.Collections;

[Serializable]
public class SimEmigration
{
	private int Type__BackingField;

	private SimPlace FromPlace__BackingField;

	private SimPlace ToPlace__BackingField;

	private int HeroAgeAtEmigration__BackingField;

	public int Type
	{
		get
		{
			return Type__BackingField;
		}
		set
		{
			Type__BackingField = value;
		}
	}

	public SimPlace FromPlace
	{
		get
		{
			return FromPlace__BackingField;
		}
		set
		{
			FromPlace__BackingField = value;
		}
	}

	public SimPlace ToPlace
	{
		get
		{
			return ToPlace__BackingField;
		}
		set
		{
			ToPlace__BackingField = value;
		}
	}

	public int HeroAgeAtEmigration
	{
		get
		{
			return HeroAgeAtEmigration__BackingField;
		}
		set
		{
			HeroAgeAtEmigration__BackingField = value;
		}
	}

	public static ArrayList GetEmigrationChildArrayAndIsCurrentLoverGoing(bool loverGoing)
	{
		return null;
	}
}
