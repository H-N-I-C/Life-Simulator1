using System.Collections;

public class ScenarioMafiaAttackKiller : Scenario
{
	protected SimSpecialJobMafia _specialJob;

	protected string _killer;

	protected bool _isRivalFamily;

	protected ArrayList _actionArray;

	protected ArrayList _martialArtMoveArray;

	protected ArrayList _bodyPartArray;

	public ScenarioMafiaAttackKiller(SimSpecialJobMafia theSpecialJob, string theKiller, bool rival)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
