using System.Collections;

public class ScenarioAstronautDiscoveryStar : Scenario
{
	private SimSpecialJobAstronaut _specialJob;

	private SimProbe _probe;

	private SimSpaceStar _star;

	private string _narrative;

	private MiniGameDataCelestialBody _gameData;

	public ScenarioAstronautDiscoveryStar(SimProbe probe, SimDiscovery star)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
