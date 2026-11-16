using System.Collections;
using System.Collections.Generic;

public class ScenarioCasinoPlayHeroCasinoGame : Scenario
{
	private string _narrative;

	private SimCasino _casino;

	private readonly CASINO_GAME_TYPE _gameType;

	private List<SimCasinoGameRoom> _gameRooms;

	private ArrayList _dropDownContents;

	private SimCasinoGameRoom _defaultGameRoom;

	public ScenarioCasinoPlayHeroCasinoGame(CASINO_GAME_TYPE gameType, SimCasinoGameRoom defaultGameRoom = null)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
