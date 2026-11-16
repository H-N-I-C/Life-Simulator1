using System;
using System.Collections;
using System.Collections.Generic;

namespace Genesis.SerializableLists
{
	public class ListInjector
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<SerializableMenuItem, string> _003C_003E9__8_0;

			public static Func<SerializableMenuItem, int> _003C_003E9__8_1;

			public static Func<SerializableMenuItem, string> _003C_003E9__9_0;

			internal string _003CSerializableMenuItems_003Eb__8_0(SerializableMenuItem q)
			{
				return null;
			}

			internal int _003CSerializableMenuItems_003Eb__8_1(SerializableMenuItem q)
			{
				return 0;
			}

			internal string _003CBuildSubItems_003Eb__9_0(SerializableMenuItem q)
			{
				return null;
			}
		}

		private SimValueProvider _valueProvider;

		private string _dataProviderName;

		public ListInjector(SimValueProvider valueProvider, string dataProviderName)
		{
		}

		public void InjectItems(ArrayList legacyList, SerializableList serializableList)
		{
		}

		private void InjectToParent(ArrayList legacyList, SerializableMenuItem item, ListItem newListItem)
		{
		}

		private int FindGroupSize(ArrayList legacyList, int groupPosition)
		{
			return 0;
		}

		private int FindSeparatorPosition(ArrayList legacyList, string serializableListTitle)
		{
			return 0;
		}

		private bool EvaluateDisabledCondition(SerializableMenuItem item)
		{
			return false;
		}

		private List<SerializableMenuItem> SerializableMenuItems(SerializableMenuItem item)
		{
			return null;
		}

		private List BuildSubItems(List<SerializableMenuItem> subItems, string title)
		{
			return null;
		}

		private ListItem BuildListItem(SerializableMenuItem item, bool isDisabled = false)
		{
			return null;
		}

		private ListItem BuildListCategorySeparation(string category)
		{
			return null;
		}

		private string GetSimData(string rawValue)
		{
			return null;
		}
	}
}
