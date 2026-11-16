using System;
using Game;

[Serializable]
public class SimZooGiftShopItem
{
	private string Name__BackingField;

	private string Product__BackingField;

	private int UnitsAvailable__BackingField;

	private double SellPrice__BackingField;

	private float PricePerUnit__BackingField;

	private SimZooAnimal Animal__BackingField;

	private int UnitsSoldLastYear__BackingField;

	private double LastYearsRevenue__BackingField;

	private int MaxUnits__BackingField;

	private float _condition;

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

	public string Product
	{
		get
		{
			return Product__BackingField;
		}
		set
		{
			Product__BackingField = value;
		}
	}

	public int UnitsAvailable
	{
		get
		{
			return UnitsAvailable__BackingField;
		}
		set
		{
			UnitsAvailable__BackingField = value;
		}
	}

	public double SellPrice
	{
		get
		{
			return SellPrice__BackingField;
		}
		set
		{
			SellPrice__BackingField = value;
		}
	}

	public float PricePerUnit
	{
		get
		{
			return PricePerUnit__BackingField;
		}
		set
		{
			PricePerUnit__BackingField = value;
		}
	}

	public SimZooAnimal Animal
	{
		get
		{
			return Animal__BackingField;
		}
		set
		{
			Animal__BackingField = value;
		}
	}

	public int UnitsSoldLastYear
	{
		get
		{
			return UnitsSoldLastYear__BackingField;
		}
		set
		{
			UnitsSoldLastYear__BackingField = value;
		}
	}

	public double LastYearsRevenue
	{
		get
		{
			return LastYearsRevenue__BackingField;
		}
		set
		{
			LastYearsRevenue__BackingField = value;
		}
	}

	public int MaxUnits
	{
		get
		{
			return MaxUnits__BackingField;
		}
		set
		{
			MaxUnits__BackingField = value;
		}
	}

	public DataZooGiftShopItem ItemData => null;

	public SimZooGiftShopItem(string product)
	{
	}

	public void IncrementAge()
	{
	}

	public float GetCondition()
	{
		return 0f;
	}

	public float GetPopularity()
	{
		return 0f;
	}

	public void SetFeaturedAnimal(SimZooAnimal animal)
	{
	}

	public double GetAnnualRevenue()
	{
		return 0.0;
	}
}
