using System.Collections;

public class ScenarioSportsPractice : Scenario
{
	protected SimSpecialJobAthlete _specialJob;

	private bool _requireRewardedVideo;

	public ScenarioSportsPractice(SimSpecialJobAthlete theSpecialJob)
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

	public override string EmojiString()
	{
		return null;
	}
}
