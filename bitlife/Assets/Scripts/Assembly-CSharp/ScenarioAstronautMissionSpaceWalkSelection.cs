using System;
using System.Collections;

public class ScenarioAstronautMissionSpaceWalkSelection : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Predicate<string> _003C_003E9__5_0;

		internal bool _003CChoiceSelectedAtIndex_003Eb__5_0(string t)
		{
			return false;
		}
	}

	private string _narrative;

	private readonly SimSpecialJobAstronaut _specialJob;

	private readonly SimSpaceMission _mission;

	public ScenarioAstronautMissionSpaceWalkSelection(SimSpaceMission mission)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
