using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioDealerGangWarStrategy : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerCartel _rivalCartel;

	private List<SimDealerCartelMember> _attackers;

	private ArrayList _attackersDropDownContents;

	private List<DataDealerCartelTakeoverMethod> _methods;

	private ArrayList _methodsDropDownContents;

	public ScenarioDealerGangWarStrategy(SimDealerCartel rivalCartel)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private float GetBailLocalValue(int score)
	{
		return 0f;
	}

	private int GetDeathCount(int score)
	{
		return 0;
	}
}
