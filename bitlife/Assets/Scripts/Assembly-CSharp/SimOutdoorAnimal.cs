using System;
using Game;

[Serializable]
public class SimOutdoorAnimal
{
	private string Species__BackingField;

	public string Species
	{
		get
		{
			return Species__BackingField;
		}
		set
		{
			Species__BackingField = value;
		}
	}

	public SimOutdoorAnimal(DataOutdoorAnimal data)
	{
	}

	public void IncrementAge()
	{
	}

	public DataOutdoorAnimal GetData()
	{
		return null;
	}
}
