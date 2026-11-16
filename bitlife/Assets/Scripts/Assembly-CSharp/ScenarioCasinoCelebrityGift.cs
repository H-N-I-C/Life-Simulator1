using System.Collections;
using System.Collections.Generic;

public class ScenarioCasinoCelebrityGift : Scenario
{
	private string _narrative;

	private SimCasino _casino;

	private readonly SimCasinoCelebrity _celebrity;

	private List<DataGift> _giftArray;

	public ScenarioCasinoCelebrityGift(SimCasinoCelebrity celebrity)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
