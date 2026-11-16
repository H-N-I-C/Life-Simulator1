using System.Collections;

public class ScenarioMafiaPromotionMadeManKill : Scenario
{
	protected SimSpecialJobMafia _specialJob;

	protected bool _isPartOfPromotion;

	protected SimMurderTarget _murderTarget;

	public ScenarioMafiaPromotionMadeManKill(SimSpecialJobMafia theSpecialJob, SimMurderTarget theMurderTarget)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public int GetMurderAttemptOutcomeForMethod(DataMurder murder)
	{
		return 0;
	}

	public override string EmojiString()
	{
		return null;
	}
}
