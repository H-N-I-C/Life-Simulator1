using System.Collections.Generic;
using Game.Genesis.BluePrints;

namespace Genesis.Controllers
{
	public class ScenarioAction
	{
		private ScenarioBase _sourceScenario;

		private ScenarioDefinitionModel _model;

		private List<ScenarioCondition> ConditionList__BackingField;

		private int Index__BackingField;

		private bool PreAction__BackingField;

		private AdvancedLogicBlock AdvancedLogic__BackingField;

		public List<ScenarioCondition> ConditionList
		{
			get
			{
				return ConditionList__BackingField;
			}
			set
			{
				ConditionList__BackingField = value;
			}
		}

		public int Index
		{
			get
			{
				return Index__BackingField;
			}
			set
			{
				Index__BackingField = value;
			}
		}

		public bool PreAction
		{
			get
			{
				return PreAction__BackingField;
			}
			set
			{
				PreAction__BackingField = value;
			}
		}

		public AdvancedLogicBlock AdvancedLogic
		{
			get
			{
				return AdvancedLogic__BackingField;
			}
			set
			{
				AdvancedLogic__BackingField = value;
			}
		}

		public bool Run(ScenarioBase sourceScenario, ScenarioDefinitionModel model, ScenarioFieldsModel data)
		{
			return false;
		}

		private bool RunAdvancedLogic(ScenarioController controller)
		{
			return false;
		}

		private bool TrySetupRoute(string routeTo, string scenarioFile, ScenarioController controller)
		{
			return false;
		}

		private bool SetupCondition(ScenarioCondition condition, Scenario nextScenario, ScenarioController controller, IBluePrint executedBlueprint)
		{
			return false;
		}
	}
}
