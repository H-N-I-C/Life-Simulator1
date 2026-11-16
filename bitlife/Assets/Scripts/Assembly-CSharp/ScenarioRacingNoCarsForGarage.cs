using System.Collections;

public class ScenarioRacingNoCarsForGarage : Scenario
{
	private string _narrative;

	private string _regularCarDealershipName;

	private string _raceCarDealershipName;

	private ArrayList _regularCarDealerships;

	private readonly string _bodyString;

	public ScenarioRacingNoCarsForGarage(string bodyString)
	{
	}

	public override void RefreshScenario()
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
