public class ScenarioLoverDemandsStuff : Scenario
{
	protected bool _demandsHouse;

	protected bool _demandsCar;

	protected string _narrative;

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}

	public override string EmojiString()
	{
		return null;
	}
}
