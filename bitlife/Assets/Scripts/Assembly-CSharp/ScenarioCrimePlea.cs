using System.Collections;

public class ScenarioCrimePlea : Scenario
{
	protected ArrayList _crimeArray;

	protected int _odds;

	protected SimCrime _crime;

	protected int _pleaBargainYears;

	private readonly bool _falseAccusation;

	public ScenarioCrimePlea(ArrayList theCrimeArray, int theOdds, bool falseAccusation)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
