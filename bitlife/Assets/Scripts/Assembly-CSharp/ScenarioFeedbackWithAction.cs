using System;

public class ScenarioFeedbackWithAction : Scenario
{
	private Action _theAction;

	public ScenarioFeedbackWithAction(string title, string body, Action anAction, Scenario nextScenario = null)
	{
	}

	public override void DismissSelected()
	{
	}
}
