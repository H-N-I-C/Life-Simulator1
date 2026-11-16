using Game;

public class ScenarioCasinoAgeUpCelebrityDemand : Scenario
{
	private string _narrative;

	private SimCasino _casino;

	private SimCasinoCelebrity _celebrity;

	private string _perk;

	private DataCasinoCelebrityDemand _perkData;

	private string _perkString;

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
