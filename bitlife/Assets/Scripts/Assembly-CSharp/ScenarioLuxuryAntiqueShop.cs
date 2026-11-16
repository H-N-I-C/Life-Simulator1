public class ScenarioLuxuryAntiqueShop : Scenario
{
	private string _narrative;

	private readonly SimLuxury _luxury;

	private readonly SimHeirloom _heirloom;

	private readonly bool _unownedLuxHeirloomExists;

	private SimHeirloom _rvHeirloom;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private SimHeirloom GetRVHeirloomOrNull()
	{
		return null;
	}
}
