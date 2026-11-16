using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioDealerCartelMemberMurder : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerCartelMember _member;

	private List<DataDealerCartelMemberMurderScenario> _methods;

	private List<string> _methodStrings;

	private BracketParser _parser;

	public ScenarioDealerCartelMemberMurder(SimDealerCartelMember member)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
