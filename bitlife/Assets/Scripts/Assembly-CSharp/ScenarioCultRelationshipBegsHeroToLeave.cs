using Game;

public class ScenarioCultRelationshipBegsHeroToLeave : Scenario
{
	private string _narrative;

	private readonly SimPerson _randomRelationshipPerson;

	private readonly string _relationshipString;

	private static DataCultAntiCommuneNagScenario _data;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private static (SimPerson, string) GetPersonFromHeroLife()
	{
		return default;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
