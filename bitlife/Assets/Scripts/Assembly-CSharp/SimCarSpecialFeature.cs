using System;

[Serializable]
public class SimCarSpecialFeature
{
	private string Name__BackingField;

	private double Cost__BackingField;

	private int YearsInstalled__BackingField;

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

	public double Cost
	{
		get
		{
			return Cost__BackingField;
		}
		set
		{
			Cost__BackingField = value;
		}
	}

	public int YearsInstalled
	{
		get
		{
			return YearsInstalled__BackingField;
		}
		set
		{
			YearsInstalled__BackingField = value;
		}
	}

	public void IncrementAge()
	{
	}
}
