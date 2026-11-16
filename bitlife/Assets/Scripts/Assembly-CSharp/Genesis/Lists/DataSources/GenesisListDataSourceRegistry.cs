using System.Collections.Generic;

namespace Genesis.Lists.DataSources
{
	public static class GenesisListDataSourceRegistry
	{
		private static readonly Dictionary<string, IGenesisListDataSourceProvider> Providers;

		public static void RegisterProvider(string key, IGenesisListDataSourceProvider provider)
		{
		}

		public static bool TryGetProvider(string key, out IGenesisListDataSourceProvider provider)
		{
			provider = null;
			return false;
		}

		public static bool TryResolveProvider(string dataSource, out IGenesisListDataSourceProvider provider)
		{
			provider = null;
			return false;
		}
	}
}
