using System.Collections;

public class ScenarioAstronautDiscoveryBlackHole : Scenario
{
	private SimSpecialJobAstronaut _specialJob;

	private SimProbe _probe;

	private SimSpaceBlackHole _blackHole;

	private string _narrative;

	private MiniGameDataCelestialBody _gameData;

	public ScenarioAstronautDiscoveryBlackHole(SimProbe probe, SimDiscovery blackHole)
	{
	}

	public override void RefreshScenario()
	{
	}

	private bool IsSuperMassiveBlackHole()
	{
		return false;
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
