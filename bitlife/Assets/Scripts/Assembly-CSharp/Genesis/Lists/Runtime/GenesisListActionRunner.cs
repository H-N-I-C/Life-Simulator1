using System.Collections.Generic;
using Game.Genesis.BluePrints;
using Genesis.Controllers;
using Genesis.Services;

namespace Genesis.Lists.Runtime
{
	public static class GenesisListActionRunner
	{
		public static void Run(GenesisListItemRuntimeContext context)
		{
		}

		internal static List BuildSelectionList(GenesisListItemRuntimeContext context, string listResourceName, IReadOnlyList<string> focusSetupRoutes)
		{
			return null;
		}

		private static bool TryPrepareActionContext(GenesisListItemRuntimeContext context)
		{
			return false;
		}

		private static bool TryApplyObjectDataFocus(GenesisListItemRuntimeContext context)
		{
			return false;
		}

		private static bool TryApplyFocusEntityReference(GenesisListItemRuntimeContext context)
		{
			return false;
		}

		public static bool TryApplyFocusEntityReferenceForTests(GenesisListItemRuntimeContext context)
		{
			return false;
		}

		public static bool TryApplyObjectDataFocusForTests(GenesisListItemRuntimeContext context)
		{
			return false;
		}

		private static bool ExecuteAction(ScenarioAction action, GenesisListItemRuntimeContext context)
		{
			return false;
		}

		private static bool ExecuteActionInternal(ScenarioAction action, GenesisListItemRuntimeContext context, bool pureBlueprintChainIsTerminal)
		{
			return false;
		}

		private static bool ExecuteAdvancedLogicAction(AdvancedLogicBlock advancedLogic, GenesisListItemRuntimeContext context)
		{
			return false;
		}

		private static ScenarioCondition BuildAdvancedRouteCondition(GenesisAdvancedLogicRouter.Selection selection, ScenarioController controller)
		{
			return null;
		}

		public static ScenarioCondition BuildAdvancedRouteConditionForTests(GenesisAdvancedLogicRouter.Selection selection, ScenarioController controller)
		{
			return null;
		}

		private static Scenario ResolveScenario(ScenarioController controller, ScenarioCondition condition, Scenario nextScenario, IBluePrint executedBlueprint, out bool chainAborted, out bool completedPureBlueprintChain, out bool deferred)
		{
			chainAborted = default;
			completedPureBlueprintChain = default;
			deferred = default;
			return null;
		}

		private static void ApplySelectedFocus(GenesisListItemRuntimeContext context)
		{
		}

		private static void SetCurrentListNpc(ScenarioController controller, SimPerson person)
		{
		}

		private static void ApplyFocusedEntityContext(GenesisListItemRuntimeContext context)
		{
		}

		private static void ApplyOwningContainer(GenesisListItemRuntimeContext context)
		{
		}

		private static void ApplyOwningContainerToRenderedScenario(GenesisListItemRuntimeContext context, Scenario scenario)
		{
		}

		public static void ApplyOwningContainerToRenderedScenarioForTests(GenesisListItemRuntimeContext context, Scenario scenario)
		{
		}
	}
}
