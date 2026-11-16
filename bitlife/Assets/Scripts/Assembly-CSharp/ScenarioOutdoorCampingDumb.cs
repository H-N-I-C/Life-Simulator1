using System;
using Game;

public class ScenarioOutdoorCampingDumb : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, bool> _003C_003E9__5_0;

		internal bool _003CRefreshScenario_003Eb__5_0(string x)
		{
			return false;
		}
	}

	private readonly SimOutdoorCampingActivity _campingActivity;

	private DataOutdoorCampingDumbScenario _dumbScenarioData;

	private BracketParser _bracketParser;

	private string _narrative;

	public ScenarioOutdoorCampingDumb(SimOutdoorCampingActivity activity)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private int FindDumbIndex(ScenarioChoice selected)
	{
		return 0;
	}

	private void Impacts(int narrativeIndex)
	{
	}

	private void CreateNarrative(int narrativeIndex)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
