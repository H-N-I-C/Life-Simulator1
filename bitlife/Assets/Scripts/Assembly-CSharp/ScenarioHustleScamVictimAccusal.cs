public class ScenarioHustleScamVictimAccusal : Scenario
{
	protected string _narrative;

	protected SimStreetPerson _streetPerson;

	protected SimSpecialJobHustler _currentSpecialJobHustler;

	protected float _amountScammed;

	protected int _scamType;

	protected bool _isPullScenario;

	private ScenarioHustleScamVictimAccusal()
	{
	}

	public ScenarioHustleScamVictimAccusal(SimStreetPerson scamRecepient, float amountScammed, int scamType, bool isPullScenario)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
