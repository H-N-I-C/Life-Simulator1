public class ScenarioCultDemandTotem : Scenario
{
	private string _narrative;

	private readonly SimCommunard _communard;

	private readonly SimCult _cult;

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
