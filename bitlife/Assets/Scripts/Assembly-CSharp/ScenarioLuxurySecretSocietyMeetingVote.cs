using Game;

public class ScenarioLuxurySecretSocietyMeetingVote : Scenario
{
	private string _narrative;

	private SimLuxury _luxury;

	private readonly SimLuxurySecretSociety _society;

	private string _directiveString;

	private bool _correctDirective;

	private DataLuxurySecretSocietyRules _ruleItem;

	public ScenarioLuxurySecretSocietyMeetingVote(SimLuxurySecretSociety society)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
