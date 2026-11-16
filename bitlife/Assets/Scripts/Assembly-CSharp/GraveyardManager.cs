using System;
using System.Collections;
using System.Collections.Generic;

public class GraveyardManager
{
	private sealed class _003C_003Ec__DisplayClass7_0
	{
		public Grave grave;

		public string requestedLanguage;

		public List<TranslationEntry> translationEntries;

		internal void _003CAddGraveForCurrentLifeAndWillHauntGame_003Eb__0(List<string> result)
		{
		}
	}

	public ArrayList GraveArray;

	private static GraveyardManager SharedInstance;

	public static GraveyardManager SharedGraveyardManager => null;

	public void LoadGraves()
	{
	}

	public void SaveGraves()
	{
	}

	public static void AddGraveForCurrentLifeAndWillHauntGame(bool hauntGame)
	{
	}

	public static void RemoveLastGrave()
	{
	}

	public static void RemoveGrave(Grave grave)
	{
	}

	public Grave GetGraveForAncestor(SimAncestor ancestor)
	{
		return null;
	}

	public bool LastPlayedGraveBulldozed()
	{
		return false;
	}

	public int NumberOfUniqueHeroGravesWithGenerationID(string pGenerationID)
	{
		return 0;
	}

	public ArrayList GravesWithGenerationID(string pGenerationID)
	{
		return null;
	}

	public static void SetBadgeForGrave(Grave grave)
	{
	}

	public static string GetStringForBadge(int badge)
	{
		return null;
	}

	public static ArrayList GetAllBadges()
	{
		return null;
	}

	public static bool NewlyEarnedBadge(int pBadge)
	{
		return false;
	}

	public static ArrayList GetAllBadgesWithEarnedFirst()
	{
		return null;
	}

	public static bool HasUserEarnedBadge(int badge)
	{
		return false;
	}

	public static bool HasUserEarnedBadge(string badgeName)
	{
		return false;
	}

	public static bool ShouldObfuscateBadge(int badge)
	{
		return false;
	}

	public static int NumPossibleBadges()
	{
		return 0;
	}

	public static int NumBadgesEarned()
	{
		return 0;
	}

	public static DateTime GetEarliestEarnedDateForBadge(int pBadge)
	{
		return default;
	}

	public static int GetEarliestGraveIndexForBadge(int pBadge)
	{
		return 0;
	}

	public static bool BadgeEarnedEarlierThanBadge(int pBadge1, int pBadge2)
	{
		return false;
	}

	public static string GetShortDescriptionForBadge(int badge)
	{
		return null;
	}
}
