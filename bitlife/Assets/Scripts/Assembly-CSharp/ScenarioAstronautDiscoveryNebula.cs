using System.Collections;

public class ScenarioAstronautDiscoveryNebula : Scenario
{
	private SimSpecialJobAstronaut _specialJob;

	private SimProbe _probe;

	private SimSpaceNebula _nebula;

	private string _narrative;

	private MiniGameDataCelestialBody _gameData;

	public ScenarioAstronautDiscoveryNebula(SimProbe probe, SimDiscovery nebula)
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
