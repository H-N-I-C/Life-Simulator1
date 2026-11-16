public class ScenarioCasinoAgeUpMafiaMemberInterrogation : Scenario
{
	private string _narrative;

	private SimCasino _casino;

	private SimCasinoMafiaMember _mafiaMember;

	private double _offer;

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
