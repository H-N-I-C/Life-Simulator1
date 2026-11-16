using System.Text;

namespace SimpleJSON
{
	public class JSONNumber : JSONNode
	{
		private double m_Data;

		public override string Value => null;

		public override double AsDouble => 0.0;

		public override Enumerator GetEnumerator()
		{
			return default;
		}

		public JSONNumber(double aData)
		{
		}

		internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
		{
		}

		private static bool IsNumeric(object value)
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
	}
}
