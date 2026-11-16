using System;

namespace CandyBusiness.Confidence.Flags
{
	public class ConfidenceFlag : IEquatable<ConfidenceFlag>
	{
		public static readonly ConfidenceFlag Empty;

		private readonly string Name__BackingField;

		public string Name => Name__BackingField;

		public ConfidenceFlag(string name)
		{
		}

		public override string ToString()
		{
			return null;
		}

		public override int GetHashCode()
		{
			return 0;
		}

		public bool Equals(ConfidenceFlag other)
		{
			return false;
		}

		public override bool Equals(object obj)
		{
			return false;
		}

		public static implicit operator string(ConfidenceFlag flag)
		{
			return null;
		}
	}
}
