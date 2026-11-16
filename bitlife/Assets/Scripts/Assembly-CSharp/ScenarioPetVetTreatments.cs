using System.Collections;

public class ScenarioPetVetTreatments : Scenario
{
	protected SimPet _pet;

	protected ArrayList _treatableDiseaseArray;

	protected ArrayList _treatableDiseaseStrings;

	protected int _vetGender;

	protected float _costTreatment1;

	protected float _costTreatment2;

	protected float _costEuthanize;

	protected string _narrative;

	public ScenarioPetVetTreatments(SimPet thePet)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
