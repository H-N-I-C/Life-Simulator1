using System;

[Serializable]
public class SimModelSignatureStyle
{
	private float Attitude__BackingField;

	private float Poise__BackingField;

	private float Quirkiness__BackingField;

	public float Attitude
	{
		get
		{
			return Attitude__BackingField;
		}
		set
		{
			Attitude__BackingField = value;
		}
	}

	public float Poise
	{
		get
		{
			return Poise__BackingField;
		}
		set
		{
			Poise__BackingField = value;
		}
	}

	public float Quirkiness
	{
		get
		{
			return Quirkiness__BackingField;
		}
		set
		{
			Quirkiness__BackingField = value;
		}
	}
}
