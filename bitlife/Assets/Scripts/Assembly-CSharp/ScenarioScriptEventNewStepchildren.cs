using System.Collections;

public class ScenarioScriptEventNewStepchildren : Scenario
{
	protected string _narrative;

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	public string GetStepchildrenTitle()
	{
		return null;
	}

	public string GetStepgrandchildrenTitleFromArray(ArrayList stepgrandchildrenArray)
	{
		return null;
	}

	public new bool PreventRandomPushAfterScenario()
	{
		return false;
	}
}
