public class ScenarioEncounterProstitute : Scenario
{
	protected bool _happenedOnWalk;

	protected int _prostituteGender;

	protected string _prostituteTerm;

	protected string _prostituteAction;

	protected bool _requireCompatibleProstitute;

	public ScenarioEncounterProstitute()
	{
	}

	public ScenarioEncounterProstitute(bool requireCompatibleProstitute = false)
	{
	}

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

	public new static bool ScenarioIsAgeAppropriateForPush()
	{
		return false;
	}
}
