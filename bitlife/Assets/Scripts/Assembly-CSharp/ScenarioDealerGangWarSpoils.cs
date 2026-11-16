public class ScenarioDealerGangWarSpoils : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerCartel _rivalCartel;

	private readonly SimDealerCartelMember _member;

	private double _cash;

	public ScenarioDealerGangWarSpoils(SimDealerCartel rivalCartel, SimDealerCartelMember member)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private double GetMoneySpoil(float moneyScore)
	{
		return 0.0;
	}
}
