using Game;

public class ScenarioRacingAgeUpSabotage : Scenario
{
	private readonly SimRaceCarDriver _rival;

	private readonly SimRaceCar _car;

	private readonly bool _vehicleWrecked;

	private double _cost;

	private BracketParser _parser;

	private DataRacingRivalSabotageString _data;

	private string _narrative;

	public ScenarioRacingAgeUpSabotage(SimRaceCar theCar, SimRaceCarDriver rival, bool completelyWreck)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
