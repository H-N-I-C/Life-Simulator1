public class DataPetDeathType
{
	private int Animal__BackingField;

	private int DeathType__BackingField;

	private int CommonnessScore__BackingField;

	private bool OtherPersonInvolved__BackingField;

	private string DescriptiveSentence__BackingField;

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

	public int DeathType
	{
		get
		{
			return DeathType__BackingField;
		}
		set
		{
			DeathType__BackingField = value;
		}
	}

	public int CommonnessScore
	{
		get
		{
			return CommonnessScore__BackingField;
		}
		set
		{
			CommonnessScore__BackingField = value;
		}
	}

	public bool OtherPersonInvolved
	{
		get
		{
			return OtherPersonInvolved__BackingField;
		}
		set
		{
			OtherPersonInvolved__BackingField = value;
		}
	}

	public string DescriptiveSentence
	{
		get
		{
			return DescriptiveSentence__BackingField;
		}
		set
		{
			DescriptiveSentence__BackingField = value;
		}
	}
}
