using System.Text;

namespace SimpleJSON
{
	internal class JSONLazyCreator : JSONNode
	{
		private JSONNode m_Node;

		private string m_Key;

		// C# has no syntax for parameterized property 'Item'.
		public override JSONNode get_Item(string aKey)
		{
			return null;
		}

		public override int AsInt => 0;

		public override double AsDouble => 0.0;

		public override bool AsBool => false;

		public override Enumerator GetEnumerator()
		{
			return default;
		}

		public JSONLazyCreator(JSONNode aNode)
		{
		}

		public JSONLazyCreator(JSONNode aNode, string aKey)
		{
		}

		private T Set<T>(T aVal)
		{
			return default;
		}

		public override void Add(JSONNode aItem)
		{
		}

		public override void Add(string aKey, JSONNode aItem)
		{
		}

		public override bool Equals(object obj)
		{
			return false;
		}

		public override int GetHashCode()
		{
			return 0;
		}

		internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
		{
		}
	}
}
