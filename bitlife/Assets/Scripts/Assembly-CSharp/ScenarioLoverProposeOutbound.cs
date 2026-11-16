using System.Collections;

public class ScenarioLoverProposeOutbound : Scenario
{
	protected SimLover _lover;

	protected ArrayList _settingArray;

	protected string _narrative;

	private bool ShouldForceLoverAcceptsProposal()
	{
		return false;
	}

	public ScenarioLoverProposeOutbound(SimLover theLover)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
