using System.Collections;

public class ScenarioOutdoorFishingTrip : Scenario
{
	private readonly SimOutdoor _simOutdoor;

	private string _narrative;

	public ScenarioOutdoorFishingTrip(SimOutdoor simOutdoor)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private ScenarioDropDown CreateLocationDropDown()
	{
		return null;
	}

	private ScenarioDropDown CreateBaitDropDown()
	{
		return null;
	}

	private void CreateNarrativeString(string location)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
