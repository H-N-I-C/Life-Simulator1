using System.Collections;

public class ScenarioWill : Scenario
{
	protected bool _hasLoverBeneficiary;

	protected int _numChildBeneficiaries;

	protected bool _comingFromRetentionFlow;

	public ScenarioWill(bool comingFromRetentionFlow = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
