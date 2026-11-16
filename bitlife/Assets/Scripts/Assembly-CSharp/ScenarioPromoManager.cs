using System.Collections;
using System.Collections.Generic;

public class ScenarioPromoManager
{
	private sealed class _003C_003Ec__DisplayClass4_0
	{
		public string aScenario;

		internal bool _003CGetEligibleScenario_003Eb__0(KeyValuePair<string, int> t)
		{
			return false;
		}
	}

	private static int YearsBetweenPromoScenarios;

	private static Dictionary<string, int> _cache;

	private static int LastAgeForcePromoScenario;

	public static List<string> AllPromoScenarios;

	private static void Init()
	{
	}

	public static string GetEligibleScenario()
	{
		return null;
	}

	private static void LoadData(string json = "")
	{
	}

	public static void ValidateAndSetScenario(string aClassString, int amountLoops, ArrayList tierList)
	{
	}

	public static bool ValidateScenario(string aClassString)
	{
		return false;
	}

	public static Scenario GetPushPromoScenario(string aClassString)
	{
		return null;
	}
}
