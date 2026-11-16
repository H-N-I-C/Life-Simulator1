using System.Collections;

public class ScenarioAstronautDiscoveryComet : Scenario
{
	private SimSpecialJobAstronaut _specialJob;

	private SimProbe _probe;

	private SimSpaceComet _comet;

	private string _narrative;

	private MiniGameDataCelestialBody _gameData;

	public ScenarioAstronautDiscoveryComet(SimProbe probe, SimDiscovery comet)
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
