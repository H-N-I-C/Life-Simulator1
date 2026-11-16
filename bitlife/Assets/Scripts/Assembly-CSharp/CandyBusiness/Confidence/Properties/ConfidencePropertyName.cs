using System;

namespace CandyBusiness.Confidence.Properties
{
	public class ConfidencePropertyName : IEquatable<ConfidencePropertyName>
	{
		private readonly string Name__BackingField;

		public string Name => Name__BackingField;

		public ConfidencePropertyName(string name)
		{
		}

		public override int GetHashCode()
		{
			return 0;
		}

		public bool Equals(ConfidencePropertyName other)
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

		public static implicit operator string(ConfidencePropertyName propertyName)
		{
			return null;
		}
	}
}
