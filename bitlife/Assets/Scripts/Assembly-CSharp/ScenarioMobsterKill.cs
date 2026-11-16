using System.Collections;

public class ScenarioMobsterKill : Scenario
{
	protected SimMobster _mobster;

	protected SimSpecialJobMafia _specialJob;

	protected bool _push;

	protected bool _afterCollectingEvidence;

	protected ArrayList _methodArray;

	public ScenarioMobsterKill(SimMobster theMobster)
	{
	}

	public ScenarioMobsterKill(SimSpecialJobMafia theSpecialJob, SimMobster theMobster, bool theAfterCollectingEvidence)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
