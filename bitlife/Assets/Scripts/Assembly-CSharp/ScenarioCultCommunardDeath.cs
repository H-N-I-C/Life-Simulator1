using Game;

public class ScenarioCultCommunardDeath : Scenario
{
	private string _narrative;

	private readonly SimCommunard _communard;

	private readonly SimCult _cult;

	private DataCultDoctrineRule _rule;

	private readonly string _deathString;

	private BracketParser _parser;

	private readonly bool _includeMemorialize;

	public ScenarioCultCommunardDeath(SimCommunard communard, string deathString, bool includeMemorialize = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
