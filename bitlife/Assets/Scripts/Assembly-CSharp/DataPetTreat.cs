public class DataPetTreat
{
	private int Animal__BackingField;

	private string TreatString__BackingField;

	private int EnjoymentScore__BackingField;

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

	public string TreatString
	{
		get
		{
			return TreatString__BackingField;
		}
		set
		{
			TreatString__BackingField = value;
		}
	}

	public int EnjoymentScore
	{
		get
		{
			return EnjoymentScore__BackingField;
		}
		set
		{
			EnjoymentScore__BackingField = value;
		}
	}
}
