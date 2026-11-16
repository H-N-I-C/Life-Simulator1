using System;
using Game;

[Serializable]
public class SimOutdoorDestinationDataCache
{
	private string Location__BackingField;

	public string Location
	{
		get
		{
			return Location__BackingField;
		}
		private set
		{
			Location__BackingField = value;
		}
	}

	public SimOutdoorDestinationDataCache(string location)
	{
	}

	public SimOutdoorDestinationDataCache(DataOutdoorDestination data)
	{
	}

	public DataOutdoorDestination GetData()
	{
		return null;
	}
}
