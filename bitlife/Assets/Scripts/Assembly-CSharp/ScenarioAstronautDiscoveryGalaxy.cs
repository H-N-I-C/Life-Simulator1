using System.Collections;

public class ScenarioAstronautDiscoveryGalaxy : Scenario
{
	private SimSpecialJobAstronaut _specialJob;

	private SimProbe _probe;

	private SimSpaceGalaxy _galaxy;

	private string _narrative;

	private MiniGameDataCelestialBody _gameData;

	public ScenarioAstronautDiscoveryGalaxy(SimProbe probe, SimDiscovery galaxy)
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
