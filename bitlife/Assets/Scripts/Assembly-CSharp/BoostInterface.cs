using System;

public class BoostInterface
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<(int, int, int), int> _003C_003E9__0_0;

		public static Func<(int, int, int), int> _003C_003E9__0_1;

		internal int _003CGetAvailableBoost_003Eb__0_0((int, int, int) att)
		{
			return 0;
		}

		internal int _003CGetAvailableBoost_003Eb__0_1((int, int, int) att)
		{
			return 0;
		}
	}

	public static DataBoost GetAvailableBoost()
	{
		return null;
	}

	public static string StringForAttribute(int attribute)
	{
		return null;
	}

	public static int BoostImpactForAttributeAfterWatchingAd(int attribute, bool afterAd)
	{
		return 0;
	}

	public static bool RequireRewardedVideo()
	{
		return false;
	}

	public static int AttributeThresholdForHidingBoost()
	{
		return 0;
	}
}
