public class DataPetRelease
{
	private int Animal__BackingField;

	private string Scenario__BackingField;

	public int Animal
	{
		get
		{
			return Animal__BackingField;
		}
		set
		{
			Animal__BackingField = value;
		}
	}

	public string Scenario
	{
		get
		{
			return Scenario__BackingField;
		}
		set
		{
			Scenario__BackingField = value;
		}
	}
}
