using Game;

public class ScenarioCultCommuneStructureBuildPay : Scenario
{
	private string _narrative;

	private readonly DataCultCommuneStructure _structure;

	private double _cost;

	private readonly bool _rejectedByFollowers;

	private SimCult _cult;

	private string _companyName;

	private int _companyReputation;

	public ScenarioCultCommuneStructureBuildPay(DataCultCommuneStructure structure, double cost = 0.0, string companyName = null, int companyReputation = 0, bool rejectedByFollowers = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
