using System.Collections.Generic;
using Genesis.Simulation;

namespace Genesis.SerializableLists
{
	public class SimValueProvider
	{
		private Dictionary<string, ISimDataProvider> _providers;

		public string GetSimValue(string providerName, string simValueRaw)
		{
			return null;
		}

		private ISimDataProvider TryLoadDataProvider(string providerName)
		{
			return null;
		}
	}
}
