public class ScenarioRacingRaceCarCondition : Scenario
{
	private string _narrative;

	private SimRacing _racing;

	private SimRaceCar _raceCar;

	public ScenarioRacingRaceCarCondition(SimRaceCar raceCar)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public static bool CarConditionMeetsRequirements(SimRaceCar raceCar)
	{
		return false;
	}
}
