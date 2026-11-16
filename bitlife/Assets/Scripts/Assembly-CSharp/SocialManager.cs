using System;
using UnityEngine;

public class SocialManager
{
	private static NativeShare NSInstance;

	private static string AndroidEmailPackageName;

	private static string AndroidTextPackageName;

	private static DateTime ShareTimer;

	public static void StartIncentivizedShare(Sprite shareImage, string shareString, int shareIndex = 1)
	{
	}

	public static void IncentivizedShareCallback(NativeShare.ShareResult result, string shareTarget)
	{
	}

	public static void UnIncentivizedShareCallback(NativeShare.ShareResult result, string shareTarget)
	{
	}

	public static void StartUnincentivizedShare(Sprite shareImagePath, string shareString)
	{
	}

	public static void AttachImage(Sprite image)
	{
	}

	public static bool UserHasSMSCapability()
	{
		return false;
	}

	public static bool UserHasEmailCapability()
	{
		return false;
	}

	public static void DraftEmail(string toAddress, string subject)
	{
	}

	public static bool UserHasInstagramApp()
	{
		return false;
	}

	public static void LaunchInstagram(string username)
	{
	}

	public static bool UserHasRedditApp()
	{
		return false;
	}

	public static void LaunchReddit(string sub)
	{
	}

	public static bool UserHasSnapchatApp()
	{
		return false;
	}

	public static void LaunchSnapchat(string snapchatID)
	{
	}

	public static bool UserHasTikTokApp()
	{
		return false;
	}

	public static void LaunchTikTok(string username)
	{
	}

	public static bool UserHasTwitterApp()
	{
		return false;
	}

	public static void LaunchTwitter(string username)
	{
	}

	public static bool UserHasYouTubeApp()
	{
		return false;
	}

	public static void LaunchYouTube(string youTubeID)
	{
	}

	public static void LaunchWeb(string url)
	{
	}
}
