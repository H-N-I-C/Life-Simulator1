using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Genesis.Controllers;
using Genesis.Lists.Models;

namespace Genesis.Services
{
	public static class GenesisAdvancedLogicRouter
	{
		[Serializable]
		private sealed class RoutePoolHistoryDocument
		{
			private Dictionary<string, List<string>> Entries__BackingField;

			public Dictionary<string, List<string>> Entries
			{
				get
				{
					return Entries__BackingField;
				}
				set
				{
					Entries__BackingField = value;
				}
			}
		}

		internal sealed class PendingHistoryWrite
		{
			private string CompositeKey__BackingField;

			private string RouteTo__BackingField;

			public string CompositeKey
			{
				get
				{
					return CompositeKey__BackingField;
				}
				set
				{
					CompositeKey__BackingField = value;
				}
			}

			public string RouteTo
			{
				get
				{
					return RouteTo__BackingField;
				}
				set
				{
					RouteTo__BackingField = value;
				}
			}
		}

		public sealed class Selection
		{
			private readonly IReadOnlyList<RouteOption> RoutePath__BackingField;

			private readonly PendingHistoryWrite PendingHistory__BackingField;

			public IReadOnlyList<RouteOption> RoutePath => RoutePath__BackingField;

			public RouteOption CategoryOption => null;

			public RouteOption SelectedRoute => null;

			public string RouteTo => null;

			public string ScenarioFile => null;

			internal PendingHistoryWrite PendingHistory => PendingHistory__BackingField;

			internal Selection(List<RouteOption> routePath, PendingHistoryWrite pendingHistory)
			{
			}
		}

		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<GenesisListDataSourceItem, bool> _003C_003E9__23_0;

			public static Func<GenesisListDataSourceItem, bool> _003C_003E9__23_1;

			internal bool _003CTryResolveEntityPopulation_003Eb__23_0(GenesisListDataSourceItem item)
			{
				return false;
			}

			internal bool _003CTryResolveEntityPopulation_003Eb__23_1(GenesisListDataSourceItem item)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass20_0
		{
			public PendingHistoryWrite pending;

			internal bool _003CCommitSuccessfulRoute_003Eb__0(string route)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass23_0
		{
			public HashSet<string> excludedEntityIds;

			internal bool _003CTryResolveEntityPopulation_003Eb__2(GenesisListDataSourceItem item)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass23_1
		{
			public bool acceptsMale;

			public bool acceptsFemale;

			public bool acceptsBoth;

			internal bool _003CTryResolveEntityPopulation_003Eb__3(GenesisListDataSourceItem item)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass27_0
		{
			public RouteOption category;

			public ScenarioController controller;

			public IReadOnlyCollection<string> usedRoutes;

			public string forcedChildRoute;

			internal bool _003CTryResolveCategory_003Eb__0(RouteOption child)
			{
				return false;
			}

			internal bool _003CTryResolveCategory_003Eb__1(RouteOption child)
			{
				return false;
			}

			internal bool _003CTryResolveCategory_003Eb__2(RouteOption child)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass27_1
		{
			public RouteOption child;

			internal bool _003CTryResolveCategory_003Eb__3(string route)
			{
				return false;
			}
		}

		private const string RoutePoolHistoryMemoryKey = "__genesis_route_pool_history_v1";

		private const int MaximumNumericExpressionLength = 4096;

		private const int MaximumNumericExpressionTokens = 512;

		private const int MaximumNumericExpressionParenthesisDepth = 32;

		private const int MaximumNumericExpressionTagLength = 256;

		private static readonly Regex DirectGenesisNumericToken;

		private static readonly Regex DirectNpcNumericTag;

		private static readonly Regex DirectHeroNumericTag;

		private static readonly Regex DirectContainerNumericTag;

		private static readonly Regex DirectGfcNumericTag;

		private static readonly Regex PlainNumericValue;

		public static bool TrySelect(AdvancedLogicBlock advancedLogic, ScenarioController controller, out Selection selection)
		{
			selection = null;
			return false;
		}

		public static bool TryApplyFocusSetup(string setupRoute, ScenarioController controller, out string focusedEntityId)
		{
			focusedEntityId = null;
			return false;
		}

		private static bool RejectFocusSetup(ScenarioController controller, string warning = null)
		{
			return false;
		}

		private static void ClearFocusSetupContext(ScenarioController controller)
		{
		}

		private static void ClearEntityFocusSetupContext(ScenarioController controller)
		{
		}

		public static void ApplySideEffects(Selection selection, ScenarioController controller)
		{
		}

		public static bool CommitSuccessfulRoute(Selection selection, ScenarioController controller)
		{
			return false;
		}

		private static AdvancedLogicThen ResolveWinningThen(AdvancedLogicBlock advancedLogic, ScenarioController controller)
		{
			return null;
		}

		public static bool IsConditionSatisfied(AdvancedLogicCondition condition, ScenarioController controller)
		{
			return false;
		}

		public static bool TryResolveEntityPopulation(DesignerEntityDropdownPopulation population, ScenarioController controller, out List<GenesisListDataSourceItem> items, out string failureReason)
		{
			items = null;
			failureReason = null;
			return false;
		}

		private static bool TryResolveEntityPopulationExclusions(IEnumerable<string> authoredIds, ScenarioController controller, out HashSet<string> excludedEntityIds, out string failureReason)
		{
			excludedEntityIds = null;
			failureReason = null;
			return false;
		}

		private static bool TryValidateEntityPopulationDisplay(DesignerEntityDropdownPopulation population, IEnumerable<GenesisListDataSourceItem> items, out string failureReason)
		{
			failureReason = null;
			return false;
		}

		private static bool ContainsUnresolvedTag(string value)
		{
			return false;
		}

		private static bool TryResolveCategory(RouteOption category, ScenarioController controller, string forcedChildRoute, string focusedEntityIdOverride, out Selection selection)
		{
			selection = null;
			return false;
		}

		private static bool IsPooledScenarioEligible(RouteOption child, RouteOption category, ScenarioController controller)
		{
			return false;
		}

		private static RouteOption SelectWeightedRoute(IReadOnlyList<RouteOption> routeOptions)
		{
			return null;
		}

		private static bool EvaluateCondition(AdvancedLogicCondition condition, ScenarioController controller)
		{
			return false;
		}

		private static bool EvaluateBooleanExpression(string expression, ScenarioController controller)
		{
			return false;
		}

		private static bool TryBuildDecimalExpression(string expression, ScenarioController controller, out string decimalExpression)
		{
			decimalExpression = null;
			return false;
		}

		private static bool TryResolveGenesisNumericToken(string token, ScenarioController controller, out decimal value)
		{
			value = 0m;
			return false;
		}

		private static bool TryResolveMasterNumericToken(string token, ScenarioController controller, out decimal value)
		{
			value = 0m;
			return false;
		}

		private static bool TryConvertToDecimal(object stored, out decimal value)
		{
			value = 0m;
			return false;
		}

		private static bool TryParsePlainDecimal(string raw, out decimal value)
		{
			value = 0m;
			return false;
		}

		private static void AppendDecimal(StringBuilder output, decimal value)
		{
		}

		private static bool ValidateBlueprintRoute(string routeTo, ScenarioController controller)
		{
			return false;
		}

		private static void ApplyRouteOptionSideEffects(RouteOption option, ScenarioController controller)
		{
		}

		private static bool TryGetFocusedEntityId(ScenarioController controller, out string focusedEntityId)
		{
			focusedEntityId = null;
			return false;
		}

		private static string MakeCompositeHistoryKey(string poolId, string focusedEntityId)
		{
			return null;
		}

		private static bool TryReadHistory(ScenarioController controller, out RoutePoolHistoryDocument history)
		{
			history = null;
			return false;
		}
	}
}
