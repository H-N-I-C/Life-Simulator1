public class DataDumbScenarioCalendar : DataDumbScenario
{
	private string Month__BackingField;

	private int Day__BackingField;

	private bool HeroHasLover__BackingField;

	private bool HeroDoesntHaveLover__BackingField;

	private int LoverGender__BackingField;

	public string Month
	{
		get
		{
			return Month__BackingField;
		}
		set
		{
			Month__BackingField = value;
		}
	}

	public int Day
	{
		get
		{
			return Day__BackingField;
		}
		set
		{
			Day__BackingField = value;
		}
	}

	public bool HeroHasLover
	{
		get
		{
			return HeroHasLover__BackingField;
		}
		set
		{
			HeroHasLover__BackingField = value;
		}
	}

	public bool HeroDoesntHaveLover
	{
		get
		{
			return HeroDoesntHaveLover__BackingField;
		}
		set
		{
			HeroDoesntHaveLover__BackingField = value;
		}
	}

	public int LoverGender
	{
		get
		{
			return LoverGender__BackingField;
		}
		set
		{
			LoverGender__BackingField = value;
		}
	}
}
