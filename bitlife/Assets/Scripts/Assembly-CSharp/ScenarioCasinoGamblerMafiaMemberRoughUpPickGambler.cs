using System.Collections;
using System.Collections.Generic;

public class ScenarioCasinoGamblerMafiaMemberRoughUpPickGambler : Scenario
{
	private string _narrative;

	private SimCasino _casino;

	private readonly SimCasinoMafiaMember _mafiaMember;

	private readonly double _amount;

	private List<SimCasinoGambler> _gamblers;

	private ArrayList _dropDownContents;

	public ScenarioCasinoGamblerMafiaMemberRoughUpPickGambler(SimCasinoMafiaMember mafiaMember, double amount)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
