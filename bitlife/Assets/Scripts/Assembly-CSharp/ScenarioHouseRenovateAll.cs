using System.Collections;

public class ScenarioHouseRenovateAll : Scenario
{
	private bool _requireRewardedVideo;

	private ArrayList _availableHouses;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public float GetAverageHouseCondition()
	{
		return 0f;
	}

	public bool RequireRewardedVideoToGet()
	{
		return false;
	}

	public ArrayList RenovationBudgetArrayForAllHouses()
	{
		return null;
	}
}
