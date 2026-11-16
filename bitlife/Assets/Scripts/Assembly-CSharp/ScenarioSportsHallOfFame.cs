public class ScenarioSportsHallOfFame : Scenario
{
	private string _narrative;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static DataSportsLeague GetHallOfFameLeague()
	{
		return null;
	}

	public static int GetHallOfFamePointsForLeague(DataSportsLeague league)
	{
		return 0;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
