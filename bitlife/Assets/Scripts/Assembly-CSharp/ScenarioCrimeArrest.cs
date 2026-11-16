using System.Collections;

public class ScenarioCrimeArrest : Scenario
{
	private readonly ArrayList _crimeArray;

	private readonly SimHero _hero;

	private readonly float _attitude;

	public ScenarioCrimeArrest(ArrayList theCrimeArray)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private float GetCountryStrictness()
	{
		return 0f;
	}

	private void VampireArrestFlow()
	{
	}

	private void StandardArrestFlow()
	{
	}
}
