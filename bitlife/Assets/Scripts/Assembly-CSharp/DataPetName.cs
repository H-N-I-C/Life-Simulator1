using System.Collections.Generic;

public class DataPetName
{
	private int Animal__BackingField;

	private string Name__BackingField;

	public List<string> ColorStringArray;

	private int Gender__BackingField;

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

	public string Name
	{
		get
		{
			return Name__BackingField;
		}
		set
		{
			Name__BackingField = value;
		}
	}

	public int Gender
	{
		get
		{
			return Gender__BackingField;
		}
		set
		{
			Gender__BackingField = value;
		}
	}
}
