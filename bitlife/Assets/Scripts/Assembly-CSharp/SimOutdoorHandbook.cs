using System;
using System.Collections.Generic;
using Game;

[Serializable]
public class SimOutdoorHandbook
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimOutdoorHandbookEntry, bool> _003C_003E9__11_0;

		public static Func<SimOutdoorHandbookEntry, SimOutdoorFishEntry> _003C_003E9__11_1;

		public static Func<SimOutdoorHandbookEntry, bool> _003C_003E9__15_0;

		public static Func<DataOutdoorAnimal, (string, string)> _003C_003E9__19_0;

		public static Func<DataOutdoorBird, (string, string)> _003C_003E9__19_1;

		public static Func<DataOutdoorCatchableFish, (string, string)> _003C_003E9__19_2;

		public static Func<DataOutdoorMarineLife, (string, string)> _003C_003E9__19_3;

		public static Func<DataOutdoorPlant, (string, string)> _003C_003E9__19_4;

		public static Func<SimOutdoorEntryDiscovery, string> _003C_003E9__20_1;

		internal bool _003CGetCatchFishList_003Eb__11_0(SimOutdoorHandbookEntry t)
		{
			return false;
		}

		internal SimOutdoorFishEntry _003CGetCatchFishList_003Eb__11_1(SimOutdoorHandbookEntry t)
		{
			return null;
		}

		internal bool _003CGetFishCaughtCount_003Eb__15_0(SimOutdoorHandbookEntry t)
		{
			return false;
		}

		internal (string, string) _003CGetAllDataStripped_003Eb__19_0(DataOutdoorAnimal t)
		{
			return default;
		}

		internal (string, string) _003CGetAllDataStripped_003Eb__19_1(DataOutdoorBird t)
		{
			return default;
		}

		internal (string, string) _003CGetAllDataStripped_003Eb__19_2(DataOutdoorCatchableFish t)
		{
			return default;
		}

		internal (string, string) _003CGetAllDataStripped_003Eb__19_3(DataOutdoorMarineLife t)
		{
			return default;
		}

		internal (string, string) _003CGetAllDataStripped_003Eb__19_4(DataOutdoorPlant t)
		{
			return default;
		}

		internal string _003CGetDiscoverableItems_003Eb__20_1(SimOutdoorEntryDiscovery x)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass20_0
	{
		public List<SimOutdoorHandbookEntry> discoveredItems;

		internal bool _003CGetDiscoverableItems_003Eb__0((string, string) t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass20_1
	{
		public (string, string) t;

		internal bool _003CGetDiscoverableItems_003Eb__2(SimOutdoorHandbookEntry x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass3_0
	{
		public OUTDOOR_HANDBOOK_ENTRY_TYPE type;

		internal bool _003CGetFoundHandbookList_003Eb__0(SimOutdoorHandbookEntry x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass9_0
	{
		public string species;

		public OUTDOOR_HANDBOOK_ENTRY_TYPE entryType;

		internal bool _003CContainsEntry_003Eb__0(SimOutdoorHandbookEntry x)
		{
			return false;
		}
	}

	private List<SimOutdoorHandbookEntry> _handbookEntries;

	private List<OUTDOOR_HANDBOOK_ENTRY_TYPE> _completedBadges;

	public List<SimOutdoorHandbookEntry> GetFoundHandbookList(OUTDOOR_HANDBOOK_ENTRY_TYPE type)
	{
		return null;
	}

	public void AddAnimal(SimOutdoorHandbookEntry animal)
	{
	}

	public void AddBird(SimOutdoorHandbookEntry bird)
	{
	}

	public void AddMarineLife(SimOutdoorHandbookEntry marineLife)
	{
	}

	public void AddFish(SimOutdoorHandbookEntry fish)
	{
	}

	public void AddPlant(SimOutdoorHandbookEntry plant)
	{
	}

	public bool ContainsEntry(string species, OUTDOOR_HANDBOOK_ENTRY_TYPE entryType)
	{
		return false;
	}

	public bool HasPlant(string plantSpecies)
	{
		return false;
	}

	public List<SimOutdoorFishEntry> GetCatchFishList()
	{
		return null;
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

	public int GetFishCaughtCount()
	{
		return 0;
	}

	public bool GetFishEntryExists(SimOutdoorCatchableFish incomingCatch)
	{
		return false;
	}

	public static int GetTotalCountForType(OUTDOOR_HANDBOOK_ENTRY_TYPE type)
	{
		return 0;
	}

	public static int GetTotalCount()
	{
		return 0;
	}

	private static List<(string, string)> GetAllDataStripped(OUTDOOR_HANDBOOK_ENTRY_TYPE type)
	{
		return null;
	}

	public static List<SimOutdoorEntryDiscovery> GetDiscoverableItems(OUTDOOR_HANDBOOK_ENTRY_TYPE type, List<SimOutdoorHandbookEntry> discoveredItems)
	{
		return null;
	}

	public bool HasCompletedDiscovery(OUTDOOR_HANDBOOK_ENTRY_TYPE animalType)
	{
		return false;
	}

	public string GetHandBookEmoji(OUTDOOR_HANDBOOK_ENTRY_TYPE animal)
	{
		return null;
	}

	public static string GetHandBookNormalEmoji(OUTDOOR_HANDBOOK_ENTRY_TYPE animal)
	{
		return null;
	}

	public static string GetHandBookBadgeEmoji(OUTDOOR_HANDBOOK_ENTRY_TYPE animal)
	{
		return null;
	}

	public List<SimOutdoorHandbookEntry> GetEntries()
	{
		return null;
	}
}
