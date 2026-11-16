using System;

[Serializable]
public class SimPetDisease
{
	private int YearsSuffered__BackingField;

	private bool RevealedByVet__BackingField;

	private int PetAgeAtLastTreatment__BackingField;

	private int PetAgeAtCure__BackingField;

	private int _Animal;

	private string _DiseaseString;

	public int YearsSuffered
	{
		get
		{
			return YearsSuffered__BackingField;
		}
		set
		{
			YearsSuffered__BackingField = value;
		}
	}

	public bool RevealedByVet
	{
		get
		{
			return RevealedByVet__BackingField;
		}
		set
		{
			RevealedByVet__BackingField = value;
		}
	}

	public int PetAgeAtLastTreatment
	{
		get
		{
			return PetAgeAtLastTreatment__BackingField;
		}
		set
		{
			PetAgeAtLastTreatment__BackingField = value;
		}
	}

	public int PetAgeAtCure
	{
		get
		{
			return PetAgeAtCure__BackingField;
		}
		set
		{
			PetAgeAtCure__BackingField = value;
		}
	}

	public DataPetDiseaseType DiseaseType
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public string Description()
	{
		return null;
	}
}
