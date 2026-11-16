using System.Collections;
using UnityEngine;

public class AccessoryManager
{
	public static ArrayList GetAllAcquiredHeadwearArray()
	{
		return null;
	}

	public static ArrayList GetAllAcquiredSunglassesArray()
	{
		return null;
	}

	public static void AcquireAccessoryHeadwearIndex(int index)
	{
	}

	public static void AcquireAccessorySunglassesIndex(int index)
	{
	}

	public static ArrayList GetDefaultAccessoryHeadwearArray()
	{
		return null;
	}

	public static ArrayList GetDefaultAccessorySunglassesArray()
	{
		return null;
	}

	public static int GetNumberOfAcquiredHeadwear()
	{
		return 0;
	}

	public static int GetNumberOfAcquiredSunglasses()
	{
		return 0;
	}

	public static bool IsCustomHolidayRewardAvailable(DataAccessory accessory)
	{
		return false;
	}

	public static ArrayList GetAllNotCollectedCustomHolidayAccessoryArray(int type)
	{
		return null;
	}

	public static DataAccessory GetCustomHolidayReward(HOLIDAY_COLLECTIBLE_TYPE type)
	{
		return null;
	}

	public static bool IsSpecialHolidayAccessory(int type, int index)
	{
		return false;
	}

	public static bool IsSeasonAccessory(int type, int index)
	{
		return false;
	}

	public static string GetCategoryStringForType(HOLIDAY_COLLECTIBLE_TYPE type)
	{
		return null;
	}

	public static bool HasZooRewardAccessoryHeadwearBeenEarned(DataAccessoryHeadwear accessory)
	{
		return false;
	}

	public static bool IsZooRewardAccessoryHeadwearAvailable(DataAccessoryHeadwear accessory)
	{
		return false;
	}

	public static ArrayList GetAllNotCollectedZooAccessoryHeadwearArray()
	{
		return null;
	}

	public static ArrayList GetZooAccessoryHeadwearIndexes()
	{
		return null;
	}

	public static bool HasAnyZooAccessoryHeadwearBeenEarned()
	{
		return false;
	}

	public static int GetNumberOfCasinoAccessories()
	{
		return 0;
	}

	public static bool HasAnyCasinoAccessoryBeenEarned()
	{
		return false;
	}

	public static DataAccessory GetUncollectedCasinoReward(int accessoryIndex)
	{
		return null;
	}

	public static bool IsCasinoRewardSpecialAccessory(int type, int index)
	{
		return false;
	}

	public static bool IsCasinoRewardNormalAccessory(int type, int index)
	{
		return false;
	}

	public static bool IsCasinoRewardAccessory(int type, int index)
	{
		return false;
	}

	private static bool IsCasinoRewardAccessoryWithSpecialChecks(int type, int index, bool special = false, bool overrideCheckAll = false)
	{
		return false;
	}

	public static int GetNumberOfRacingAccessories()
	{
		return 0;
	}

	public static bool HasAnyRacingAccessoryBeenEarned()
	{
		return false;
	}

	public static DataAccessory GetUncollectedRacingReward()
	{
		return null;
	}

	public static bool IsRacingRewardAccessory(int index)
	{
		return false;
	}

	public static bool IsRewardAccessoryAvailable()
	{
		return false;
	}

	public static DataAccessory GetRewardAccessory()
	{
		return null;
	}

	public static ArrayList GetAllNotCollectedAccessoryArray()
	{
		return null;
	}

	public static Sprite GetBadgeImageForAccessory(DataAccessory accessory)
	{
		return null;
	}

	public static bool IsAccessoryAvailableForNPCUse(DataAccessory accessory)
	{
		return false;
	}

	public static bool HasAccessoryBeenAcquired(DataAccessory accessory)
	{
		return false;
	}
}
