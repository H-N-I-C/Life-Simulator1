using System;

public class ScenarioOutdoorFishingConclusionFeedback : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimOutdoorCatchableFish, string> _003C_003E9__5_0;

		public static Func<SimOutdoorCatchableFish, string> _003C_003E9__6_0;

		public static Func<string, string, string> _003C_003E9__6_2;

		public static Func<string, string, string> _003C_003E9__6_1;

		internal string _003CGetCaughtFishString_003Eb__5_0(SimOutdoorCatchableFish fish)
		{
			return null;
		}

		internal string _003CGetCaughtFishNarrative_003Eb__6_0(SimOutdoorCatchableFish fish)
		{
			return null;
		}

		internal string _003CGetCaughtFishNarrative_003Eb__6_2(string i, string j)
		{
			return null;
		}

		internal string _003CGetCaughtFishNarrative_003Eb__6_1(string i, string j)
		{
			return null;
		}
	}

	private readonly SimOutdoorFishingTrip _fishingTrip;

	private readonly bool _leftvoluntarily;

	private string _narrative;

	public ScenarioOutdoorFishingConclusionFeedback(SimOutdoorFishingTrip fishingTrip, bool leftvoluntarily = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	private string GetCaughtFishString()
	{
		return null;
	}

	private string GetCaughtFishNarrative()
	{
		return null;
	}
}
