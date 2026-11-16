using System;

[Serializable]
public class SimChildSupport
{
	private double Amount__BackingField;

	private bool ExHasCustody__BackingField;

	public double Amount
	{
		get
		{
			return Amount__BackingField;
		}
		set
		{
			Amount__BackingField = value;
		}
	}

	public bool ExHasCustody
	{
		get
		{
			return ExHasCustody__BackingField;
		}
		set
		{
			ExHasCustody__BackingField = value;
		}
	}
}
