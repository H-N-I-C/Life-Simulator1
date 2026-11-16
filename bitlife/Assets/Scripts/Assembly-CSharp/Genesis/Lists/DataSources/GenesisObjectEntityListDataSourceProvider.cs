using System.Collections.Generic;
using Genesis.Controllers;
using Genesis.Entities;
using Genesis.Lists.Models;

namespace Genesis.Lists.DataSources
{
	public sealed class GenesisObjectEntityListDataSourceProvider : IGenesisListDataSourceProvider
	{
		private sealed class _003C_003Ec__DisplayClass7_0
		{
			public string entityType;

			internal bool _003CSelectFallbackRoute_003Eb__0(GenesisListFallbackRoute r)
			{
				return false;
			}
		}

		public const string ProviderKey = "genesis_entities:objects";

		private static readonly GenesisObjectEntityListDataSourceProvider Instance__BackingField;

		public static GenesisObjectEntityListDataSourceProvider Instance => Instance__BackingField;

		public IEnumerable<GenesisListDataSourceItem> GetItems(ScenarioController controller, GenesisListDefinition definition)
		{
			return null;
		}

		private static GenesisListDataSourceItem CreateItem(GenesisDataEntity entity, GenesisListFallbackRoute fallbackRoute)
		{
			return null;
		}

		private static List<ScenarioAction> BuildActionList(GenesisListFallbackRoute route)
		{
			return null;
		}

		private static GenesisListFallbackRoute SelectFallbackRoute(List<GenesisListFallbackRoute> routes, int index, string entityType)
		{
			return null;
		}
	}
}
