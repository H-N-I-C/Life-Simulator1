public class ScenarioVampireDrinkBlood : Scenario
{
	private string _narrative;

	private DrinkBloodActivity _activity;

	private bool _includeConversionButton;

	private bool _isFirstTimeInThisScenario;

	public ScenarioVampireDrinkBlood(DrinkBloodActivity activity, bool includeConversionButton, bool isFirstTimeInThisScenario = true)
	{
	}

	public override void RefreshScenario()
	{
	}

	private void BuildContinueToDrinkBodyString()
	{
	}

	private void BuildFirstTimeBodyString()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
