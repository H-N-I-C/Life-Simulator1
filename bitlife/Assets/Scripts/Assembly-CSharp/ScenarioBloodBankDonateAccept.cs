public class ScenarioBloodBankDonateAccept : Scenario
{
	protected string _narrative;

	protected bool _bloodDonation;

	protected SimCompany _bloodBank;

	public ScenarioBloodBankDonateAccept(bool isBloodDonation, SimCompany bloodBank)
	{
	}

	public override void RefreshScenario()
	{
	}

	private float CalculateProbabilityOfDisease()
	{
		return 0f;
	}

	public override void DismissSelected()
	{
	}
}
