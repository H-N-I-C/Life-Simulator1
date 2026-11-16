public class ScenarioUFMCustomLifeGuidance : Scenario
{
	public enum Branch
	{
		Switched = 0,
		WaitAdult = 1,
		WaitUnder18 = 2,
		WaitPrison = 3
	}

	private const string DefaultT1MenuName = "Occupation";

	private const string FightMenuName = "Fight";

	private const string MyStuffMenuName = "My Stuff";

	private readonly Branch _branch;

	private bool _hasLogged;

	public ScenarioUFMCustomLifeGuidance(Branch branch, Scenario nextScenario = null)
	{
	}

	public override void RefreshScenario()
	{
	}

	public void InsertScenarioChainBeforeCurrentNext(Scenario scenarioChain)
	{
	}

	public static Branch GetWaitBranch(bool isInPrison, int heroAge)
	{
		return Branch.Switched;
	}

	public static string GetT1MenuNameForState(int state)
	{
		return null;
	}

	private string GetTitle(Branch branch)
	{
		return null;
	}

	private void SetBody(Branch branch, string t1MenuName)
	{
	}

	public static string GetLog(Branch branch, string t1MenuName)
	{
		return null;
	}

	private static string ResolveCurrentT1MenuName()
	{
		return null;
	}

	private static string NormalizeMenuName(string t1MenuName)
	{
		return null;
	}
}
