using System.Collections;

public class ScenarioModelAgencyModelGift : Scenario
{
	protected SimModel _model;

	private SimSpecialJobModel _modelJob;

	protected ArrayList _giftArray;

	protected bool _requireRewardedVideo;

	public ScenarioModelAgencyModelGift(SimModel model)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public bool RequireRewardedVideoToGet()
	{
		return false;
	}
}
