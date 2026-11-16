using System.Collections;
using System.Collections.Generic;

public class ScenarioOutdoorLifestyleSnorkeling : Scenario
{
	private readonly SimOutdoor _outdoor;

	private int _cost;

	private string _siteString;

	private string _narrative;

	private readonly List<string> _birdwatchingSites;

	public ScenarioOutdoorLifestyleSnorkeling(SimOutdoor outdoor)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private string GetRandomSnorkelingSite()
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}

	private ScenarioDropDown CreateSiteDropDown()
	{
		return null;
	}
}
