using System.Collections.Generic;

namespace Genesis.FeatureContainers
{
	public class TriggerGenesisFeatureContainer : TriggerManager.ITriggerScenario
	{
		public Scenario GetScenario(List<TriggerEvent> triggers)
		{
			return null;
		}

		public static Dictionary<string, string> ParseEventHooks(string eventHooksStr)
		{
			return null;
		}
	}
}
