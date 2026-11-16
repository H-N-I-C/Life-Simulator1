using System.Collections;

public class ScenarioSchoolClassmateGift : Scenario
{
	protected SimOccupationStudent _student;

	protected SimClassmate _classmate;

	protected ArrayList _giftArray;

	protected bool _requireRewardedVideo;

	public ScenarioSchoolClassmateGift(SimClassmate theClassmate)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private bool RequireRewardedVideoToGet()
	{
		return false;
	}
}
