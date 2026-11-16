using System;

[Serializable]
public class SimActorDirectorMeta
{
	private SimPersonName Name__BackingField;

	private int Gender__BackingField;

	public SimPersonName Name
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
