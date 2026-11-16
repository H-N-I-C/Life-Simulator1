using System.Collections.Generic;

namespace Genesis.Entities
{
	public static class GenesisTransientEntityStore
	{
		private static readonly Dictionary<string, GenesisDataEntity> Objects;

		public static int Count => 0;

		public static void Add(GenesisDataEntity entity)
		{
		}

		public static GenesisDataEntity Get(string entityId)
		{
			return null;
		}

		public static bool Remove(string entityId)
		{
			return false;
		}

		public static void Clear()
		{
		}
	}
}
