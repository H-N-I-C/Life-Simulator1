using System.Collections.Generic;

public class ScenarioAstronautMissionAnxiety : Scenario
{
	private string _narrative;

	private readonly SimSpecialJobAstronaut _specialJob;

	private readonly SimPerson _person;

	private readonly SimSpaceMission _mission;

	private readonly DataAstronautAnxietyScenario _scenarioData;

	public ScenarioAstronautMissionAnxiety(SimSpecialJobAstronaut specialJob, SimPerson person, SimSpaceMission mission)
	{
	}

	public override void RefreshScenario()
	{
	}

	private string GetRelation()
	{
		return null;
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static SimPerson GetPersonFromHeroLife(List<string> items)
	{
		return null;
	}

	private static bool MeetsBegPersonRequirement(List<string> list, string item)
	{
		return false;
	}
}
