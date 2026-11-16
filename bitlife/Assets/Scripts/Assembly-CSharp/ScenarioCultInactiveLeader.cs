public class ScenarioCultInactiveLeader : Scenario
{
	private string _narrative;

	private SimCult _cult;

	private SimCommunard _communard;

	private bool _leavesForAnotherCommune;

	private BracketParser _parser;

	private string _newPhilosophyName;

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
