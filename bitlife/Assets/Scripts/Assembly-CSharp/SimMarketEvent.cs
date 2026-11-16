using System;

[Serializable]
public class SimMarketEvent
{
	private int EventType__BackingField;

	public int EventType
	{
		get
		{
			return EventType__BackingField;
		}
		set
		{
			EventType__BackingField = value;
		}
	}
}
