using System.Collections;

public class ScenarioAstronautDiscoveryPlanet : Scenario
{
	private readonly SimSpacePlanet _planet;

	private readonly SimProbe _probe;

	private readonly SimSpecialJobAstronaut _specialJob;

	private string _narrative;

	private MiniGameDataCelestialBody _gameData;

	public ScenarioAstronautDiscoveryPlanet(SimProbe probe, SimDiscovery planet)
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
