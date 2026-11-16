using Game;

public class ScenarioCultCommunardTotemDisrespect : Scenario
{
	private string _narrative;

	private readonly SimCommunard _communard;

	private readonly SimCult _cult;

	private readonly SimTotem _totem;

	private DataCultTotemDisrespect _data;

	private readonly BracketParser _parser;

	public ScenarioCultCommunardTotemDisrespect(SimTotem totem)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
