public class ScenarioMafiaHeroKilled : Scenario
{
	protected SimSpecialJobMafia _specialJob;

	protected SimMafiaFamily _family;

	protected string _killer;

	protected bool _isRivalFamily;

	protected string _narrative;

	public ScenarioMafiaHeroKilled(SimMafiaFamily theFamily, string theKiller, bool rival)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
