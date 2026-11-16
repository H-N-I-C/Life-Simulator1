using System;
using System.Collections.Generic;

[Serializable]
public class SimOutdoor
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimRecreationalVehicle, bool> _003C_003E9__44_0;

		public static Func<SimRecreationalVehicle, float> _003C_003E9__44_1;

		internal bool _003CGetDealerShipInventory_003Eb__44_0(SimRecreationalVehicle x)
		{
			return false;
		}

		internal float _003CGetDealerShipInventory_003Eb__44_1(SimRecreationalVehicle t)
		{
			return 0f;
		}
	}

	private const int MAX_DEALERSHIP_ITEMS = 8;

	private SimOutdoorHandbook Handbook__BackingField;

	private int _successfulFishingTripsThisAge;

	private int _successfulBirdwatchingTripsThisAge;

	private int _successfulSnorkelingTripsThisAge;

	private List<SimOutdoorMountedFish> _mountedFishList;

	private string ATVDealershipName__BackingField;

	private int NumTimesWentHiking__BackingField;

	private int NumTimesWentCamping__BackingField;

	private int NumTimesWentSpelunking__BackingField;

	private int NumFishCaught__BackingField;

	[NonSerialized]
	private List<SimRecreationalVehicle> _listDealerRecreationalVehicle;

	public SimOutdoorHandbook Handbook
	{
		get
		{
			return Handbook__BackingField;
		}
		set
		{
			Handbook__BackingField = value;
		}
	}

	public string ATVDealershipName
	{
		get
		{
			return ATVDealershipName__BackingField;
		}
		set
		{
			ATVDealershipName__BackingField = value;
		}
	}

	public int NumTimesWentHiking
	{
		get
		{
			return NumTimesWentHiking__BackingField;
		}
		set
		{
			NumTimesWentHiking__BackingField = value;
		}
	}

	public int NumTimesWentCamping
	{
		get
		{
			return NumTimesWentCamping__BackingField;
		}
		set
		{
			NumTimesWentCamping__BackingField = value;
		}
	}

	public int NumTimesWentSpelunking
	{
		get
		{
			return NumTimesWentSpelunking__BackingField;
		}
		set
		{
			NumTimesWentSpelunking__BackingField = value;
		}
	}

	public int NumFishCaught
	{
		get
		{
			return NumFishCaught__BackingField;
		}
		set
		{
			NumFishCaught__BackingField = value;
		}
	}

	public void IncrementAge()
	{
	}

	public SimOutdoorFishEntry GetFishEntryBySpecies(string species)
	{
		return null;
	}

	public void UpdateFishEntry(SimOutdoorCatchableFish fish)
	{
	}

	public void AddNewFishEntry(SimOutdoorCatchableFish caughtFish)
	{
	}

	public bool GetFishEntryExists(SimOutdoorCatchableFish incomingCatch)
	{
		return false;
	}

	public int GetFishCaughtCount()
	{
		return 0;
	}

	public void IncrementSuccessfulFishingTrip()
	{
	}

	public bool MaximumSuccessfulFishingTripsReached()
	{
		return false;
	}

	public int GetTotalFish()
	{
		return 0;
	}

	public static SimOutdoorFishingTreasure GetFishingTreasureOrNull(string location)
	{
		return null;
	}

	public void IncrementSuccessfulBirdwatchingTrip()
	{
	}

	public void IncrementSuccessfulSnorkelingTrip()
	{
	}

	public bool MaximumSuccessfulBirdwatchingTripsReached()
	{
		return false;
	}

	public List<SimRecreationalVehicle> GetDealerShipInventory()
	{
		return null;
	}

	private void ReplenishDealerShipInventory()
	{
	}

	public List<SimRecreationalVehicle> GetVehicles()
	{
		return null;
	}

	public void AddVehicle(SimRecreationalVehicle item)
	{
	}

	public static string EmojiForOutdoorVehicle(SimRecreationalVehicle vehicle)
	{
		return null;
	}

	public bool MaximumSuccessfulSnorkelingTripsReached()
	{
		return false;
	}

	public static string GetAnimalTypeString(OUTDOOR_HANDBOOK_ENTRY_TYPE type)
	{
		return null;
	}

	public void AddMountedFish(SimOutdoorMountedFish mountedFish)
	{
	}

	public void RemoveMountedFish(SimOutdoorMountedFish mountedFish)
	{
	}

	public List<SimOutdoorMountedFish> GetMountedFishList()
	{
		return null;
	}
}
