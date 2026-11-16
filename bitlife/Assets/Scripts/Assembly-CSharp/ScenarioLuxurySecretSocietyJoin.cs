public class ScenarioLuxurySecretSocietyJoin : Scenario
{
	private string _narrative;

	private SimLuxury _luxury;

	private readonly SimLuxurySecretSociety _society;

	public ScenarioLuxurySecretSocietyJoin(SimLuxurySecretSociety society)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private bool CheckJoinCondition(SimLuxurySecretSociety society)
	{
		return false;
	}
}
