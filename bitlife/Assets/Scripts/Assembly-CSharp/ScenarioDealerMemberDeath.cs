public class ScenarioDealerMemberDeath : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerCartelMember _member;

	private readonly string _deathString;

	private readonly string _narrativeString;

	public ScenarioDealerMemberDeath(SimDealerCartelMember member, string deathString, string narrativeString)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
