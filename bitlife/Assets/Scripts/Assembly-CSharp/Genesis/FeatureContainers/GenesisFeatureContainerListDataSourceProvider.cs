using System;
using System.Collections.Generic;
using Genesis.Controllers;
using Genesis.Entities;
using Genesis.Lists.DataSources;
using Genesis.Lists.Models;

namespace Genesis.FeatureContainers
{
	public sealed class GenesisFeatureContainerListDataSourceProvider : IGenesisListDataSourceProvider
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<KeyValuePair<double, GenesisListDataSourceItem>, double> _003C_003E9__7_0;

			public static Func<KeyValuePair<double, GenesisListDataSourceItem>, double> _003C_003E9__7_1;

			internal double _003CSortItemsByMetric_003Eb__7_0(KeyValuePair<double, GenesisListDataSourceItem> kv)
			{
				return 0.0;
			}

			internal double _003CSortItemsByMetric_003Eb__7_1(KeyValuePair<double, GenesisListDataSourceItem> kv)
			{
				return 0.0;
			}
		}

		private sealed class _003C_003Ec__DisplayClass25_0
		{
			public string entityType;

			internal bool _003CSelectFallbackRoute_003Eb__0(GenesisListFallbackRoute r)
			{
				return false;
			}
		}

		public const string ProviderKey = "gfc";

		private static readonly GenesisFeatureContainerListDataSourceProvider Instance__BackingField;

		private static readonly Random _rng;

		private const int MaxDrawCount = 20;

		private static readonly string[] TwoCharDrawFilterOps;

		private static readonly char[] OneCharDrawFilterOps;

		public static GenesisFeatureContainerListDataSourceProvider Instance => Instance__BackingField;

		public IEnumerable<GenesisListDataSourceItem> GetItems(ScenarioController controller, GenesisListDefinition definition)
		{
			return null;
		}

		private static List<GenesisListDataSourceItem> SortItemsByMetric(List<GenesisListDataSourceItem> items, GenesisListDefinition definition)
		{
			return null;
		}

		private static string ExtractMetricPropertyName(string raw)
		{
			return null;
		}

		private static bool TryReadEntityMetric(object focusObject, string property, out double value)
		{
			value = default;
			return false;
		}

		private static List<string> ApplyDrawFilter(List<string> pool, GenesisListDefinition definition, GenesisEntityContainer entityContainer)
		{
			return null;
		}

		private static bool TryParseDrawFilter(string filter, out string property, out string op, out string value)
		{
			property = null;
			op = null;
			value = null;
			return false;
		}

		private static bool MatchesDrawFilter(IGenesisEntity entity, string property, string op, string rawValue, bool valueIsNumeric, double numericValue)
		{
			return false;
		}

		private static string ReadEntityFilterProperty(IGenesisEntity entity, string property)
		{
			return null;
		}

		private static List<string> ResolveCachedDraw(GenesisFeatureContainer container, GenesisListDefinition definition, string collectionName, List<string> pool, int heroAge)
		{
			return null;
		}

		public static bool TryParseDataSource(string dataSource, out string containerId, out string collectionName)
		{
			containerId = null;
			collectionName = null;
			return false;
		}

		private static GenesisListDataSourceItem CreateItem(SimEntity entity, GenesisListDefinition definition, string containerId, string collectionName, GenesisListFallbackRoute fallbackRoute)
		{
			return null;
		}

		private static string ResolveBoundProperty(SimEntity entity, string boundProperty)
		{
			return null;
		}

		private static GenesisListBarGraphDefinition BuildEntityBarGraph(SimEntity entity, GenesisListBarGraphDefinition authored)
		{
			return null;
		}

		private static GenesisListDataSourceItem CreateObjectItem(GenesisDataEntity entity, GenesisListDefinition definition, string containerId, string collectionName, GenesisListFallbackRoute fallbackRoute)
		{
			return null;
		}

		private static string ResolveBoundObjectProperty(GenesisDataEntity entity, string boundProperty)
		{
			return null;
		}

		private static GenesisListBarGraphDefinition BuildObjectBarGraph(GenesisDataEntity entity, GenesisListBarGraphDefinition authored)
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
