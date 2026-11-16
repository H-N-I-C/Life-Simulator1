using System;
using System.Collections;
using Game;

public class ScenarioVampireDrinkBloodPickLocation : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataVampireFeedingLocation, string> _003C_003E9__3_0;

		internal string _003CRefreshScenario_003Eb__3_0(DataVampireFeedingLocation locationData)
		{
			return null;
		}
	}

	private readonly DrinkBloodActivity _activity;

	private string _narrative;

	public ScenarioVampireDrinkBloodPickLocation(SimPerson person)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private void FormatPersonPossessive(BracketParser parser, SimPerson person)
	{
	}

	private void UpdateNarrative()
	{
	}

	private void ShowLowHealthFeedback()
	{
	}

	private void ShowRefusalFeedback()
	{
	}

	private void ShowVampireDrinkBloodScenario(string location)
	{
	}

	private void ShowVampireDrinkBloodArgumentScenario(string location)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
