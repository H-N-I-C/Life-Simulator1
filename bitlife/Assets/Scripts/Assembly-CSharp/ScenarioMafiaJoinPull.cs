using System.Collections;

public class ScenarioMafiaJoinPull : Scenario
{
	protected DataMafiaMob _mob;

	protected SimMafiaFamily _family;

	protected ArrayList _methodArray;

	public ScenarioMafiaJoinPull(DataMafiaMob theMob)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public static int GetCriminalRecordScore()
	{
		return 0;
	}

	public override string EmojiString()
	{
		return null;
	}
}
