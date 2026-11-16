using System.Collections;
using System.Collections.Generic;

public class ScenarioCasinoAgeUpCelebrityContractOffer : Scenario
{
	private string _narrative;

	private SimCasino _casino;

	private readonly SimCasinoCelebrity _celebrity;

	private List<int> _contractLengths;

	private ArrayList _dropDownContents;

	public ScenarioCasinoAgeUpCelebrityContractOffer(SimCasinoCelebrity celebrity)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
