public class ScenarioCasinoAgeUpMafiaMemberLaunder : Scenario
{
	private string _narrative;

	private SimCasino _casino;

	private double _cut;

	private SimCasinoMafiaMember _mafiaMember;

	private double _amount;

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
