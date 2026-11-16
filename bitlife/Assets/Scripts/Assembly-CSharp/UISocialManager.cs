using UnityEngine;

public class UISocialManager
{
	private const string DefaultZendeskLocale = "en-us";

	private int shareOrigin__BackingField;

	private Scenario successScenario__BackingField;

	private Scenario failureScenario__BackingField;

	private static UISocialManager inst;

	private int shareOrigin
	{
		get
		{
			return shareOrigin__BackingField;
		}
		set
		{
			shareOrigin__BackingField = value;
		}
	}

	private Scenario successScenario
	{
		get
		{
			return successScenario__BackingField;
		}
		set
		{
			successScenario__BackingField = value;
		}
	}

	private Scenario failureScenario
	{
		get
		{
			return failureScenario__BackingField;
		}
		set
		{
			failureScenario__BackingField = value;
		}
	}

	public static UISocialManager Inst => null;

	public static bool IsIncentivizedShareAvailable()
	{
		return false;
	}

	public void StartIncentivizedShare(int theShareOrigin, Scenario theSuccessScenario, Scenario theFailureScenario)
	{
	}

	public void IncentivizedShareCompleted(string result)
	{
	}

	public void StartUnincentivizedShare(int theShareOrigin, Sprite shareImage = null)
	{
	}

	public void UnincentivizedShareCompleted(bool shared)
	{
	}

	public static void LaunchInstagram()
	{
	}

	public static void LaunchReddit()
	{
	}

	public static void LaunchSnapChat()
	{
	}

	public static void LaunchTikTok()
	{
	}

	public static void LaunchTwitter()
	{
	}

	public static void LaunchYouTube()
	{
	}

	public static void LaunchSupport(bool requestAccountDelete = false)
	{
	}

	private static string GetZendeskLocale()
	{
		return null;
	}

	private static string GetZendeskLocale(string languageCode)
	{
		return null;
	}

	public static void LaunchMerch()
	{
	}

	public static void LaunchEmailFeedback()
	{
	}

	public string UnincentivizedShareStringForShareOrigin(int theShareOrigin)
	{
		return null;
	}

	public static string OrganicSoundingShareStringForHeroNameAndAge(string fullName, int age)
	{
		return null;
	}

	public static string GetShareLink()
	{
		return null;
	}

	public static string GetFriendShareLink()
	{
		return null;
	}
}
