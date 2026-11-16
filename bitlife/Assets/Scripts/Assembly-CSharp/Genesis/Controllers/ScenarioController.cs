using System;
using System.Collections.Generic;
using Game;
using Game.Genesis.Components;

namespace Genesis.Controllers
{
	public class ScenarioController
	{
		private ScenarioCSVData _csvData;

		private GenesisMemory _memory;

		private BracketParser _parser;

		public List<List<GenesisContent>> SelectedContent;

		public ScenarioCSVData CsvData => null;

		public GenesisMemory GenesisMemory => null;

		public BracketParser Parser => null;

		public ScenarioController(Dictionary<string, object> dataSource)
		{
		}

		public static List<string> SplitBluePrintChain(string scenarioId)
		{
			return null;
		}

		public static bool ChainContainsAbortOnFailureBlueprint(string scenarioId, ScenarioController controller)
		{
			return false;
		}

		public void LoadAndShowScenario(string csvContent, bool show, Action<string, Scenario> giveItemAction = null)
		{
		}

		public Scenario LoadScenario(string id)
		{
			return null;
		}

		public string GetScenarioIdForFilter(GENESIS_FILTER filter)
		{
			return null;
		}
	}
}
