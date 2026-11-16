public class ScenarioFriendPartyInbound : Scenario
{
	protected SimPerson _friend;

	protected string _yesFeedbackString;

	protected string _yesNarrative;

	protected bool _partyIncludesAlcohol;

	protected int _enjoymentScore;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static SimPerson GetFriend()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
