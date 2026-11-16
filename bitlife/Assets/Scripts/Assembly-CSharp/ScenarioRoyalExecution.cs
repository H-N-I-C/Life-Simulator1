using System.Collections;

public class ScenarioRoyalExecution : Scenario
{
	protected SimPerson _reactionPerson;

	protected string _reactionPersonNameScenario;

	protected string _reactionPersonNameNarrative;

	public ScenarioRoyalExecution()
	{
	}

	public ScenarioRoyalExecution(SimPerson thePerson, string thePersonNameScenario, string thePersonNameNarrative)
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

	public bool IsReaction()
	{
		return false;
	}

	public void ImpactKarmaForEvenConsideringIt()
	{
	}
}
