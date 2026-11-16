using System.Collections.Generic;

namespace Genesis.Simulation
{
	public static class SimDataProviderFactory
	{
		private static readonly Dictionary<string, ISimDataProvider> ProviderMap;

		public static ISimDataProvider GetProvider(string typeName)
		{
			return null;
		}

		private static ISimDataProvider CreateDataProvider(string simTypeName)
		{
			return null;
		}
	}
}
