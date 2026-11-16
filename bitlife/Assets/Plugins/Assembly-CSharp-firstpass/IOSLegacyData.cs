using System.Runtime.InteropServices;

public class IOSLegacyData
{
	[PreserveSig]
	private static extern bool IOSGetMissionVarsExist();

	[PreserveSig]
	private static extern bool IOSGetMissionVarsBoolWithKey(string key);

	[PreserveSig]
	private static extern int IOSGetMissionVarsIntWithKey(string key);

	[PreserveSig]
	private static extern int IOSGetMissionVarsArrayCountWithKey(string key);

	[PreserveSig]
	private static extern string IOSGetMissionVarsArrayStringWithKeyAndIndex(string key, int index);

	[PreserveSig]
	private static extern bool IOSGetAchievementsExist();

	[PreserveSig]
	private static extern bool IOSGetAchievementsBoolWithKey(string key);

	[PreserveSig]
	private static extern bool IOSGetCompletedChallengesExist();

	[PreserveSig]
	private static extern int IOSGetCompletedChallengesCount();

	[PreserveSig]
	private static extern bool IOSGetCompletedChallengesBoolWithIndexAndField(int index, string field);

	[PreserveSig]
	private static extern int IOSGetCompletedChallengesIntWithIndexAndField(int index, string field);

	[PreserveSig]
	private static extern bool IOSGetCareerCollectiblesExist();

	[PreserveSig]
	private static extern int IOSGetCareerCollectiblesCount();

	[PreserveSig]
	private static extern string IOSGetCareerCollectiblesStringWithIndexAndField(int index, string field);

	[PreserveSig]
	private static extern bool IOSGetGravesExist();

	[PreserveSig]
	private static extern int IOSGetGravesCount();

	[PreserveSig]
	private static extern string IOSGetGravesStringWithIndexAndField(int index, string field);

	[PreserveSig]
	private static extern int IOSGetGravesIntWithIndexAndField(int index, string field);

	[PreserveSig]
	private static extern bool IOSGetGravesBoolWithIndexAndField(int index, string field);

	[PreserveSig]
	private static extern double IOSGetGravesDoubleWithIndexAndField(int index, string field);

	[PreserveSig]
	private static extern float IOSGetGravesFloatWithIndexAndField(int index, string field);

	[PreserveSig]
	private static extern int IOSGetGravesArrayCountWithIndexAndKey(int index, string key);

	[PreserveSig]
	private static extern int IOSGetGravesArrayIntWithIndexAndArrayIndexAndKeyAndField(int index, int arrayIndex, string key, string field);

	[PreserveSig]
	private static extern string IOSGetGravesArrayStringWithIndexAndArrayIndexAndKey(int index, int arrayIndex, string key);

	[PreserveSig]
	private static extern bool IOSGetCustomPersonsExist();

	[PreserveSig]
	private static extern int IOSGetCustomPersonsCount();

	[PreserveSig]
	private static extern string IOSGetCustomPersonsStringWithIndexAndField(int index, string field);

	[PreserveSig]
	private static extern bool IOSGetCustomPersonsBoolWithIndexAndField(int index, string field);

	[PreserveSig]
	private static extern int IOSGetCustomPersonsIntWithIndexAndField(int index, string field);

	[PreserveSig]
	private static extern bool IOSGetCustomCitiesExist();

	[PreserveSig]
	private static extern int IOSGetCustomCitiesCount();

	[PreserveSig]
	private static extern string IOSGetCustomCitiesStringWithIndexAndField(int index, string field);

	public static bool GetMissionVarsExist()
	{
		return false;
	}

	public static bool GetMissionVarsBoolWithKey(string key)
	{
		return false;
	}

	public static int GetMissionVarsIntWithKey(string key)
	{
		return 0;
	}

	public static int GetMissionVarsArrayCountWithKey(string key)
	{
		return 0;
	}

	public static string GetMissionVarsArrayStringWithKeyAndIndex(string key, int index)
	{
		return null;
	}

	public static bool GetAchievementsExist()
	{
		return false;
	}

	public static bool GetAchievementsBoolWithKey(string key)
	{
		return false;
	}

	public static bool GetCompletedChallengesExist()
	{
		return false;
	}

	public static int GetCompletedChallengesCount()
	{
		return 0;
	}

	public static bool GetCompletedChallengesBoolWithIndexAndField(int index, string field)
	{
		return false;
	}

	public static int GetCompletedChallengesIntWithIndexAndField(int index, string field)
	{
		return 0;
	}

	public static bool GetCareerCollectiblesExist()
	{
		return false;
	}

	public static int GetCareerCollectiblesCount()
	{
		return 0;
	}

	public static string GetCareerCollectiblesStringWithIndexAndField(int index, string field)
	{
		return null;
	}

	public static bool GetGravesExist()
	{
		return false;
	}

	public static int GetGravesCount()
	{
		return 0;
	}

	public static string GetGravesStringWithIndexAndField(int index, string field)
	{
		return null;
	}

	public static int GetGravesIntWithIndexAndField(int index, string field)
	{
		return 0;
	}

	public static bool GetGravesBoolWithIndexAndField(int index, string field)
	{
		return false;
	}

	public static double GetGravesDoubleWithIndexAndField(int index, string field)
	{
		return 0.0;
	}

	public static float GetGravesFloatWithIndexAndField(int index, string field)
	{
		return 0f;
	}

	public static int GetGravesArrayCountWithIndexAndKey(int index, string key)
	{
		return 0;
	}

	public static int GetGravesArrayIntWithIndexAndArrayIndexAndKeyAndField(int index, int arrayIndex, string key, string field)
	{
		return 0;
	}

	public static string GetGravesArrayStringWithIndexAndArrayIndexAndKey(int index, int arrayIndex, string key)
	{
		return null;
	}

	public static bool GetCustomPersonsExist()
	{
		return false;
	}

	public static int GetCustomPersonsCount()
	{
		return 0;
	}

	public static string GetCustomPersonsStringWithIndexAndField(int index, string field)
	{
		return null;
	}

	public static bool GetCustomPersonsBoolWithIndexAndField(int index, string field)
	{
		return false;
	}

	public static int GetCustomPersonsIntWithIndexAndField(int index, string field)
	{
		return 0;
	}

	public static bool GetCustomCitiesExist()
	{
		return false;
	}

	public static int GetCustomCitiesCount()
	{
		return 0;
	}

	public static string GetCustomCitiesStringWithIndexAndField(int index, string field)
	{
		return null;
	}
}
