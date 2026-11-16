using System;

[Serializable]
public class SimFacility
{
	private string Name__BackingField;

	private int NumEmployees__BackingField;

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

	public int NumEmployees
	{
		get
		{
			return NumEmployees__BackingField;
		}
		set
		{
			NumEmployees__BackingField = value;
		}
	}
}
