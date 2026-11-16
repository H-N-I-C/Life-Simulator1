using System.Collections;

public class ScenarioSportsInjuryRehabSelect : Scenario
{
	protected SimSpecialJobAthlete _specialJob;

	protected SimSportsInjury _injury;

	private ArrayList _eligibleInjuryArray;

	private string _narrative;

	public ScenarioSportsInjuryRehabSelect(SimSpecialJobAthlete theSpecialJob)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
