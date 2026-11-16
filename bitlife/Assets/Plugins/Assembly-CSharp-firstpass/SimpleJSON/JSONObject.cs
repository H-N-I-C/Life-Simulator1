using System.Collections.Generic;
using System.Text;

namespace SimpleJSON
{
	public class JSONObject : JSONNode
	{
		private Dictionary<string, JSONNode> m_Dict;

		private bool inline;

		// C# has no syntax for parameterized property 'Item'.
		public override JSONNode get_Item(string aKey)
		{
			return null;
		}

		public override Enumerator GetEnumerator()
		{
			return default;
		}

		public override void Add(string aKey, JSONNode aItem)
		{
		}

		internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
		{
		}
	}
}
