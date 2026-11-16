using System.Collections.Generic;
using System.Text;

namespace SimpleJSON
{
	public abstract class JSONNode
	{
		public struct Enumerator
		{
			private enum Type
			{
				None = 0,
				Array = 1,
				Object = 2
			}

			private Type type;

			private Dictionary<string, JSONNode>.Enumerator m_Object;

			private List<JSONNode>.Enumerator m_Array;

			public KeyValuePair<string, JSONNode> Current => default;

			public Enumerator(List<JSONNode>.Enumerator aArrayEnum)
			{
				type = Type.None;
				m_Object = default;
				m_Array = default;
			}

			public Enumerator(Dictionary<string, JSONNode>.Enumerator aDictEnum)
			{
				type = Type.None;
				m_Object = default;
				m_Array = default;
			}

			public bool MoveNext()
			{
				return false;
			}
		}

		public static bool forceASCII;

		private static StringBuilder m_EscapeBuilder;

		// C# has no syntax for parameterized property 'Item'.
		public virtual JSONNode get_Item(string aKey)
		{
			return null;
		}

		public virtual string Value => null;

		public virtual double AsDouble => 0.0;

		public virtual int AsInt => 0;

		public virtual bool AsBool => false;

		internal static StringBuilder EscapeBuilder => null;

		public virtual void Add(string aKey, JSONNode aItem)
		{
		}

		public virtual void Add(JSONNode aItem)
		{
		}

		public override string ToString()
		{
			return null;
		}

		internal abstract void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode);

		public abstract Enumerator GetEnumerator();

		public static implicit operator JSONNode(string s)
		{
			return null;
		}

		public static implicit operator string(JSONNode d)
		{
			return null;
		}

		public static implicit operator JSONNode(double n)
		{
			return null;
		}

		public static implicit operator JSONNode(bool b)
		{
			return null;
		}

		public static bool operator ==(JSONNode a, object b)
		{
			return false;
		}

		public static bool operator !=(JSONNode a, object b)
		{
			return false;
		}

		public override bool Equals(object obj)
		{
			return false;
		}

		public override int GetHashCode()
		{
			return 0;
		}

		internal static string Escape(string aText)
		{
			return null;
		}

		private static JSONNode ParseElement(string token, bool quoted)
		{
			return null;
		}

		public static JSONNode Parse(string aJSON)
		{
			return null;
		}
	}
}
