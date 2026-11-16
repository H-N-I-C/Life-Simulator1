public class DataDumbScenarioAstronaut : DataDumbScenario
{
	private bool HeroHasLover__BackingField;

	private int LoverGender__BackingField;

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
