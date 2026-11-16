using System;

public class DataPrisonGang : IComparable
{
	private int IndexNI__BackingField;

	private int Type__BackingField;

	private string MaleName__BackingField;

	private string FemaleName__BackingField;

	private string GangDescription__BackingField;

	private string ShortDescription__BackingField;

	private int Power__BackingField;

	private int Violence__BackingField;

	private int RequiredRespect__BackingField;

	public int IndexNI
	{
		get
		{
			return IndexNI__BackingField;
		}
		set
		{
			IndexNI__BackingField = value;
		}
	}

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

	public string MaleName
	{
		get
		{
			return MaleName__BackingField;
		}
		set
		{
			MaleName__BackingField = value;
		}
	}

	public string FemaleName
	{
		get
		{
			return FemaleName__BackingField;
		}
		set
		{
			FemaleName__BackingField = value;
		}
	}

	public string GangDescription
	{
		get
		{
			return GangDescription__BackingField;
		}
		set
		{
			GangDescription__BackingField = value;
		}
	}

	public string ShortDescription
	{
		get
		{
			return ShortDescription__BackingField;
		}
		set
		{
			ShortDescription__BackingField = value;
		}
	}

	public int Power
	{
		get
		{
			return Power__BackingField;
		}
		set
		{
			Power__BackingField = value;
		}
	}

	public int Violence
	{
		get
		{
			return Violence__BackingField;
		}
		set
		{
			Violence__BackingField = value;
		}
	}

	public int RequiredRespect
	{
		get
		{
			return RequiredRespect__BackingField;
		}
		set
		{
			RequiredRespect__BackingField = value;
		}
	}

	public string GangNameForGender(int gender)
	{
		return null;
	}

	public int CompareTo(object otherObject)
	{
		return 0;
	}
}
