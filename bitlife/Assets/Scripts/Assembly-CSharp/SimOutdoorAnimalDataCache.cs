using System;
using Game;

[Serializable]
public class SimOutdoorAnimalDataCache
{
	private string AnimalSpecies__BackingField;

	public string AnimalSpecies
	{
		get
		{
			return AnimalSpecies__BackingField;
		}
		private set
		{
			AnimalSpecies__BackingField = value;
		}
	}

	public SimOutdoorAnimalDataCache(string animalSpecies)
	{
	}

	public SimOutdoorAnimalDataCache(DataOutdoorAnimal data)
	{
	}

	public DataOutdoorAnimal GetData()
	{
		return null;
	}
}
