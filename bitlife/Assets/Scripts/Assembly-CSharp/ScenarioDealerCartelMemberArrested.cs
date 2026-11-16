using System.Collections;

public class ScenarioDealerCartelMemberArrested : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerCartelMember _member;

	private readonly ArrayList _crimeList;

	private readonly double _bailAmount;

	private readonly bool _includePayOption;

	public ScenarioDealerCartelMemberArrested(SimDealerCartelMember member, ArrayList crimeList, double bailAmount, bool includePayOption = true)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
