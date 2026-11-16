using System.Collections.Generic;

public class ScenarioOutdoorVehicleProfile : Scenario
{
	private readonly SimRecreationalVehicle _recreationalVehicle;

	private string _narrative;

	public ScenarioOutdoorVehicleProfile(SimRecreationalVehicle recreationalVehicle)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	private Dictionary<string, string> GetTableDictionary()
	{
		return null;
	}
}
