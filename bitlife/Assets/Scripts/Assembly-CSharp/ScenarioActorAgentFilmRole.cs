public class ScenarioActorAgentFilmRole : Scenario
{
	protected SimActorFilmRole _role;

	protected SimActorFilm _film;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
