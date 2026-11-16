public class ScenarioHustleScamVictim : Scenario
{
	protected string _narrative;

	protected DataHustlerScamType _scamType;

	protected DataHustlerScamVictimScenario _victimScenario;

	protected DataHustlerScamItem _scamItem;

	protected SimSpecialJobHustler _currentSpecialJobHustler;

	protected SimStreetPerson _streetPerson;

	protected BracketParser _parser;

	protected float _scamAmount;

	protected bool _firstInChain;

	protected bool _isPullScenario;

	protected bool _isPolice;

	private ScenarioHustleScamVictim()
	{
	}

	public ScenarioHustleScamVictim(int scamType, bool firstInChain, SimStreetPerson selectedStreetPerson = null)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private bool WillGetBusted()
	{
		return false;
	}

	public override string EmojiString()
	{
		return null;
	}
}
