using Game;

public class ScenarioVampireCouncilCodeReview : Scenario
{
	private const int ChanceOfNothingHappenAfterResult = 55;

	private const int HookupHappinessImpact = 2;

	private const int HookupNotorietyImpact = -1;

	private const int HookupFameImpact = 1;

	private const int BadOpinionHappinessImpact = -2;

	private string _narrative;

	private DataVampireCodeData _vampireCodeData;

	private BracketParser _bracketParser;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private void ApplyBadOpinionImpact()
	{
	}

	private void ApplyHookupImpact()
	{
	}

	private void PushResultScenario(bool councilPassesCode)
	{
	}

	private void PushBadOpinionScenario()
	{
	}

	private void PushHookupScenario()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
