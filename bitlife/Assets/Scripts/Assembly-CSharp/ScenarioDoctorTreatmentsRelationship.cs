using System.Collections;

public class ScenarioDoctorTreatmentsRelationship : Scenario
{
	protected SimChild _child;

	protected SimParent _parent;

	protected SimPerson _person;

	protected SimDoctor _doctor;

	protected bool _inPushFlow;

	protected string _narrative;

	protected ArrayList _treatableDiseaseArray;

	public ScenarioDoctorTreatmentsRelationship(SimChild theChild, SimDoctor theDoctor, bool inPushFlow)
	{
	}

	public ScenarioDoctorTreatmentsRelationship(SimParent theParent, SimDoctor theDoctor, bool inPushFlow)
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
