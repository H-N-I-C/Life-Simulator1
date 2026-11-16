using System.Collections.Generic;

public class ScenarioZooSupplierCandidate : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	private readonly List<SimZooSupplier> _suppliers;

	private int _supplierIndex;

	private readonly SimZooSupplier _candidate;

	public ScenarioZooSupplierCandidate(List<SimZooSupplier> suppliers, int supplierIndex = 0)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
