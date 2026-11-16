using System.Collections;
using System.Collections.Generic;

public class ScenarioCasinoTournamentWager : Scenario
{
	private string _narrative;

	private SimCasino _casino;

	private readonly SimCasinoTournament _tournament;

	private List<SimCasinoGambler> _gamblers;

	private ArrayList _dropDownContents;

	public ScenarioCasinoTournamentWager(SimCasinoTournament tournament)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override void DismissSelected()
	{
	}
}
