using System.Collections;

public class ScenarioFertilitySurrogate : Scenario
{
	protected SimSurrogateSituation _surrogateSituation;

	public ScenarioFertilitySurrogate(SimSurrogateSituation theSurrogateSituation)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private ArrayList GetEggDonorArray()
	{
		return null;
	}

	private ArrayList GetSpermDonorArray()
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}
}
