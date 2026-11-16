using System;
using Game;

[Serializable]
public class SimSpyAgencySecurityFeature
{
	private string Name__BackingField;

	private int YearsInstalled__BackingField;

	private float Condition__BackingField;

	public DataSecretAgentAgencySecurityFeature SecurityFeatureData
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

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

	public float Condition
	{
		get
		{
			return Condition__BackingField;
		}
		set
		{
			Condition__BackingField = value;
		}
	}

	public SimSpyAgencySecurityFeature(string featureName)
	{
	}

	public void IncrementAge()
	{
	}
}
