using Genesis.Controllers;
using Genesis.Entities;

namespace Game.Genesis.BluePrints
{
	public sealed class BluePrintHookupOutcome : IBluePrint
	{
		private ScenarioController _controller;

		private bool _payloadIsValid;

		private bool _executed;

		private bool _succeeded;

		private string _targetScenarioId;

		private Scenario _nextScenario;

		private BluePrintType BluePrintType__BackingField;

		public BluePrintType BluePrintType
		{
			get
			{
				return BluePrintType__BackingField;
			}
			set
			{
				BluePrintType__BackingField = value;
			}
		}

		public void Setup(BluePrintData data)
		{
		}

		public bool ConditionSelected()
		{
			return false;
		}

		public Scenario GetNextScenario(string followupScenario)
		{
			return null;
		}

		private void EnsureExecuted()
		{
		}

		private static bool IsCommittedHookup(SimEntity actor)
		{
			return false;
		}

		private static bool ReadBool(object raw)
		{
			return false;
		}
	}
}
