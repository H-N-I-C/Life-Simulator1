public class ScenarioVampireHuntPerson : Scenario
{
	private readonly VampireHuntActivity _activity;

	private string _narrative;

	private SimPerson _person;

	private int _attempts;

	public ScenarioVampireHuntPerson(VampireHuntActivity activity, int attempts = 1)
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
