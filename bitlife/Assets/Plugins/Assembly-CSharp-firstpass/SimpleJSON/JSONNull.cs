using System.Text;

namespace SimpleJSON
{
	public class JSONNull : JSONNode
	{
		private static JSONNull m_StaticInstance;

		public static bool reuseSameInstance;

		public override string Value => null;

		public override bool AsBool => false;

		public static JSONNull CreateOrGet()
		{
			return null;
		}

		private JSONNull()
		{
		}

		public override Enumerator GetEnumerator()
		{
			return default;
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
