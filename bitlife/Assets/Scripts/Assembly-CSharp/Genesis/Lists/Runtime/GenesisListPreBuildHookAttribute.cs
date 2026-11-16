using System;
using System.Collections.Generic;

namespace Genesis.Lists.Runtime
{
	public sealed class GenesisListPreBuildHookAttribute : Attribute
	{
		private readonly IReadOnlyList<string> ListIds__BackingField;

		private string DataSourceKey__BackingField;

		public IReadOnlyList<string> ListIds => ListIds__BackingField;

		public string DataSourceKey
		{
			get
			{
				return DataSourceKey__BackingField;
			}
			set
			{
				DataSourceKey__BackingField = value;
			}
		}

		public GenesisListPreBuildHookAttribute(string[] listIds)
		{
		}
	}
}
