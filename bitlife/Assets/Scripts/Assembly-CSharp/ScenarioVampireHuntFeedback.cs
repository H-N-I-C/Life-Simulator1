public class ScenarioVampireHuntFeedback : Scenario
{
	private string _narrative;

	private readonly SimVampire _heroVampire;

	private readonly SimPerson _vampire;

	private readonly bool _IsMaker;

	private readonly bool _alwaysAgrees;

	public ScenarioVampireHuntFeedback(SimPerson vampire)
	{
	}

	public ScenarioVampireHuntFeedback(SimPerson vampire, bool alwaysAgrees)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
