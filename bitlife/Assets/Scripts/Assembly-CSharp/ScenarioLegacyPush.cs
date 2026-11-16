public abstract class ScenarioLegacyPush : Scenario
{
	protected abstract string GetIAPName();

	protected abstract string GetIAPScenarioID();

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	protected virtual void PushPaywall()
	{
	}

	protected virtual void ClickSuccess()
	{
	}

	protected virtual void ClickFail()
	{
	}
}
