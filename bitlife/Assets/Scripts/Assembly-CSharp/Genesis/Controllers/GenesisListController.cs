using System;
using System.Collections.Generic;
using Genesis.Lists.Models;
using Genesis.Lists.Runtime;

namespace Genesis.Controllers
{
	public static class GenesisListController
	{
		private static readonly Dictionary<string, List<Action<GenesisListDefinition, ScenarioController>>> PreBuildHooks;

		private static readonly Dictionary<string, List<Action<GenesisListDefinition, ScenarioController>>> DataSourcePreBuildHooks;

		public static void RegisterPreBuildHook(string listId, Action<GenesisListDefinition, ScenarioController> hook)
		{
		}

		public static void RegisterPreBuildHookForDataSource(string dataSourceKey, Action<GenesisListDefinition, ScenarioController> hook)
		{
		}

		public static bool ShowList(string resourceName, ScenarioController controller, bool dismissAllFirst = false)
		{
			return false;
		}

		public static bool ShowList(string resourceName, ScenarioController controller, GenesisListInvocationSource invocationSource, bool dismissAllFirst = false)
		{
			return false;
		}

		public static List BuildList(string resourceName, ScenarioController controller, GenesisListInvocationSource invocationSource = GenesisListInvocationSource.SelectionChild)
		{
			return null;
		}

		internal static void ApplyPreBuildHooks(GenesisListDefinition definition, ScenarioController controller)
		{
		}

		private static void InvokeHooks(GenesisListDefinition definition, ScenarioController controller, string key, Dictionary<string, List<Action<GenesisListDefinition, ScenarioController>>> registry, HashSet<Action<GenesisListDefinition, ScenarioController>> invoked)
		{
		}
	}
}
