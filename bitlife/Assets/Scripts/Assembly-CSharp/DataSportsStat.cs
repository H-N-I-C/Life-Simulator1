using System.Collections;

public class DataSportsStat
{
	private int Sport__BackingField;

	private string Stat__BackingField;

	private int IntegerDigits__BackingField;

	private int FractionDigits__BackingField;

	private ArrayList SkillsArray__BackingField;

	private ArrayList PercentileArray__BackingField;

	public int Sport
	{
		get
		{
			return Sport__BackingField;
		}
		set
		{
			Sport__BackingField = value;
		}
	}

	public string Stat
	{
		get
		{
			return Stat__BackingField;
		}
		set
		{
			Stat__BackingField = value;
		}
	}

	public int IntegerDigits
	{
		get
		{
			return IntegerDigits__BackingField;
		}
		set
		{
			IntegerDigits__BackingField = value;
		}
	}

	public int FractionDigits
	{
		get
		{
			return FractionDigits__BackingField;
		}
		set
		{
			FractionDigits__BackingField = value;
		}
	}

	public ArrayList SkillsArray
	{
		get
		{
			return SkillsArray__BackingField;
		}
		set
		{
			SkillsArray__BackingField = value;
		}
	}

	public ArrayList PercentileArray
	{
		get
		{
			return PercentileArray__BackingField;
		}
		set
		{
			PercentileArray__BackingField = value;
		}
	}
}
