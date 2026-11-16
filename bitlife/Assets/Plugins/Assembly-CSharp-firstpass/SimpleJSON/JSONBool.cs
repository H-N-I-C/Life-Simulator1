using System.Text;

namespace SimpleJSON
{
	public class JSONBool : JSONNode
	{
		private bool m_Data;

		public override string Value => null;

		public override bool AsBool => false;

		public override Enumerator GetEnumerator()
		{
			return default;
		}

		public JSONBool(bool aData)
		{
		}

		internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
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
	}
}
