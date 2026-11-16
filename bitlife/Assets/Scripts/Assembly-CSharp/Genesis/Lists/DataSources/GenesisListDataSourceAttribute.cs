using System;

namespace Genesis.Lists.DataSources
{
	public sealed class GenesisListDataSourceAttribute : Attribute
	{
		private readonly string Key__BackingField;

		public string Key => Key__BackingField;

		public GenesisListDataSourceAttribute(string key)
		{
		}
	}
}
