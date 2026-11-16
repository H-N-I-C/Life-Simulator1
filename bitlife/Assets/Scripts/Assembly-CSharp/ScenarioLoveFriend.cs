public class ScenarioLoveFriend : Scenario
{
	private readonly SimPerson _friend;

	private readonly SimSeniorPerson _seniorFriend;

	private SimLover _loverCandidate;

	private BracketParser _parser;

	private string _relationshipTemplate;

	public ScenarioLoveFriend(SimPerson theFriend)
	{
	}

	public ScenarioLoveFriend(SimSeniorPerson seniorFriend)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
