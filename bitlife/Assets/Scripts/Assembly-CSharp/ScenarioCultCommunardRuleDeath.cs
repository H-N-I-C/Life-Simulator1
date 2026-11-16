using Game;

public class ScenarioCultCommunardRuleDeath : Scenario
{
	private string _narrative;

	private SimCommunard _communard;

	private SimCult _cult;

	private DataCultDoctrineRule _rule;

	private string _deathString;

	private BracketParser _parser;

	public ScenarioCultCommunardRuleDeath(SimCommunard communard)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
