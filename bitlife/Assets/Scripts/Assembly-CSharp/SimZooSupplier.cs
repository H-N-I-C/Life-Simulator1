using System;

[Serializable]
public class SimZooSupplier
{
	private string Name__BackingField;

	private double CostPerAnimal__BackingField;

	private float QualityLevel__BackingField;

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

	public double CostPerAnimal
	{
		get
		{
			return CostPerAnimal__BackingField;
		}
		set
		{
			CostPerAnimal__BackingField = value;
		}
	}

	public float QualityLevel
	{
		get
		{
			return QualityLevel__BackingField;
		}
		set
		{
			QualityLevel__BackingField = value;
		}
	}

	public SimZooSupplier(float quality = 0f)
	{
	}
}
