public class ScenarioCultCommunardRumorInbound : Scenario
{
	private string _narrative;

	private SimCult _cult;

	private SimCommunard _communard;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
