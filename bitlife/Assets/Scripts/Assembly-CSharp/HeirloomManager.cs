using System;
using UnityEngine;

public class HeirloomManager
{
	public static SimHeirloom GenerateAnHeirloom()
	{
		return null;
	}

	public static SimHeirloom GenerateSpecificHeirloom(int index)
	{
		return null;
	}

	public static SimHeirloom GenerateAnExtraHeirloomIfApplicable()
	{
		return null;
	}

	public static SimHeirloom GenerateUnownedLuxuryHeirloomForCurrentActiveLifeOrNull(double maxValue = 0.0)
	{
		return null;
	}

	public static SimHeirloom GenerateHeirloomWithMaximumValueRangeOrNull(double lowerMaxValue, double upperMaxValue)
	{
		return null;
	}

	public static void AcquireHeirloom(SimHeirloom heirloom, bool includeNarrative = true)
	{
	}

	public static DateTime DateOfNextHeirloom()
	{
		return default;
	}

	public static Sprite GetBadgeImageForHeirloomData(DataHeirloom heirloomData)
	{
		return null;
	}

	public static int NumHeirloomsFound()
	{
		return 0;
	}

	public static bool AlreadyEarnedHeirloomForCurrentLife(DataHeirloom heirloomData)
	{
		return false;
	}

	public static bool LuxuryHeirloomsAvailableForCurrentActiveLife()
	{
		return false;
	}
}
