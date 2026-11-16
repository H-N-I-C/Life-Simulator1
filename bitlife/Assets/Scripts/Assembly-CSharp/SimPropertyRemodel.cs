using System;

[Serializable]
public class SimPropertyRemodel
{
	private double Cost__BackingField;

	private float ProjectedAddedValue__BackingField;

	private float ActualAddedValue__BackingField;

	private string Project__BackingField;

	private string CompanyName__BackingField;

	private float CompanyReputation__BackingField;

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

	public float ProjectedAddedValue
	{
		get
		{
			return ProjectedAddedValue__BackingField;
		}
		set
		{
			ProjectedAddedValue__BackingField = value;
		}
	}

	public float ActualAddedValue
	{
		get
		{
			return ActualAddedValue__BackingField;
		}
		set
		{
			ActualAddedValue__BackingField = value;
		}
	}

	public DataLandlordHomeRemodelFeature Feature
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public string Project
	{
		get
		{
			return Project__BackingField;
		}
		set
		{
			Project__BackingField = value;
		}
	}

	public string CompanyName
	{
		get
		{
			return CompanyName__BackingField;
		}
		set
		{
			CompanyName__BackingField = value;
		}
	}

	public float CompanyReputation
	{
		get
		{
			return CompanyReputation__BackingField;
		}
		set
		{
			CompanyReputation__BackingField = value;
		}
	}

	public SimPropertyRemodel(DataLandlordHomeRemodelFeature feature, double targetCost)
	{
	}
}
