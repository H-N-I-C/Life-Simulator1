using System.Collections;

public class ScenarioAstronautClassmateGift : Scenario
{
	private readonly SimSpecialJobAstronaut _specialJob;

	private readonly SimAstronaut _classmate;

	private ArrayList _giftArray;

	private bool _requireRewardedVideo;

	public ScenarioAstronautClassmateGift(SimAstronaut theClassmate)
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
