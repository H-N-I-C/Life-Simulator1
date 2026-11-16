using System;
using System.Collections;
using Game;

[Serializable]
public class SimThirdPersonAssets
{
	[NonSerialized]
	public SimPerson TheDelegate;

	private double BankBalance__BackingField;

	private ArrayList _HouseArray__BackingField;

	private ArrayList _CarArray__BackingField;

	private ArrayList _AircraftArray__BackingField;

	private ArrayList _WatercraftArray__BackingField;

	private ArrayList _RecreationalVehicleArray__BackingField;

	private ArrayList _JewelryArray__BackingField;

	private ArrayList _PhysicalInstrumentArray__BackingField;

	private ArrayList _BlackMarketAuctionItemsArray;

	private ArrayList _ModelGiftArray;

	private ArrayList _CasinoCollectibleArray;

	private ArrayList _RacingCollectibleArray;

	private ArrayList _OutdoorFishingTreasureArray;

	public double BankBalance
	{
		get
		{
			return BankBalance__BackingField;
		}
		set
		{
			BankBalance__BackingField = value;
		}
	}

	private ArrayList _HouseArray
	{
		get
		{
			return _HouseArray__BackingField;
		}
		set
		{
			_HouseArray__BackingField = value;
		}
	}

	public ArrayList HouseArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _CarArray
	{
		get
		{
			return _CarArray__BackingField;
		}
		set
		{
			_CarArray__BackingField = value;
		}
	}

	public ArrayList CarArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _AircraftArray
	{
		get
		{
			return _AircraftArray__BackingField;
		}
		set
		{
			_AircraftArray__BackingField = value;
		}
	}

	public ArrayList AircraftArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _WatercraftArray
	{
		get
		{
			return _WatercraftArray__BackingField;
		}
		set
		{
			_WatercraftArray__BackingField = value;
		}
	}

	public ArrayList WatercraftArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _RecreationalVehicleArray
	{
		get
		{
			return _RecreationalVehicleArray__BackingField;
		}
		set
		{
			_RecreationalVehicleArray__BackingField = value;
		}
	}

	public ArrayList RecreationalVehicleArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _JewelryArray
	{
		get
		{
			return _JewelryArray__BackingField;
		}
		set
		{
			_JewelryArray__BackingField = value;
		}
	}

	public ArrayList JewelryArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _PhysicalInstrumentArray
	{
		get
		{
			return _PhysicalInstrumentArray__BackingField;
		}
		set
		{
			_PhysicalInstrumentArray__BackingField = value;
		}
	}

	public ArrayList PhysicalInstrumentArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList BlackMarketAuctionItemsArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList ModelGiftArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList CasinoCollectibleArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList RacingCollectibleArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList OutdoorFishingTreasureArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimThirdPersonAssets()
	{
	}

	public SimThirdPersonAssets(SimPerson person)
	{
	}

	public SimThirdPersonAssets(SimCelebrityLover celebrityLover)
	{
	}

	public void SetupForCelebrityLover(SimCelebrityLover celebrityLover)
	{
	}

	public void SetupForPerson(SimPerson person)
	{
	}

	public void IncrementAgeWithSalaryAndLivingExpenses(float salary, float livingExpenses, SimPerson aDelegate = null)
	{
	}

	public double NetWorth()
	{
		return 0.0;
	}

	public double Assets()
	{
		return 0.0;
	}

	public double HardAssetsTotalValue()
	{
		return 0.0;
	}

	public double AnnualExpensesForAssets()
	{
		return 0.0;
	}

	public double Liabilities()
	{
		return 0.0;
	}

	public void AdjustBankBalanceByAmount(double amount)
	{
	}

	public void RecalibrateMoneyAttribute()
	{
	}
}
