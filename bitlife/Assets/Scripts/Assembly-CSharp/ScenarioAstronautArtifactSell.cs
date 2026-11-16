using System.Collections;

public class ScenarioAstronautArtifactSell : Scenario
{
	private readonly SimSpecialJobAstronaut _special;

	private readonly SimArtifact _artifact;

	private string _narrative;

	private readonly DataStreetPerson _buyer;

	public ScenarioAstronautArtifactSell(SimSpecialJobAstronaut special, SimArtifact artifact)
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
