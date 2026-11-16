using System;

[Serializable]
public class SimCompetitor
{
	private string Name__BackingField;

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

	public SimCompetitor(string name)
	{
	}
}
