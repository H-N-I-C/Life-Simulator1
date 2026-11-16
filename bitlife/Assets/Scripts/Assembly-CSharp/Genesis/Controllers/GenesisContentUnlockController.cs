using System.Collections.Generic;

namespace Genesis.Controllers
{
	public static class GenesisContentUnlockController
	{
		private const string LoanResourceName = "genesis_loan";

		private const string ArsonResourceName = "genesis_arson";

		private const string ArsonEntryScenarioId = "scenario.arson.chooseTargetAndAccelerant";

		private const string SeniorCenterResourceName = "genesis_senior_center";

		private const string SeniorCenterMenuScenarioId = "SENIOR_CENTER_MENU";

		private static Dictionary<string, GenesisContentData> _contentSetupData;

		private static void Init()
		{
		}

		public static bool CanLaunch(string contentId)
		{
			return false;
		}

		public static bool TryLaunch(string contentId, bool requireUnlocked = true)
		{
			return false;
		}

		public static void ShowGenesisLoanActivity()
		{
		}

		public static void ShowGenesisArsonActivity()
		{
		}
	}
}
