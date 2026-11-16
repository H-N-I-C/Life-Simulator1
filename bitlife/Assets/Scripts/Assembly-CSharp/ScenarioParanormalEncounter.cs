public class ScenarioParanormalEncounter : Scenario
{
	protected DataParanormalEncounter _encounter;

	protected int _heroTerror;

	protected float _ghostFriendliness;

	protected string _narrative;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static SimHouse GetHauntedHouse()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
