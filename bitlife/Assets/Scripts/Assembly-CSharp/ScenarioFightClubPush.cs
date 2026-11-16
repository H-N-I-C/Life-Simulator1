public abstract class ScenarioFightClubPush : ScenarioLegacyPush
{
	public override void RefreshScenario()
	{
	}

	protected sealed override string GetIAPName()
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}

	protected sealed override void PushPaywall()
	{
	}
}
