public class ScenarioFriendMeet : Scenario
{
	protected SimPerson _friend;

	private string _acceptNarrative;

	private Scenario _friendshipFeedback;

	private string _overrideEmoji;

	public ScenarioFriendMeet()
	{
	}

	public ScenarioFriendMeet(SimPerson friend)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
