using System;

namespace CandyBusiness.Confidence
{
	public class ConfidenceAttributeName : IEquatable<ConfidenceAttributeName>
	{
		private readonly string Name__BackingField;

		public string Name => Name__BackingField;

		public ConfidenceAttributeName(string name)
		{
		}

		public override int GetHashCode()
		{
			return 0;
		}

		public bool Equals(ConfidenceAttributeName other)
		{
			return false;
		}

		public override bool Equals(object obj)
		{
			return false;
		}

		public override string ToString()
		{
			return null;
		}

		public static implicit operator string(ConfidenceAttributeName attributeName)
		{
			return null;
		}
	}
}
