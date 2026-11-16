using System.Collections;

public class ScenarioDoctorTreatments : Scenario
{
	protected SimDoctor _doctor;

	protected bool _inPushFlow;

	protected string _narrative;

	protected ArrayList _treatableDiseaseArray;

	public ScenarioDoctorTreatments(SimDoctor theDoctor, bool inPushFlow)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
