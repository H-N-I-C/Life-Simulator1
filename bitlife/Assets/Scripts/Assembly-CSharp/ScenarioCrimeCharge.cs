using System.Collections;

public class ScenarioCrimeCharge : Scenario
{
	protected ArrayList _crimeArray;

	protected float _firm1Cost;

	protected float _firm2Cost;

	protected bool _excludeFirm1;

	protected bool _excludeFirm2;

	protected bool _falseAccusation;

	public ScenarioCrimeCharge(ArrayList theCrimeArray, bool shouldExcludeFirm1, bool shouldExcludeFirm2, bool wasFalseAccusation)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
