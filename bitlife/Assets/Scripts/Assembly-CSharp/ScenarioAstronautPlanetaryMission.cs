using System;

public class ScenarioAstronautPlanetaryMission : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<char, bool> _003C_003E9__11_0;

		internal bool _003CRefreshScenario_003Eb__11_0(char t)
		{
			return false;
		}
	}

	private string _narrative;

	private readonly SimSpecialJobAstronaut _specialJob;

	private readonly SimSpaceMission _mission;

	private readonly int _discoveryType;

	private readonly string _planetName;

	private string _logMission;

	private string _bodyMission;

	private string _logSuccess;

	private string _logFailure;

	private SimAstronaut _director;

	public ScenarioAstronautPlanetaryMission(SimSpaceMission mission, int discoveryType)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void CheckForAchievement()
	{
	}

	private string GenerateReason(SimAstronaut person)
	{
		return null;
	}

	private Scenario ScenarioVolunteer()
	{
		return null;
	}

	private Scenario ScenarioAskCrewmate(SimAstronaut person, string reason)
	{
		return null;
	}

	private Scenario ScenarioGoofOff()
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}
}
