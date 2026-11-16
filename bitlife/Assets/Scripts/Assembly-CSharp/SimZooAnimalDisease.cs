using System;
using Game;

[Serializable]
public class SimZooAnimalDisease
{
	private int YearsSuffered__BackingField;

	private bool RevealedByVet__BackingField;

	private int AgeAtLastTreatment__BackingField;

	private int AgeAtCure__BackingField;

	private string _Animal;

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

	public int AgeAtLastTreatment
	{
		get
		{
			return AgeAtLastTreatment__BackingField;
		}
		set
		{
			AgeAtLastTreatment__BackingField = value;
		}
	}

	public int AgeAtCure
	{
		get
		{
			return AgeAtCure__BackingField;
		}
		set
		{
			AgeAtCure__BackingField = value;
		}
	}

	public DataZooAnimalDisease DiseaseData
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
