using Game;

public class ScenarioCultDissent : Scenario
{
	private string _narrative;

	private SimCommunard _exCommunard;

	private DataCultExCommunardDissentScenario _dissentData;

	private BracketParser _parser;

	private SimCult _cult;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
