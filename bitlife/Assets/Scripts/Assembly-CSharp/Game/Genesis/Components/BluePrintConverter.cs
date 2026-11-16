using Game.Genesis.BluePrints;
using Genesis.Controllers;

namespace Game.Genesis.Components
{
	public class BluePrintConverter
	{
		public static bool TryGetBluePrintTypeForScenarioId(string scenarioId, out BluePrintType bluePrintType)
		{
			bluePrintType = default;
			return false;
		}

		public static IBluePrint ConvertToBluePrint(string scenarioId, ScenarioController controller)
		{
			return null;
		}

		private static IBluePrint GetBluePrintFromString(string typeString, string data, bool hasExplicitData, ScenarioController controller)
		{
			return null;
		}
	}
}
