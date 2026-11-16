using System;
using System.Collections;

namespace Genesis.FeatureContainers
{
	public static class GenesisFeatureContainerInjector
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Comparison<(GenesisFeatureContainerDefinition, ListItem)> _003C_003E9__0_0;

			internal int _003CInjectMenuEntries_003Eb__0_0((GenesisFeatureContainerDefinition, ListItem) a, (GenesisFeatureContainerDefinition, ListItem) b)
			{
				return 0;
			}
		}

		private sealed class _003C_003Ec__DisplayClass2_0
		{
			public string containerId;

			public string entryListId;

			public string csvName;

			internal void _003CBuildListItem_003Eb__0()
			{
			}
		}

		public static void InjectMenuEntries(ArrayList listItems, string menuId)
		{
		}

		private static void InsertAlphabetically(ArrayList listItems, ListItem newItem)
		{
		}

		private static ListItem BuildListItem(GenesisFeatureContainerDefinition def)
		{
			return null;
		}
	}
}
