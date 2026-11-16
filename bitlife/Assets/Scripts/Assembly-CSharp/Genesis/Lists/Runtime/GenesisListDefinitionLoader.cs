using System.Collections.Generic;
using Genesis.Lists.Models;

namespace Genesis.Lists.Runtime
{
	public static class GenesisListDefinitionLoader
	{
		private const string ResourcePrefix = "genesis_lists/";

		private static readonly Dictionary<string, string> ListIdToResourceName;

		private static bool _hasIndexedResources;

		public static GenesisListDefinition Load(string resourceName)
		{
			return null;
		}

		public static bool TryResolveResourceName(string listId, out string resourceName)
		{
			resourceName = null;
			return false;
		}

		private static void ValidateDefinition(GenesisListDefinition definition, string resourceName)
		{
		}

		private static void WarnOnMalformedSections(GenesisListDefinition definition, string resourceName)
		{
		}

		private static void RegisterListIdMapping(GenesisListDefinition definition, string resourceName)
		{
		}

		private static void IndexAllDefinitions()
		{
		}
	}
}
