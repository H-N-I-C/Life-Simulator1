using System.Collections.Generic;

public class ScenarioSecretAgentInfiltratePassword : Scenario
{
	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimRivalSpyAgency _rivalAgency;

	private string _correctPassword;

	private List<string> _passwordOptions;

	private readonly int _timeRemaining;

	private int _currentPasswordSet;

	private int _passwordsToCrack;

	private int _attemptsRemaining;

	private string _currentHint;

	private readonly bool _firstAttemptAtCurrentFirewall;

	private static string _lastPasswordAttempt;

	private bool _accessibilityInPlay;

	public ScenarioSecretAgentInfiltratePassword(SimRivalSpyAgency rivalAgency, int currentPasswordSet = 1)
	{
	}

	public ScenarioSecretAgentInfiltratePassword(SimRivalSpyAgency rivalAgency, int currentPasswordSet, string currentHint, string correctPassword, List<string> passwordOptions, int timeRemaining, int attemptsRemaining)
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
