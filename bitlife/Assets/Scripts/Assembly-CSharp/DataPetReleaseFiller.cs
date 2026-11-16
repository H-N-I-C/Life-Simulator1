public class DataPetReleaseFiller
{
	private int Animal__BackingField;

	private string TheString__BackingField;

	private int PetGender__BackingField;

	private bool BigAnimal__BackingField;

	private bool Clean__BackingField;

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

	public string TheString
	{
		get
		{
			return TheString__BackingField;
		}
		set
		{
			TheString__BackingField = value;
		}
	}

	public int PetGender
	{
		get
		{
			return PetGender__BackingField;
		}
		set
		{
			PetGender__BackingField = value;
		}
	}

	public bool BigAnimal
	{
		get
		{
			return BigAnimal__BackingField;
		}
		set
		{
			BigAnimal__BackingField = value;
		}
	}

	public bool Clean
	{
		get
		{
			return Clean__BackingField;
		}
		set
		{
			Clean__BackingField = value;
		}
	}

	public bool IsBreedEligible(string breed)
	{
		return false;
	}
}
