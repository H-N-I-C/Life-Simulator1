public class ScenarioFriendNew : Scenario
{
	private readonly string _injectedFeedbackTitle;

	private readonly string _injectedNarrative;

	protected SimPerson _friend;

	protected bool _usedFriendshipBracelet;

	protected string _narrative;

	public ScenarioFriendNew(SimPerson theFriend, bool usedFriendshipBracelet = false)
	{
	}

	public ScenarioFriendNew(SimPerson theFriend, string injectedFeedbackTitle, string injectedNarrative)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
