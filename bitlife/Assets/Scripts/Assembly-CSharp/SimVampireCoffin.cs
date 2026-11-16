using System;

[Serializable]
public class SimVampireCoffin
{
	private string Descriptor__BackingField;

	private SimPerson Vampire__BackingField;

	public string Descriptor
	{
		get
		{
			return Descriptor__BackingField;
		}
		set
		{
			Descriptor__BackingField = value;
		}
	}

	public SimPerson Vampire
	{
		get
		{
			return Vampire__BackingField;
		}
		set
		{
			Vampire__BackingField = value;
		}
	}
}
