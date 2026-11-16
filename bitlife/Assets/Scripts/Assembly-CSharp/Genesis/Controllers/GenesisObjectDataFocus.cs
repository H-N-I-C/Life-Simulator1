using System;

namespace Genesis.Controllers
{
	[Serializable]
	public sealed class GenesisObjectDataFocus
	{
		private readonly string RootEntityId__BackingField;

		private readonly string RootEntityType__BackingField;

		private readonly string RootAlias__BackingField;

		private readonly string PathPrefix__BackingField;

		public string RootEntityId => RootEntityId__BackingField;

		public string RootEntityType => RootEntityType__BackingField;

		public string RootAlias => RootAlias__BackingField;

		public string PathPrefix => PathPrefix__BackingField;

		public GenesisObjectDataFocus(string rootEntityId, string rootEntityType, string rootAlias, string pathPrefix)
		{
		}

		private static string NormalizePathPart(string value)
		{
			return null;
		}
	}
}
