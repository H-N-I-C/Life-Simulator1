using System;
using Genesis.Controllers;
using Genesis.Entities;
using Genesis.Lists.Models;

namespace Genesis.FeatureContainers
{
	public static class GenesisFeatureContainerAutoPopulator
	{
		public const string AutoPopulatedMarker = "_gfc_autopopulated";

		public const string DefaultAssignedFlag = "is_assigned_to_hero";

		private const int MaxTarget = 20;

		public static void EnsurePopulated(GenesisFeatureContainer container, GenesisListDefinition definition, string collectionName, GenesisEntityContainer entityContainer, int heroAge, ScenarioController controller = null)
		{
		}

		internal static void EnsurePopulated(GenesisFeatureContainer container, GenesisListDefinition definition, string collectionName, GenesisEntityContainer entityContainer, int heroAge, Func<string, GenesisDataEntity> generator, ScenarioController controller = null)
		{
		}

		private static void ResolveEntityStringTags(GenesisDataEntity entity, ScenarioController controller)
		{
		}

		private static GenesisDataEntity DefaultGenerator(string entityType)
		{
			return null;
		}

		public static void CleanupGeneratedForReroll(GenesisFeatureContainer container, string collectionName, GenesisEntityContainer entityContainer, string assignedFlag)
		{
		}

		public static bool TryResolveConfigForCachedDrawKey(string cachedDrawKey, string containerId, out GenesisAutoPopulateConfig config)
		{
			config = default;
			return false;
		}

		private static int ClampTarget(int requested)
		{
			return 0;
		}

		private static string ResolveAssignedFlag(GenesisListDefinition definition)
		{
			return null;
		}

		private static int CountLiveGeneratedNonAssigned(GenesisFeatureContainer container, string collectionName, GenesisEntityContainer entityContainer, string assignedFlag)
		{
			return 0;
		}

		private static bool IsAutoPopulated(IGenesisEntity entity)
		{
			return false;
		}

		private static bool IsAssigned(IGenesisEntity entity, string assignedFlag)
		{
			return false;
		}

		private static bool ReadBoolProperty(IGenesisEntity entity, string key)
		{
			return false;
		}

		private static string BuildCacheKey(string listId, string collectionName)
		{
			return null;
		}

		private static bool TrySplitCacheKey(string key, out string listId, out string collectionName)
		{
			listId = null;
			collectionName = null;
			return false;
		}

		private static GenesisListDefinition TryLoadListDefinition(string listId)
		{
			return null;
		}
	}
}
