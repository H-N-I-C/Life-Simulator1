public class ScenarioRacingRaceCarModInstallResult : Scenario
{
	private string _narrative;

	private readonly SimRaceCar _raceCar;

	private readonly SimRaceCarMod _mod;

	private readonly SimRaceCarMechanic _mechanic;

	private BracketParser _parser;

	private readonly double _cost;

	public ScenarioRacingRaceCarModInstallResult(SimRaceCar raceCar, SimRaceCarMod mod, SimRaceCarMechanic mechanic, double cost)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
