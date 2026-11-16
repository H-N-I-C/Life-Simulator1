using System.Collections;
using System.Collections.Generic;

public class ScenarioCasinoBet : Scenario
{
	protected SimCasinoSession _session;

	private RouletteBet Bet__BackingField;

	private List<float> _slotsBetAmounts;

	private ArrayList _dropDownContents;

	private string _buttonActionString;

	public RouletteBet Bet
	{
		get
		{
			return Bet__BackingField;
		}
		set
		{
			Bet__BackingField = value;
		}
	}

	public ScenarioCasinoBet(SimCasinoSession theSession)
	{
	}

	public override void DismissSelected()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private void LeaveCasino()
	{
	}
}
