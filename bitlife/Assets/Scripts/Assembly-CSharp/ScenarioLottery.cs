public class ScenarioLottery : Scenario
{
	protected int _ticketCost;

	protected float _jackpot;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public bool ShouldWinWithTickets(int numTickets)
	{
		return false;
	}
}
