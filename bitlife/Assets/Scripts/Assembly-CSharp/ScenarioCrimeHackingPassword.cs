using System.Collections.Generic;

public class ScenarioCrimeHackingPassword : Scenario
{
	private string _correctPassword;

	private List<string> _passwordOptions;

	private int _timeRemaining;

	private int _attemptsRemaining;

	private string _hint;

	private bool _useTimeLimit;

	private readonly SimHackingTarget _target;

	public ScenarioCrimeHackingPassword(SimHackingTarget target)
	{
	}

	public ScenarioCrimeHackingPassword(SimHackingTarget target, string hint, string correctPassword, List<string> passwordOptions, int timeRemaining, int attemptsRemaining)
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
