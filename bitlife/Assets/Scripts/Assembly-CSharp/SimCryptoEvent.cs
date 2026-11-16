using System;

[Serializable]
public class SimCryptoEvent
{
	private double Price__BackingField;

	private int EventType__BackingField;

	public double Price
	{
		get
		{
			return Price__BackingField;
		}
		set
		{
			Price__BackingField = value;
		}
	}

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

	public SimCryptoEvent(double price, int eventType)
	{
	}
}
