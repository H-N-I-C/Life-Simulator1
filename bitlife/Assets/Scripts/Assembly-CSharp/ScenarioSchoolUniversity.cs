using System.Collections;

public class ScenarioSchoolUniversity : Scenario
{
	protected ArrayList MajorArray;

	protected bool AfterSchoolGraduateScenario;

	public ScenarioSchoolUniversity()
	{
	}

	public ScenarioSchoolUniversity(bool afterSchoolScenario = true)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public bool WillBeAccepted()
	{
		return false;
	}

	public new static bool ScenarioIsAgeAppropriateForPush()
	{
		return false;
	}
}
