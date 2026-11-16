public class ScenarioCultHeroBlackmailed : Scenario
{
	private string _narrative;

	private SimCommunard _exCommunard;

	private SimCult _cult;

	private double _amount;

	private readonly bool _negotiated;

	public ScenarioCultHeroBlackmailed(double negotiatedAmount = 0.0, SimCommunard exCommunard = null)
	{
	}

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
