using Game;

public class ScenarioOutdoorSnorkelingSeeing : Scenario
{
	private readonly SimOutdoor _outdoor;

	private readonly DataOutdoorMarineLife _marineLife;

	private int _enjoyment;

	private readonly DataOutdoorSnorkelingDiscoveryScenario _discoveryScenario;

	private string _narrative;

	public ScenarioOutdoorSnorkelingSeeing(SimOutdoor outdoor, DataOutdoorMarineLife marineLife, DataOutdoorSnorkelingDiscoveryScenario discoveryScenario)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void TryPlay(bool success)
	{
	}

	private void GetAttacked()
	{
	}

	private void ObserveBird()
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	private void PushEndScenario()
	{
	}

	private void HeroDied()
	{
	}

	private void CheckDiscovery(OUTDOOR_HANDBOOK_ENTRY_TYPE animalType)
	{
	}
}
