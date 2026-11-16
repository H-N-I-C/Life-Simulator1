using System.Collections;

public class ScenarioMafiaExtortionAction : Scenario
{
	protected SimSpecialJobMafia _specialJob;

	protected SimMafiaTarget _target;

	protected float _demandedAmount;

	protected float _volunteeredAmount;

	protected int _action;

	protected ArrayList _methodArray;

	protected ArrayList _methodStringArray;

	public ScenarioMafiaExtortionAction(SimMafiaTarget theTarget, SimSpecialJobMafia theSpecialJob, float theDemandedAmount, float theVolunteeredAmount, bool isShakedown)
	{
	}

	public ScenarioMafiaExtortionAction(SimMafiaTarget theTarget, SimSpecialJobMafia theSpecialJob)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
