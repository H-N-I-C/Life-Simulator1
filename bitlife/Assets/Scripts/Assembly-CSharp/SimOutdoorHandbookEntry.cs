using System;

[Serializable]
public class SimOutdoorHandbookEntry
{
	private SimOutdoorAnimal _animal;

	private SimOutdoorPlant _plant;

	private SimOutdoorBird _bird;

	private SimOutdoorMarineLife _marineLife;

	private SimOutdoorFishEntry _fish;

	private OUTDOOR_HANDBOOK_ENTRY_TYPE AnimalType__BackingField;

	private string DiscoveryLocation__BackingField;

	public SimOutdoorPlant Plant => null;

	public SimOutdoorAnimal Animal => null;

	public SimOutdoorBird Bird => null;

	public SimOutdoorMarineLife MarineLife => null;

	public SimOutdoorFishEntry Fish => null;

	public OUTDOOR_HANDBOOK_ENTRY_TYPE AnimalType
	{
		get
		{
			return AnimalType__BackingField;
		}
		set
		{
			AnimalType__BackingField = value;
		}
	}

	public string DiscoveryLocation
	{
		get
		{
			return DiscoveryLocation__BackingField;
		}
		set
		{
			DiscoveryLocation__BackingField = value;
		}
	}

	public SimOutdoorHandbookEntry(SimOutdoorFishEntry fish)
	{
	}

	public SimOutdoorHandbookEntry(SimOutdoorBird bird)
	{
	}

	public SimOutdoorHandbookEntry(SimOutdoorMarineLife marineLife)
	{
	}

	public SimOutdoorHandbookEntry(SimOutdoorPlant plant, string location)
	{
	}

	public SimOutdoorHandbookEntry(SimOutdoorAnimal animal, string location)
	{
	}

	public string GetEmoji()
	{
		return null;
	}

	public string GetSpecies()
	{
		return null;
	}

	public string GetHandbookColorString()
	{
		return null;
	}

	public string GetHabitatString()
	{
		return null;
	}
}
