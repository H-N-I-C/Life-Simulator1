public class ScenarioBloodBankQuestionnaire : Scenario
{
	protected SimCompany _bloodBank;

	protected string _carriedDisease;

	protected bool _hasKnownBTDisease;

	protected bool _bloodDonation;

	public ScenarioBloodBankQuestionnaire(SimCompany bloodBank, bool isBloodDonation)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
