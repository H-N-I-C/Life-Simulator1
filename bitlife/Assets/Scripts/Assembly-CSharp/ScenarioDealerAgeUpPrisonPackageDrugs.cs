public class ScenarioDealerAgeUpPrisonPackageDrugs : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly string _package;

	private readonly SimDealerCartelMember _sendingMember;

	private SimDealerDrug _drug;

	private int _doses;

	private readonly BracketParser _parser;

	public ScenarioDealerAgeUpPrisonPackageDrugs(string package, SimDealerCartelMember sendingMember)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
