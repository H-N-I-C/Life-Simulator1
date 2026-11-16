using System.Collections;

public class DataBusinessProduct
{
	private int Industry__BackingField;

	private ArrayList ProductList__BackingField;

	private string ProductNameBase__BackingField;

	private string Unit__BackingField;

	private string UnitPlural__BackingField;

	private float AverageUnitPrice__BackingField;

	private float MaxUnitPrice__BackingField;

	private float MinUnitCost__BackingField;

	private float MaxUnitCost__BackingField;

	private int DemandScore__BackingField;

	private int ShelfLifeYears__BackingField;

	public int Industry
	{
		get
		{
			return Industry__BackingField;
		}
		set
		{
			Industry__BackingField = value;
		}
	}

	public ArrayList ProductList
	{
		get
		{
			return ProductList__BackingField;
		}
		set
		{
			ProductList__BackingField = value;
		}
	}

	public string ProductNameBase
	{
		get
		{
			return ProductNameBase__BackingField;
		}
		set
		{
			ProductNameBase__BackingField = value;
		}
	}

	public string Unit
	{
		get
		{
			return Unit__BackingField;
		}
		set
		{
			Unit__BackingField = value;
		}
	}

	public string UnitPlural
	{
		get
		{
			return UnitPlural__BackingField;
		}
		set
		{
			UnitPlural__BackingField = value;
		}
	}

	public float AverageUnitPrice
	{
		private get
		{
			return AverageUnitPrice__BackingField;
		}
		set
		{
			AverageUnitPrice__BackingField = value;
		}
	}

	public float MaxUnitPrice
	{
		private get
		{
			return MaxUnitPrice__BackingField;
		}
		set
		{
			MaxUnitPrice__BackingField = value;
		}
	}

	public float MinUnitCost
	{
		private get
		{
			return MinUnitCost__BackingField;
		}
		set
		{
			MinUnitCost__BackingField = value;
		}
	}

	public float MaxUnitCost
	{
		private get
		{
			return MaxUnitCost__BackingField;
		}
		set
		{
			MaxUnitCost__BackingField = value;
		}
	}

	public int DemandScore
	{
		get
		{
			return DemandScore__BackingField;
		}
		set
		{
			DemandScore__BackingField = value;
		}
	}

	public int ShelfLifeYears
	{
		get
		{
			return ShelfLifeYears__BackingField;
		}
		set
		{
			ShelfLifeYears__BackingField = value;
		}
	}

	public float GetAverageUnitPrice(DataCountry country)
	{
		return 0f;
	}

	public float GetMaxUnitPrice(DataCountry country)
	{
		return 0f;
	}

	public float GetMinUnitCost(DataCountry country)
	{
		return 0f;
	}

	public float GetMaxUnitCost(DataCountry country)
	{
		return 0f;
	}
}
