public class ScenarioHustleScamVictimRefusal : Scenario
{
	protected string _narrative;

	protected DataHustlerScamType _scamType;

	protected DataHustlerScamItem _scamItem;

	protected SimSpecialJobHustler _currentSpecialJobHustler;

	protected SimStreetPerson _streetPerson;

	protected bool _isPull;

	private ScenarioHustleScamVictimRefusal()
	{
	}

	public ScenarioHustleScamVictimRefusal(DataHustlerScamType scamType, SimStreetPerson person, DataHustlerScamItem scamItem, bool isPull)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
