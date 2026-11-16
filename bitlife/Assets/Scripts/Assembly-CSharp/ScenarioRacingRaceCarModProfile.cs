public class ScenarioRacingRaceCarModProfile : Scenario
{
	private string _narrative;

	private SimRacing _racing;

	private readonly SimRaceCar _raceCar;

	private readonly SimRaceCarMod _mod;

	private float _estimatedCostToRepair;

	private float _estimatedCostToReplace;

	private readonly float _modCondition;

	public ScenarioRacingRaceCarModProfile(SimRaceCar raceCar, SimRaceCarMod mod)
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
