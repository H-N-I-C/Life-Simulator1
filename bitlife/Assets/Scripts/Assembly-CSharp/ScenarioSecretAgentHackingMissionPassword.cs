using System.Collections.Generic;
using Game;

public class ScenarioSecretAgentHackingMissionPassword : Scenario
{
	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSecretAgentHackingMission _mission;

	private string _correctPassword;

	private List<string> _passwordOptions;

	private readonly int _timeRemaining;

	private int _attemptsRemaining;

	private List<string> _currentHints;

	private readonly bool _firstAttemptAtFirewall;

	private static DataSecretAgentInfiltrationPassword _lastData;

	private bool _accessibilityInPlay;

	public ScenarioSecretAgentHackingMissionPassword(SimSecretAgentHackingMission mission)
	{
	}

	public ScenarioSecretAgentHackingMissionPassword(SimSecretAgentHackingMission mission, List<string> currentHints, string correctPassword, List<string> passwordOptions, int timeRemaining, int attemptsRemaining)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void CountdownTimerExpired()
	{
	}

	public override void CountdownTimerUpdated(int currentTimerValue)
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
