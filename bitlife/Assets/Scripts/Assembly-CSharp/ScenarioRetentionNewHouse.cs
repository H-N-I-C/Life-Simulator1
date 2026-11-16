public class ScenarioRetentionNewHouse : Scenario
{
	private SimHouse _house;

	private string _description;

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

	public static bool WillOfferNewHouse()
	{
		return false;
	}

	private void CreateHouse()
	{
	}
}
