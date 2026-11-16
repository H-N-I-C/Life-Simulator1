public class ScenarioVampireCouncilCrimsonAudit : Scenario
{
	private string _narrative;

	private SimPerson _person;

	private bool _passesInspection;

	private const int MinHumanDrainedQuantity = 3;

	private const int MaxHumanDrainedQuantity = 25;

	private const int MinVampireCreated = 1;

	private const int MaxVampireCreated = 10;

	private const int MinHuntersKilled = 0;

	private const int MaxHuntersKilled = 5;

	private const int MinTotalToPassInspection = 15;

	private const int ChanceOfNothingHappening = 55;

	private const int CorrectChoiceStandingImpact = 2;

	private const int CorrectChoiceKarmaImpact = 2;

	private const int IncorrectChoiceKarmaImpact = -2;

	private const int IncorrectChoiceStandingImpact = -2;

	private BracketParser _bracketParser;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private void ApplyImpactCorrectChoice()
	{
	}

	private void ApplyImpactIncorrectChoice()
	{
	}

	private void PushRewardScenario()
	{
	}

	private void PushFriendScenario()
	{
	}

	private void PushPunishmentScenario()
	{
	}

	private void PushRageScenario()
	{
	}

	private void PushFamiliarScenario()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
