using System.Collections;

public class ScenarioPoliticalOfficeRun : Scenario
{
	protected int _office;

	protected bool _isDirectReelection;

	protected SimSpecialJobPolitician _oldSpecialJob;

	protected DataCountryPoliticalSystem _system;

	public ScenarioPoliticalOfficeRun(int theOffice, bool theIsDirectReelection)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
