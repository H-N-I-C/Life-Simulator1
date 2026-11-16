using System.Collections;

public class ScenarioVampireBuyCastleCoffin : Scenario
{
	private static readonly VampireHouseSoundPlayer SoundPlayer;

	private string _narrative;

	private readonly SimHouse _house;

	public ScenarioVampireBuyCastleCoffin(SimHouse house)
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

	public static void OnAnyClosedTryAppend(Scenario scenario)
	{
	}

	private static SimHouse GetHouseIfShouldShowScenario(ArrayList houseArray)
	{
		return null;
	}

	private static bool ShouldAddCoffinIfVampireCastle(SimHouse house)
	{
		return false;
	}
}
