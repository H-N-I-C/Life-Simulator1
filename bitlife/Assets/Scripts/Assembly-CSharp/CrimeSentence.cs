using System.Collections;

public class CrimeSentence
{
	private ArrayList _crimeArray;

	private readonly int _sentenceOverrideYears;

	private readonly string _prisonSecurityOverride;

	private readonly string _prisonNameOverride;

	private string _narrative;

	private string _title;

	private string _body;

	public CrimeSentence(ArrayList theCrimeArray, int theSentenceOverrideYears, string prisonSecurityOverride, string prisonNameOverride)
	{
	}

	public Scenario GetNextScenario()
	{
		return null;
	}

	public Scenario GetNextScenarioForJuvie()
	{
		return null;
	}

	private bool TryGetPrisonSecurityOverride(out int prisonSecurity)
	{
		prisonSecurity = default;
		return false;
	}

	private static SimCompany CreatePrisonCompany(int companyType, string prisonNameOverride)
	{
		return null;
	}
}
