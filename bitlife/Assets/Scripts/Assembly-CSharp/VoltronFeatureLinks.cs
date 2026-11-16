using System.Collections;
using System.Collections.Generic;

public static class VoltronFeatureLinks
{
	public static bool IsBaldingHairForID(string pID)
	{
		return false;
	}

	public static bool IsCurlyAndStraightHairForID(string pID)
	{
		return false;
	}

	public static VoltronChanceItem CreateLinkWithIDAndChance(string pID, int pChance)
	{
		return null;
	}

	public static bool IsCurlyHairForID(string pID)
	{
		return false;
	}

	public static bool IsHijabForID(string pID)
	{
		return false;
	}

	public static Dictionary<string, string> GetBlackListedIDs()
	{
		return null;
	}

	public static ArrayList GetBlackListedCultHairIDs(bool pIsMale)
	{
		return null;
	}

	public static ArrayList GetBlackListedCultFacialHairIDs()
	{
		return null;
	}

	public static ArrayList GetBlackListedVampireFacialHairIDs()
	{
		return null;
	}

	public static ArrayList GetEvolutionLinksForID(string pIDString)
	{
		return null;
	}

	public static ArrayList GetGenderSwapLinksForID(string pIDString)
	{
		return null;
	}

	public static ArrayList GetConnectionLinksForIDAndConnectionType(string pIDString, int pConnectionType)
	{
		return null;
	}

	private static bool GetConnectionLinksForIDAndConnectionTypeForGlasses(string pIDString, int pConnectionType, ref ArrayList pLinks)
	{
		return false;
	}

	private static bool GetConnectionLinksForIDAndConnectionTypeForEyebrows(string pIDString, int pConnectionType, ref ArrayList pLinks)
	{
		return false;
	}

	private static bool GetConnectionLinksForIDAndConnectionTypeForFacialHair(string pIDString, int pConnectionType, ref ArrayList pLinks)
	{
		return false;
	}

	private static bool GetConnectionLinksForIDAndConnectionTypeForHairMale(string pIDString, int pConnectionType, ref ArrayList pLinks)
	{
		return false;
	}

	private static bool GetConnectionLinksForIDAndConnectionTypeForHairFemale(string pIDString, int pConnectionType, ref ArrayList pLinks)
	{
		return false;
	}
}
