public class ScenarioDealerGangWarPayment : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerCartelMember _member;

	private readonly double _spoilsAmount;

	private double _cutRequestAmount;

	public ScenarioDealerGangWarPayment(SimDealerCartelMember member, double spoilsAmount)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
