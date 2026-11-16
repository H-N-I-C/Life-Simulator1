public class ScenarioLoverSteals : Scenario
{
	protected int _moneyTaken;

	protected int _type;

	protected SimCar _car;

	protected string _narrative;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
