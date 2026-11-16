using System.Collections.Generic;

namespace Genesis.SerializableLists
{
	public class SerializableList
	{
		public string SimDataProvider;

		public string Parent;

		public string Group;

		public List<SerializableMenuItem> Items;

		public StaticCall InjectFromMethod;

		public string Requirements;
	}
}
