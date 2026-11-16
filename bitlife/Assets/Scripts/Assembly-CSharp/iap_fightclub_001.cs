public class iap_fightclub_001 : ScenarioFightClubPush
{
	protected override string GetIAPScenarioID()
	{
		return null;
	}

	public override void RefreshScenario()
	{
	}

	protected override void ClickSuccess()
	{
	}

	protected override void ClickFail()
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
