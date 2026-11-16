using System;

public class ScenarioOutdoorCampingEnd : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimOutdoorPlantDataCache, string> _003C_003E9__5_0;

		public static Func<SimOutdoorAnimalDataCache, string> _003C_003E9__5_1;

		internal string _003CWildLifeEncounteredCombined_003Eb__5_0(SimOutdoorPlantDataCache x)
		{
			return null;
		}

		internal string _003CWildLifeEncounteredCombined_003Eb__5_1(SimOutdoorAnimalDataCache x)
		{
			return null;
		}
	}

	private string _narrative;

	private readonly SimOutdoorCampingActivity _campingActivity;

	private readonly bool _randomizeEnjoyment;

	public ScenarioOutdoorCampingEnd(SimOutdoorCampingActivity campingActivity, bool randomizeEnjoyment = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	private string WildLifeEncounteredCombined()
	{
		return null;
	}

	private string GetBodyString()
	{
		return null;
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
