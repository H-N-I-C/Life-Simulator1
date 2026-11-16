using System;

[Serializable]
public class SimBookingAgent : SimLover
{
	private float Att_reputation__BackingField;

	public float Att_reputation
	{
		get
		{
			return Att_reputation__BackingField;
		}
		set
		{
			Att_reputation__BackingField = value;
		}
	}

	public SimBookingAgent(bool initHere)
		: base(initHere: false)
	{
	}

	public override void Die()
	{
	}
}
