using System;
using Game;

[Serializable]
public class SimOutdoorPlantDataCache
{
	private string PlantSpecies__BackingField;

	public string PlantSpecies
	{
		get
		{
			return PlantSpecies__BackingField;
		}
		private set
		{
			PlantSpecies__BackingField = value;
		}
	}

	public SimOutdoorPlantDataCache(string species)
	{
	}

	public SimOutdoorPlantDataCache(DataOutdoorPlant data)
	{
	}

	public DataOutdoorPlant GetData()
	{
		return null;
	}
}
