public class ScenarioGamePrisonEscape : Scenario
{
	protected SimOccupationPrisoner _prisoner;

	protected MiniGameDataTheseus _theseusData;

	public ScenarioGamePrisonEscape(SimOccupationPrisoner thePrisoner)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void MiniGameCompleted()
	{
	}

	public bool OfferRewardedVideoToEscape()
	{
		return false;
	}

	public override string EmojiString()
	{
		return null;
	}
}
