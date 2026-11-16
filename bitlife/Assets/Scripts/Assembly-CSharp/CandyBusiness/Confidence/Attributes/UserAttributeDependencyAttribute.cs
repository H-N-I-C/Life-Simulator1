using System;
using CandyBusiness.Confidence.Dependencies;

namespace CandyBusiness.Confidence.Attributes
{
	public sealed class UserAttributeDependencyAttribute : Attribute
	{
		private DependencyMode Mode__BackingField;

		private float ExpectedSeconds__BackingField;

		private string Reason__BackingField;

		private string Ticket__BackingField;

		public DependencyMode Mode
		{
			get
			{
				return Mode__BackingField;
			}
			set
			{
				Mode__BackingField = value;
			}
		}

		public float ExpectedSeconds
		{
			get
			{
				return ExpectedSeconds__BackingField;
			}
			set
			{
				ExpectedSeconds__BackingField = value;
			}
		}

		public string Reason
		{
			get
			{
				return Reason__BackingField;
			}
			set
			{
				Reason__BackingField = value;
			}
		}

		public string Ticket
		{
			get
			{
				return Ticket__BackingField;
			}
			set
			{
				Ticket__BackingField = value;
			}
		}

		public override string ToString()
		{
			return null;
		}
	}
}
