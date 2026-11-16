using System.Collections.Generic;
using Game;

public class ScenarioDealerSupplyRunDumb : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerSupplyRun _supplyRun;

	private DataDealerDrugRunDumbScenario _data;

	private BracketParser _parser;

	private List<string> _buttons;

	public ScenarioDealerSupplyRunDumb(SimDealerSupplyRun supplyRun)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
